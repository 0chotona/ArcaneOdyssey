using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Chise_Attack : Attack
{
    [Header("데미지 박스 위치"), SerializeField] Transform _damageBoxPos;
    [Header("데미지 박스"), SerializeField] GameObject _damageBoxObj;
    Chise_DamageBox_Attack _damageBox;


    [Header("돌진 속도"), SerializeField] float _dashSpeed = 1f;
    [Header("돌진 거리"), SerializeField] float _dashDistance = 5f;
    
    public bool _IsMaxLevel => _isMaxLevel;

    Transform _playerTrs;
    PlayerMove _playerMove;

    [Header("쿨타임 거리"), SerializeField] float _coolDistance = 7f;
    [Header("슬래쉬 파티클"), SerializeField] ParticleSystem _particle;

    [Header("데미지 박스 크기"), SerializeField] Vector3 _damageBoxScale = new Vector3(3f, 0.5f, 3f);

    public float _timer = 0f;

    SkillGage _skillGage;
    int _skillCombo = 0;
    private void OnEnable()
    {
        _name = eSKILL.BrokenWing;
    }
    private void Start()
    {
        _damageBox = _damageBoxObj.GetComponent<Chise_DamageBox_Attack>();
        //_damageBoxObj.transform.SetParent(null);
        //StartCoroutine(CRT_Attack());
        _isMaxLevel = false;
        _skillGage = UIManager.Instance._SkillGage;
        _skillGage.SetSliderSetting(_coolDistance);

        _damageBoxObj.SetActive(false);
        _canAttack = true;

        _particle.Stop();
    }
    private void Update()
    {
        if(_playerMove._MoveDir != Vector3.zero)
        {
            /*
            쿨타임 = _coolDistance / _playerMove._FinalSpeed
            쿨타임 / 5f 초마다 업데이트 슬라이더
            */
            if(_canAttack)
            {
                _timer += Time.deltaTime;
                float updateInterval = _coolTime / 5f;


                if (_timer >= _coolTime)
                {

                    StartCoroutine(CRT_Attack());
                }

                // _timer가 updateInterval의 배수가 될 때마다 _skillGage 업데이트
                if (Mathf.Floor(_timer / updateInterval) > Mathf.Floor((_timer - Time.deltaTime) / updateInterval))
                {
                    _skillGage.UpdateSkillGage(_timer * _playerMove._FinalSpeed);
                }
            }
            
        }
    }
    public override void AttackInteract()
    {

        float finalDamage = _damage + (_damage * _buffStat._Att);


        bool isCritical = SkillManager.Instance.IsCritical(0f);
        if (isCritical)
        {
            finalDamage *= SkillManager.Instance._BaseCriDmg;
        }
        _damageBox.UpdateDamage(finalDamage);
        _damageBox.UpdateIsMaxLevel(_isMaxLevel);
        _damageBox.UpdateScale(_damageBoxScale * (1 + _buffStat._Range));
        _damageBoxObj.transform.position = _damageBoxPos.position;
        _damageBoxObj.SetActive(true);
    }
    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override IEnumerator CRT_Attack()
    {
        _canAttack = false;
        _timer = 0f;
        
        Vector3 targetPos = _playerTrs.position + _playerTrs.forward * _dashDistance;
        _playerMove.Dash(targetPos, _dashSpeed, _skillCombo);
        _playerMove.SetCanMove(false);
        _skillCombo = (_skillCombo == 0) ? 1 : 0;
        AttackInteract();
        
        yield return new WaitForSeconds(0.1f);
        _particle.Play();
        yield return new WaitForSeconds(0.2f);
        _damageBoxObj.SetActive(false);
        _playerMove.SetCanMove(true);
        yield return new WaitForSeconds(0.2f);

        
        _skillGage.UpdateSkillGage(0f);
        _canAttack = true;
    }
    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _coolTime = _coolDistance / _playerMove._FinalSpeed;
        _criRate = stat._criRate;
        if (_level >= 6)
            _isMaxLevel = true;

    }
    public override void StartAttack() { return; }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }

    public override void SetPlayerTrs(Transform playerTrs) 
    {
        _playerTrs = playerTrs;
        _playerMove = _playerTrs.GetComponent<PlayerMove>();
    }
    IEnumerator CRT_Dash()
    {
        Vector3 targetPos = _playerTrs.position + _playerTrs.forward * _dashDistance;
        while(Vector3.Distance(_playerTrs.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(_playerTrs.position, targetPos, _dashSpeed * Time.deltaTime);
            yield return null;
        }
        AttackInteract();
        yield return new WaitForSeconds(0.05f);
        _damageBoxObj.SetActive(false);
    }
}
