using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Boomerang : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Boomerang _damageBox;

    [SerializeField] SkillManager _skillManager;

    private void OnEnable()
    {
        _name = eSKILL.Boomerang;
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

    
    public override void AttackInteract()
    {
        for (int i = 0; i < _attCount; i++)
        {
            GameObject damageBox = Instantiate(_damageBoxObj, transform.position, Quaternion.identity);

            _damageBox = damageBox.GetComponent<DamageBox_Boomerang>();

            _damageBox.UpdateScale(_attRange);
            _damageBox.UpdateDamage(_damage);
            _damageBox.SetPlayerTrs(transform);
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
