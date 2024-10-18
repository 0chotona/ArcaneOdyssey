using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 메아리치는 박쥐칼날 피해량, 쿨타임, 치명타, 투사체 EchoBatBlade 
피해량 35 / 55 / 75 / 95 / 115
투사체 4 / 5 / 5 / 6 / 6
쿨타임 3 / 3 / 2.25 / 2.25 / 1.5
*/
public class Attack_EchoBatBlade : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_EchoBatBlade _damageBox;


    [Header("거리"), SerializeField] float _distance = 8f;
    [Header("속도"), SerializeField] float _speed = 20f;

    [Header("발사 간격"), SerializeField] float _gap = 0.1f;


    [Header("발사 위치"), SerializeField] Transform _shootTrs;

    [Header("충돌 당 증가 비율"), SerializeField] float _increaseRate = 1.5f;

    private void Awake()
    {


        _name = eSKILL.EchoBatBlade;
    }

    public override void AttackInteract()
    {
        float rndAngle = Random.Range(0, 360f);
        Vector3 targetPos = _shootTrs.position + Quaternion.Euler(0, rndAngle, 0) * _shootTrs.forward * _distance;
        GameObject bullet = Instantiate(_damageBoxObj, _shootTrs.position, _shootTrs.rotation);
        DamageBox_EchoBatBlade damageBox = bullet.GetComponent<DamageBox_EchoBatBlade>();

        float finalDamage = _damage + (_damage * _buffStat._Att);


        bool isCritical = SkillManager.Instance.IsCritical(_criRate);
        if (isCritical)
        {
            finalDamage *= SkillManager.Instance._BaseCriDmg;
        }
        damageBox.UpdateDamage(finalDamage);
        damageBox.UpdateSpeed(_speed);
        damageBox.SetIncreaseRate(_increaseRate);
        damageBox.Shot(targetPos);


    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
            
            for (int i = 0; i < (_projectileCount + _buffStat._ProjectileCount); i++)
            {
                AttackInteract();
                yield return new WaitForSeconds(_gap);
            }
            
        }
    }

    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _coolTime = stat._coolTime;
        _projectileCount = stat._projectileCount;


    }

    public override void StartAttack()
    {
        StartCoroutine(CRT_Attack());
    }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }
}
