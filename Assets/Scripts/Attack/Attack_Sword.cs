using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1. 데미지 (0. 5 1. 7 2. 10)
2. 공격횟수 (0. 1번 공격 1. 2번 공격 2. 파이널 어택)
3. 공격 범위 (0. 1단 1. 2단 2. 검기)
4. 쿨타임 (0. 2초 1. 1.5초 2. 1초)
*/
public class Attack_Sword : Attack
{
    [SerializeField] Transform _damageBoxTrs;
    DamageBox_Sword _damageBox;

    Coroutine _crt_Attack;

    int _finalDamage = 5;


    Vector3 _boxScale = new Vector3(3, 0.5f, 3);
    enum eSTATE
    {
        Attack,
        FinalAttack
    }

    eSTATE _eState;

    //[SerializeField] AnimController _anim;
    private void OnEnable()
    {
        //_name = eSKILL.Slash;
    }
    private void Awake()
    {
        _damageBox = _damageBoxTrs.GetComponent<DamageBox_Sword>();

    }
    private void Start()
    {
        //UpdateStat();
        StartCoroutine(CRT_Attack());
    }
    public override void AttackInteract()
    {
        _damageBoxTrs.localScale = _boxScale * _attRange;
        SpawnEffect(eSTATE.Attack);

    }
    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }
    void FinalAttack()
    {
        _damageBoxTrs.localScale = new Vector3(4, _boxScale.y, _boxScale.z * 3);
        SpawnEffect(eSTATE.FinalAttack);
    }
        
    public override IEnumerator CRT_Attack()
    {
        _canAttack = true;
        while (true)
        {
            float coolTime = _coolTime - _projectileCount * 0.25f;

            yield return new WaitForSeconds(coolTime);

            

            

            for (int i = 0; i < _projectileCount; i++)
            {
                _damageBoxTrs.gameObject.SetActive(true);
                _damageBox.UpdateDamage(_damage);
                AttackInteract();

                //_anim.SetAttackAnimation(_level);

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
    public override void SetPlayerTrs(Transform playerTrs) { }
    void SpawnEffect(eSTATE eState)
    {
        /*
        GameObject effect = (eState == eSTATE.Attack) ? 
            Instantiate(_effect, _damageBoxTrs.position, transform.rotation,transform) : 
            Instantiate(_finalEffect, transform.position, transform.rotation, transform);
        if (eState == eSTATE.FinalAttack)
            effect.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
        effect.transform.localScale = Vector3.one * _attRange;

        
        Destroy(effect, 1);
        */
    }
    public override void UpdateStat(CStat stat)
    {
        if (_level < 5)
            _eState = eSTATE.Attack;
        else
            _eState = eSTATE.FinalAttack;

        _damage = stat._damage;
        _projectileCount = stat._projectileCount;
        _attRange = stat._attRange;
        _coolTime = stat._coolTime;
        
        _damageBox.UpdateDamage(_damage);
    }

    public override void StartAttack() { return; }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        throw new System.NotImplementedException();
    }
}
