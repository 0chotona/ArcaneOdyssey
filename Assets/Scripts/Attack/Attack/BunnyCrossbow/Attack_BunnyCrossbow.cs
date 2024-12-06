using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_BunnyCrossbow : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_BunnyCrossbow _damageBox;

    [Header("�ϳ� �� ����"), SerializeField] float _angle = 10f;


    [Header("�Ÿ�"), SerializeField] float _distance = 8f;
    [Header("�ӵ�"), SerializeField] float _speed = 20f;

    [Header("���� ������"), SerializeField] float _soundDelay = 0.3f;

    [Header("�߻� ��ġ"), SerializeField] Transform _shootTrs;


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
            yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime - _soundDelay);
            SoundManager.Instance.PlaySound(eSKILLSOUNDTYPE.BunnyCrossbow);
            yield return new WaitForSeconds(_soundDelay);
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
    public override void SetPlayerTrs(Transform playerTrs) { }
}
