using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/*
 * 1. 공격횟수 (0. 한바퀴 1. 두바퀴 2. 세바퀴)
2. 공격범위 (0. 1단 1. 2단 2. 3단 + 스턴)
3. 쿨타임 (0. 5초 1. 4초 2. 3초)
*/
public class Attack_Boomerang : AttackController
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Boomerang _damageBox;

    Skill _skill;

    private void Start()
    {
        StartCoroutine(CRT_Attack());

        _name = "부메랑";
    }
    
    public override void Attack()
    {
        for(int i = 0; i < _attCount; i++)
        {
            GameObject damageBox = Instantiate(_damageBoxObj,transform.position, Quaternion.identity);

            _damageBox = damageBox.GetComponent<DamageBox_Boomerang>();

            _damageBox.UpdateScale(_attRange);
            _damageBox.UpdateDamage(_damage);
            _damageBox.SetPlayerTrs(transform);
        }
        
        //UpdateStat();
        
        
    }
    public override void UpdateStat(Skill skill)
    {
        _level = skill._level;
        switch (_level) 
        {
            case 1:
                _damage = 5;
                _attCount = 1;
                _attRange = 0.8f;
                _coolTime = 5f;
                break;
            case 2:
                _damage = 6;
                _attCount = 1;
                _attRange = 0.8f;
                _coolTime = 5f;
                break;
            case 3:
                _damage = 7;
                _attCount = 2;
                _attRange = 0.8f;
                _coolTime = 5f;
                break;
            case 4:
                _damage = 9;
                _attCount = 2;
                _attRange = 1f;
                _coolTime = 4f;
                break;
            case 5:
                _damage = 9;
                _attCount = 3;
                _attRange = 1f;
                _coolTime = 4f;
                break;
            case 6:
                _damage = 8;
                _attCount = 5;
                _attRange = 1.2f;
                _coolTime = 3f;
                break;
        }
        skill.UpdateStat(_level, _damage, _attCount, _attRange, _coolTime, _durTime, _shotSpeed);
    }

    public override IEnumerator CRT_Attack()
    {
        while(true)
        {
            yield return new WaitForSeconds(_coolTime);
            Attack();
        }
    }

    public override void SetSkill(Skill skill)
    {
        _skill = skill;
    }
}
