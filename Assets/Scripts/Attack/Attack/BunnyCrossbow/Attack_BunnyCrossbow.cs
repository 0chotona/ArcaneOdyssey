using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_BunnyCrossbow : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_BunnyCrossbow _damageBox;

    [Header("하나 당 각도"), SerializeField] float _angle = 10f;


    [Header("거리"), SerializeField] float _distance = 8f;
    [Header("속도"), SerializeField] float _speed = 20f;


    [Header("발사 위치"), SerializeField] Transform _shootTrs;


    private void Awake()
    {
        

        _name = eSKILL.BunnyCrossbow;
    }
    
    public override void AttackInteract()
    {
        float rndAngle = Random.Range(0, 360f);

        float startAngle = -((_projectileCount + _buffStat._ProjectileCount) * _angle * 0.5f) + rndAngle;
        //Quaternion startAngle = Quaternion.Euler(0, startAngle, 0);


        for (int i = 0; i < (_projectileCount + _buffStat._ProjectileCount); i++)
        {
            Vector3 targetPos = _shootTrs.position + Quaternion.Euler(0, startAngle + i * _angle, 0) * _shootTrs.forward * _distance;
            GameObject bullet = Instantiate(_damageBoxObj, _shootTrs.position, _shootTrs.rotation);
            DamageBox_BunnyCrossbow damageBox = bullet.GetComponent<DamageBox_BunnyCrossbow>();

            float finalDamage = _damage + (_damage * _buffStat._Att);


            bool isCritical = SkillManager.Instance.IsCritical(_criRate);
            if (isCritical)
            {
                finalDamage *= SkillManager.Instance._BaseCriDmg;
            }
            damageBox.UpdateDamage(finalDamage);
            damageBox.UpdateSpeed(_speed);
            damageBox.Shot(targetPos);
        }

        
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
            AttackInteract();
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
        _criRate = stat._criRate;
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
