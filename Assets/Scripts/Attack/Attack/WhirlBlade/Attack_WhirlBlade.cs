using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 	
롤아이콘-공격력 신규 피해량: 피해량이 증가합니다.
롤아이콘-스킬가속 스킬가속: 재사용 대기시간이 감소합니다.
롤아이콘-사거리 신규 투사체 개수: 투사체 수가 증가합니다.
롤아이콘-범위크기 신규 범위크기: 투사체 크기가 증가합니다.
롤아이콘-지속시간 신규 지속시간: 지속 시간이 증가합니다.
 * */
public class Attack_WhirlBlade : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_WhirlBlade _damageBox;

    [SerializeField] SkillManager _skillManager;

    List<GameObject> _damageBoxList = new List<GameObject>();

    float _angle;

    float _curCount = 0;
    public bool _IsMaxLevel => _isMaxLevel;
    [Header("발사 위치"), SerializeField] Transform _shootTrs;
    [Header("반지름"), SerializeField] float _radius = 4f;

    [Header("기본 크기"), SerializeField] Vector3 _scale = new Vector3(2f, 2f, 2f);
    private void Awake()
    {
        _isMaxLevel = false;
    }
    private void OnEnable()
    {
        _name = eSKILL.WhirlBlade;
    }
    public override void AttackInteract()
    {
        int finalAttCount = _projectileCount + (int)_buffStat._ProjectileCount;
        _angle = 360 / finalAttCount;

        // If damage boxes already exist and it's max level, just update them
        if (_damageBoxList.Count > 0 && _isMaxLevel)
        {
            UpdateDamageBoxStat(_damageBoxList);
        }
        else
        {
            for (int i = 0; i < finalAttCount; i++)
            {
                GameObject damageBox = Instantiate(_damageBoxObj);
                _damageBoxList.Add(damageBox);
            }
            UpdateDamageBoxStat(_damageBoxList);
        }
    }
    void UpdateDamageBoxStat(List<GameObject> damageBoxList)
    {
        int count = 0;
        foreach (GameObject damageBoxObj in damageBoxList)
        {
            DamageBox_WhirlBlade damageBox = damageBoxObj.GetComponent<DamageBox_WhirlBlade>();

            damageBox.UpdateRadius(_radius);
            float finalDamage = _damage + (_damage * _buffStat._Att);
            damageBox.UpdateDamage(finalDamage);

            float finalRange = 1 + _buffStat._Range;
            damageBox.UpdateScale(_scale * finalRange);

            damageBox.SetTarget(_shootTrs);

            damageBox.SetStartAngle(_angle * count);
            count++;
        }
    }
    void UpdateDamageBox(List<GameObject> damageBoxList)
    {
        if (_curCount != _buffStat._ProjectileCount)
        {
            _curCount = _buffStat._ProjectileCount;
            for (int i = _damageBoxList.Count - 1; i >= 0; i--)
            {
                Destroy(_damageBoxList[i]);
                _damageBoxList.RemoveAt(i);
            }
            int finalAttCount = _projectileCount + (int)_buffStat._ProjectileCount;
            _angle = 360 / finalAttCount;

            for (int i = 0; i < finalAttCount; i++)
            {
                GameObject damageBoxObj = Instantiate(_damageBoxObj);
                _damageBoxList.Add(damageBoxObj);
            }
            for(int i = 0; i < _damageBoxList.Count; i++)
            {
                DamageBox_WhirlBlade damageBox = _damageBoxList[i].GetComponent<DamageBox_WhirlBlade>();

                float finalDamage = _damage + (_damage * _buffStat._Att);
                damageBox.UpdateDamage(finalDamage);

                float finalRange = 1 + _buffStat._Range;
                damageBox.UpdateScale(_scale * finalRange);
                damageBox.SetTarget(transform);

                damageBox.SetStartAngle(_angle * i);
            }
        }
        
        /*
        int count = 0;
        foreach (GameObject damageBoxObj in damageBoxList)
        {
            DamageBox_WhirlBlade damageBox = damageBoxObj.GetComponent<DamageBox_WhirlBlade>();

            float finalDamage = _damage + (_damage * _buffStat._Att);
            damageBox.UpdateDamage(finalDamage);

            float finalRange = 1 + _buffStat._Range;
            damageBox.UpdateScale(finalRange);

            count++;

            
        }
        */
    }
    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _projectileCount = stat._projectileCount;
        
        _coolTime = stat._coolTime;
        _durTime = stat._durTime;
        if (_level >= 6)
        {
            _isMaxLevel = true;
        }
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            if (_level > 0)
                AttackInteract();

            if (!_isMaxLevel)
            {
                yield return new WaitForSeconds(_durTime + _buffStat._Dur);
                foreach (GameObject damageBoxObj in _damageBoxList)
                {
                    Destroy(damageBoxObj);
                }
                _damageBoxList.Clear();
            }
            yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
        }
    }

    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override void StartAttack()
    {
        StartCoroutine(CRT_Attack());
    }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;

        
        if (_damageBoxList.Count > 0)
        {
            // Update the existing damage boxes if they exist
            UpdateDamageBox(_damageBoxList);
        }
    }

    public override void SetPlayerTrs(Transform playerTrs) { }
}