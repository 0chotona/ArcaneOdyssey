using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1. 데미지 (0. 5 1. 7 2. 10)
2. 공격 범위 (0. 1단 1. 2단 2. 3단 + 화상) 
3. 쿨타임 (0. 7초 1. 5.5초 2. 4초)
*/
public class Active_Spin : MonoBehaviour, IActiveAttackable
{
    
    [SerializeField] Transform _damageBoxTrs;
    [SerializeField] GameObject _effect;
    DamageBox_Spin _damageBox;

    bool _canAttack = false;
    [Header("쿨타임"), SerializeField] float _coolTime = 10f;
    private void Awake()
    {
        _damageBox = _damageBoxTrs.GetComponent<DamageBox_Spin>();
        _damageBoxTrs.gameObject.SetActive(false);

    }
    IEnumerator CRT_Attack() 
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

    void SpawnEffect()
    {
        GameObject effect = Instantiate(_effect, transform.position, transform.rotation);
        
        Destroy(effect, 1);
    }


    public void ActiveInteract()
    {
        if(_canAttack)
        {
            SpawnEffect();
            StartCoroutine(CRT_Attack());
            StartCoroutine(CRT_SetCoolTime());

        }
        
    }
}
