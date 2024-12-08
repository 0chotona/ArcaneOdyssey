using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Chise_Attack : Attack
{
    [Header("������ �ڽ� ��ġ"), SerializeField] Transform _damageBoxPos;
    [Header("������ �ڽ�"), SerializeField] GameObject _damageBoxObj;
    Chise_DamageBox_Attack _damageBox;



    [Header("���� �ӵ�"), SerializeField] float _dashSpeed = 1f;
    [Header("���� �Ÿ�"), SerializeField] float _dashDistance = 5f;
    
    public bool _IsMaxLevel => _isMaxLevel;

    Transform _playerTrs;
    PlayerMove _playerMove;

    [Header("��Ÿ�� �Ÿ�"), SerializeField] float _coolDistance = 7f;
    [Header("������ ��ƼŬ"), SerializeField] ParticleSystem _particle;
    [Header("��� ��ƼŬ"), SerializeField] ParticleSystem _airborneParticle;

    [Header("������ �ڽ� ũ��"), SerializeField] Vector3 _damageBoxScale = new Vector3(3f, 0.5f, 3f);

    [Header("��� �ð�"), SerializeField] float _airborneTime = 0.5f;
    [Header("��� ���ǵ�"), SerializeField] float _airborneSpeed = 5f;


    [Header("��� �ڽ�"), SerializeField] GameObject _airborneBox;

    [Header("��� ũ��"), SerializeField] Vector3 _airborneScale = new Vector3(6f, 1f, 6f);

    [Header("R ����"), SerializeField] Chise_Active_R _activeR;
    float _timer = 0f;

    SkillGage _skillGage;
    int _skillCombo = 0;
    private void OnEnable()
    {
        _name = eSKILL.BrokenWing;
        _particle.Stop();
    }
    private void Start()
    {
        _damageBox = _damageBoxObj.GetComponent<Chise_DamageBox_Attack>();
        _damageBox.UpdateCollider(false);
        //_damageBoxObj.transform.SetParent(null);
        //StartCoroutine(CRT_Attack());
        _isMaxLevel = false;
        _skillGage = UIManager.Instance._SkillGage;
        _skillGage.SetSliderSetting(_coolDistance);

        _canAttack = true;

        StartCoroutine(CRT_ParticleAwake());

        _damageBox.SetAirborneSetting(_airborneTime, _airborneSpeed);
    }
    private void Update()
    {
        if(_playerMove._MoveDir != Vector3.zero)
        {
            /*
            ��Ÿ�� = _coolDistance / _playerMove._FinalSpeed
            ��Ÿ�� / 5f �ʸ��� ������Ʈ �����̴�
            */
            if(_canAttack)
            {
                _timer += Time.deltaTime;
                float updateInterval = _coolTime / 5f;


                if (_timer >= _coolTime)
                {

                    StartCoroutine(CRT_Attack());
                }

                // _timer�� updateInterval�� ����� �� ������ _skillGage ������Ʈ
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
        _damageBox.UpdateCombo(_skillCombo);


        _damageBox.UpdateDamage(finalDamage);
        _damageBox.UpdateIsMaxLevel(_isMaxLevel);
        _damageBox.UpdateScale(_damageBoxScale * (1 + _buffStat._Range));
        _damageBoxObj.transform.position = _damageBoxPos.position;
        _damageBox.UpdateCollider(true);

        if (_isMaxLevel && _skillCombo == 1)
        {
            MaxLevelAirborne();
        }
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

        if (_skillCombo == 1)
        {
            StartCoroutine(CRT_PlayAirborneParticle());
            //_airborneParticle.Play();
        }
        else
        {
            _particle.Play();
            SoundManager.Instance.PlaySound(eCHARSOUNDTYPE.Chise_Attack);
        }
        //AttackInteract(); //�ݶ��̴� On
        yield return new WaitForSeconds(0.2f);
        if (_skillCombo == 1)
        {
            //StartCoroutine(CRT_PlayAirborneParticle());
            //_airborneParticle.Play();
        }
        if (_activeR._IsActive)
        {
            _activeR.ShootAttack();
        }
        AttackInteract(); //�ݶ��̴� On
        yield return new WaitForSeconds(0.05f);
        _damageBox.UpdateCollider(false);
        yield return new WaitForSeconds(0.1f);
        
        
        //_playerMove.SetCanMove(true);
        yield return new WaitForSeconds(0.2f);

        _playerMove.SetCanMove(true);
        _skillGage.UpdateSkillGage(0f);


        _skillCombo = (_skillCombo == 0) ? 1 : 0;
        _canAttack = true;
    }
    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _criRate = stat._criRate;

        _playerMove.UpdatePassiveSpeed(_criRate);
        _coolTime = _coolDistance / _playerMove._FinalSpeed;
        
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
    IEnumerator CRT_ParticleAwake()
    {
        _airborneParticle.Stop();
        _airborneParticle.Play();
        yield return new WaitForSeconds(0.3f);
        _airborneParticle.Pause();
    }
    IEnumerator CRT_PlayAirborneParticle()
    {
        SoundManager.Instance.PlaySound(eCHARSOUNDTYPE.Chise_Airbone);

        yield return new WaitForSeconds(0.1f);
        _airborneParticle.Play();
        
        yield return new WaitForSeconds(0.5f);
        _airborneParticle.Stop();
        _airborneParticle.Play();   
        yield return new WaitForSeconds(0.3f);
        _airborneParticle.Pause();
    }
    void MaxLevelAirborne()
    {
        GameObject airborneBoxObj = Instantiate(_airborneBox, transform.position, Quaternion.identity);
        Chise_DamageBox_Airborne airborneBox = airborneBoxObj.GetComponent<Chise_DamageBox_Airborne>();
        float finalDamage = _damage + (_damage * _buffStat._Att);


        bool isCritical = SkillManager.Instance.IsCritical(0f);
        if (isCritical)
        {
            finalDamage *= SkillManager.Instance._BaseCriDmg;
        }
        airborneBox.SetAirborneSetting(_airborneTime, _airborneSpeed);
        airborneBox.UpdateDamage(finalDamage);
        airborneBox.UpdateScale(_airborneScale * (1 + _buffStat._Range));
        airborneBox.StartAirborne();
    }
}
