using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1. 데미지 (0. 5 1. 7 2. 10)
2. 공격횟수 (0. 1번 공격 1. 2번 공격 2. 파이널 어택)
3. 공격 범위 (0. 1단 1. 2단 2. 검기)
4. 쿨타임 (0. 2초 1. 1.5초 2. 1초)
*/
public class Attack_Sword : AttackController
{
    [SerializeField] Transform _damageBoxTrs;
    DamageBox_Sword _damageBox;

    Coroutine _crt_Attack;

    int _finalDamage = 5;

    Skill _skill;

    Vector3 _boxScale = new Vector3(3, 0.5f, 3);
    enum eSTATE
    {
        Attack,
        FinalAttack
    }

    eSTATE _eState;

    [SerializeField] AnimController _anim;
    private void Awake()
    {

        _damageBox = _damageBoxTrs.GetComponent<DamageBox_Sword>();
        _name = "기본 베기";

    }
    private void Start()
    {
        //UpdateStat();
        StartCoroutine(CRT_Attack());
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            _skillManager.UpgradeLevel("기본 베기");
            UpdateStat();
        }
        UpdateStat();
        */
    }
    public override void Attack()
    {
        _damageBoxTrs.localScale = _boxScale * _attRange;
        SpawnEffect(eSTATE.Attack);

    }
    public override void SetSkill(Skill skill)
    {
        _skill = skill;
        UpdateStat(_skill);
    }
    void FinalAttack()
    {
        _damageBoxTrs.localScale = new Vector3(4, _boxScale.y, _boxScale.z * 3);
        SpawnEffect(eSTATE.FinalAttack);
        //transform.rotation = Quaternion.Euler(transform.forward);
        /*
        if (_nearEnemies.Count > 0)
        {
            Transform targetEnemy = GetNearestEnemy();
            if (targetEnemy != null)
            {
                Vector3 direction = targetEnemy.position - transform.position;
                direction.y = 0f;

                float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
                transform.rotation = targetRotation;

            }
        }
        */
    }
        
    public override IEnumerator CRT_Attack()
    {
        _canAttack = true;
        while (true)
        {
            float coolTime = _coolTime - _attCount * 0.25f;

            yield return new WaitForSeconds(coolTime);

            

            

            for (int i = 0; i < _attCount; i++)
            {
                _damageBoxTrs.gameObject.SetActive(true);
                _damageBox.UpdateDamage(_damage);
                Attack();

                _anim.SetAttackAnimation(_level);

                yield return new WaitForSeconds(0.05f);

                _damageBoxTrs.gameObject.SetActive(false);

                yield return new WaitForSeconds(0.2f);
            }
            if(_eState == eSTATE.FinalAttack)
            {

                
                //yield return new WaitForSeconds(0.4f);

                _damageBoxTrs.gameObject.SetActive(true);
                _damageBox.UpdateDamage(_damage + _finalDamage);
                FinalAttack();

                yield return new WaitForSeconds(0.05f);

                _damageBoxTrs.gameObject.SetActive(false);
            }
        }
        
    }
    
    void SpawnEffect(eSTATE eState)
    {
        GameObject effect = (eState == eSTATE.Attack) ? 
            Instantiate(_effect, _damageBoxTrs.position, transform.rotation,transform) : 
            Instantiate(_finalEffect, transform.position, transform.rotation, transform);
        if (eState == eSTATE.FinalAttack)
            effect.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
        effect.transform.localScale = Vector3.one * _attRange;

        
        Destroy(effect, 1);
    }
    public override void UpdateStat(Skill skill)
    {
        _level = skill._level;
        switch (_level)
        {
            case 1:
                _eState = eSTATE.Attack;
                _damage = 5;
                _attCount = 1;
                _attRange = 0.8f;
                _coolTime = 2f;
                break;
            case 2:
                _damage = 7;
                _attCount = 1;
                _attRange = 0.8f;
                _coolTime = 2f;
                break;
            case 3:
                _damage = 7;
                _attCount = 2;
                _attRange = 0.8f;
                _coolTime = 2f;
                break;
            case 4:
                _damage = 9;
                _attCount = 2;
                _attRange = 1;
                _coolTime = 1.5f;
                break;
            case 5:
                _eState = eSTATE.FinalAttack;
                _damage = 9;
                _attCount = 2;
                _attRange = 1;
                _coolTime = 1.5f;
                break;
            case 6:
                _damage = 11;
                _attCount = 2;
                _attRange = 1.2f;
                _coolTime = 1.5f;
                break;
        }
        skill.UpdateStat(_level, _damage, _attCount, _attRange, _coolTime, _durTime, _shotSpeed);
        _damageBox.UpdateDamage(_damage);
    }

}
