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
    [Header("데미지"), SerializeField] float _damage = 10f;
    [Header("이펙트"), SerializeField] ParticleSystem _effectParticle;
    ParticleSystem[] _effectParticles;
    private void Awake()
    {
        _damageBox = _damageBoxTrs.GetComponent<DamageBox_Spin>();
        _damageBoxTrs.gameObject.SetActive(false);
        _effectParticles = _effectParticle.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _effectParticles)
            particle.Stop();
        StartCoroutine(CRT_SetCoolTime());

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
        yield return new WaitForSeconds(_coolTime);
        
        _canAttack = true;
    }


    public void ActiveInteract()
    {
        if(_canAttack)
        {
            _damageBox.UpdateDamage(_damage);
            ShowParticle();
            StartCoroutine(CRT_Attack());
            StartCoroutine(CRT_SetCoolTime());

        }
        
    }
    void ShowParticle()
    {
        foreach (ParticleSystem particle in _effectParticles)
            particle.Play();
    }
    public void SetPlayerTrs(Transform playerTrs) { }

    public void SetBuffStat(CBuffStat buffStat)
    {
        throw new System.NotImplementedException();
    }
}
