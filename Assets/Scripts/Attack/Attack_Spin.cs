using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1. 데미지 (0. 5 1. 7 2. 10)
2. 공격 범위 (0. 1단 1. 2단 2. 3단 + 화상) 
3. 쿨타임 (0. 7초 1. 5.5초 2. 4초)
*/
public class Attack_Spin : AttackController
{
    
    [SerializeField] Transform _damageBoxTrs;
    DamageBox_Spin _damageBox;


    Skill _skill;
    private void Awake()
    {
        _canAttack = true;
        _damageBox = _damageBoxTrs.GetComponent<DamageBox_Spin>();
        _damageBoxTrs.gameObject.SetActive(false);

        _name = "회전 베기";
    }
    public override void Attack()
    {
        if(_canAttack)
        {
            _damageBox.UpdateScale(_attRange);
            SpawnEffect();
            StartCoroutine(CRT_Attack());
            StartCoroutine(CRT_SetCoolTime());
        }
    }

    public override IEnumerator CRT_Attack() 
    {
        _damageBoxTrs.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        _damageBoxTrs.gameObject.SetActive(false);
    }
    IEnumerator CRT_SetCoolTime()
    {
        _canAttack = false;
        float curTime = 0f;
        while(curTime < _coolTime)
        {
            curTime += Time.deltaTime;
            yield return null;
        }
        
        _canAttack = true;
    }//일시정지시 쿨타임 정지 https://www.inflearn.com/questions/1005470/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%BF%A8%ED%83%80%EC%9E%84-%EC%BD%94%EB%A3%A8%ED%8B%B4%ED%99%9C%EC%9A%A9-%EC%A7%88%EB%AC%B8

    public override void UpdateStat(Skill skill)
    {
        _level = skill._level;
        switch (_level)
        {
            case 1:
                _damage = 5;
                _attRange = 1f;
                _coolTime = 9f;
                break;
            case 2:
                _damage = 7;
                _attRange = 1f;
                _coolTime = 9f;
                break;
            case 3:
                _damage = 7;
                _attRange = 1.5f;
                _coolTime = 9f;
                break;
            case 4:
                _damage = 7;
                _attRange = 1.5f;
                _coolTime = 7f;
                break;
            case 5:
                _damage = 10;
                _attRange = 2f;
                _coolTime = 7f;
                break;
            case 6:
                _damage = 15;
                _attRange = 2.5f;
                _coolTime = 5f;
                break;
        }
        skill.UpdateStat(_level, _damage, _attCount, _attRange, _coolTime, _durTime, _shotSpeed);
        _damageBox.UpdateDamage(_damage);
    }
    void SpawnEffect()
    {
        GameObject effect = Instantiate(_effect, transform.position, transform.rotation);
        
        effect.transform.localScale = Vector3.one * _attRange;
        Destroy(effect, 1);
    }

    public override void SetSkill(Skill skill)
    {
        _skill = skill;
    }
}
