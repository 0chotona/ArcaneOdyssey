using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_SpinBall : AttackController
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_SpinBall _damageBox;

    [SerializeField] SkillManager _skillManager;

    Skill _skill;
    private void Start()
    {
        StartCoroutine(CRT_Attack());
        _name = "회전구";
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _skillManager.UpgradeLevel("회전구");
            UpdateStat();
        }
        UpdateStat();
        */
    }
    public override void Attack()
    {
        float angle = 360 / _attCount;
        for(int i = 0; i < _attCount; i++)
        {
            GameObject damageBox = Instantiate(_damageBoxObj);
            _damageBox = damageBox.GetComponent<DamageBox_SpinBall>();

            _damageBox.SetStartAngle(angle * i);

            _damageBox.UpdateScale(_attRange);
            _damageBox.UpdateDamage(_damage);
            _damageBox.UpdateSpeed(_shotSpeed);
            //UpdateStat();
            _damageBox.SetTarget(transform);
            Destroy(damageBox, _durTime);
        }
        
        
    }
    public override void UpdateStat(Skill skill)
    {
        _level = skill._level;
        switch (_level)
        {
            case 1:
                _damage = 4;
                _attCount = 2;
                _durTime = 1;
                _attRange = 0.7f;
                _coolTime = 5f;
                _shotSpeed = 1f;
                break;
            case 2:
                _damage = 5;
                _durTime = 1f;
                _attRange = 0.7f;
                _coolTime = 5f;
                _shotSpeed = 1f;
                break;
            case 3:
                _damage = 5;
                _durTime = 1.5f;
                _attRange = 1f;
                _coolTime = 5f;
                _shotSpeed = 1f;
                break;
            case 4:
                _damage = 6;
                _attCount = 3;
                _durTime = 1.5f;
                _attRange = 1f;
                _coolTime = 4f;
                _shotSpeed = 1.3f;
                break;
            case 5:
                _damage = 6;
                _durTime = 2f;
                _attRange = 1.3f;
                _coolTime = 4f;
                _shotSpeed = 1.3f;
                break;
            case 6:
                _damage = 8;
                _attCount = 6;
                _durTime = 2f;
                _attRange = 1.3f;
                _coolTime = 3f;
                _shotSpeed = 1.6f;
                break;
        }
        skill.UpdateStat(_level, _damage, _attCount, _attRange, _coolTime, _durTime, _shotSpeed);
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime);
            if(_level > 0)
                Attack();
        }
    }

    public override void SetSkill(Skill skill)
    {
        _skill = skill;
    }
}
