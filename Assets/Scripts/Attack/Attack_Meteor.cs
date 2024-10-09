using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Meteor : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Meteor _damageBox;


    [SerializeField] SkillManager _skillManager;

    private void OnEnable()
    {

        StartCoroutine(CRT_Attack());

        _name = eSKILL.Meteor;
    }
    public override void AttackInteract()
    {
        for (int i = 0; i < _attCount; i++)
        {

            GameObject damageBox = Instantiate(_damageBoxObj, transform.position + GetRandomPos(), Quaternion.identity);

            _damageBox = damageBox.GetComponent<DamageBox_Meteor>();

            _damageBox.UpdateScale(_attRange);
            _damageBox.UpdateDamage(_damage);
        }



    }
    Vector3 GetRandomPos()
    {
        int rndDist = Random.Range(0, 10);
        int rndAngle = Random.Range(0, 360);

        Vector3 rndPos = new Vector3(Mathf.Cos(rndAngle * Mathf.Deg2Rad) * rndDist, 15, Mathf.Sin(rndAngle * Mathf.Deg2Rad) * rndDist);
        return rndPos;
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
    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attCount = stat._attCount;
        _attRange = stat._attRange;
        _coolTime = stat._coolTime;

    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime - _coolTime * BuffStat.Instance._CoolTimeBuff);
            if (_level > 0)
                AttackInteract();
        }
    }
}
