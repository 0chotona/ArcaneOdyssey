using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_SpinBall : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_SpinBall _damageBox;

    [SerializeField] SkillManager _skillManager;


    private void OnEnable()
    {
        _name = eSKILL.SpinBall;
    }
    public override void AttackInteract()
    {
        float angle = 360 / _attCount;
        for(int i = 0; i < _attCount; i++)
        {
            GameObject damageBox = Instantiate(_damageBoxObj);
            _damageBox = damageBox.GetComponent<DamageBox_SpinBall>();

            _damageBox.UpdateDamage(_damage);
            _damageBox.SetStartAngle(angle * i);

            _damageBox.UpdateScale(_attRange);
            _damageBox.UpdateDamage(_damage);
            _damageBox.UpdateSpeed(_shotSpeed);
            //UpdateStat();
            _damageBox.SetTarget(transform);
            Destroy(damageBox, _durTime);
        }
        
        
    }
    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attCount = stat._attCount;
        _attRange = stat._attRange;
        _coolTime = stat._coolTime;
        _durTime = stat._durTime;
        _shotSpeed = stat._shotSpeed;


    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime - _coolTime * BuffStat.Instance._CoolTimeBuff);
            if(_level > 0)
                AttackInteract();
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
}
