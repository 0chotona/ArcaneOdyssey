using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Explosion : MonoBehaviour
{
    float _baseDamage;
    GameObject _tmpObj;

    Collider _collider;

    ParticleSystem[] _particles;

    float _hpPercent;
    float _dmgIncrease;

    public void ShowParticle()
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _particles)
            particle.Play();
    }
    public void Explode()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = true;
        StartCoroutine(CRT_ColliderOff());
    }
    IEnumerator CRT_ColliderOff()
    {
        yield return new WaitForSeconds(0.05f);
        _collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            float percent = enemyHealth.GetHpPercent(); //적 현재 체력
            SetDamageByHp(percent);

            float damage = _baseDamage * _dmgIncrease;
            damage = Mathf.Floor(damage * 10f) / 10f; //소수점 한자리 빼고 버림
            enemyHealth.LoseDamage(damage);
        }
    }
    public void DestroyObj(float time)
    {
        _tmpObj = gameObject;
        Destroy(_tmpObj, time);

    }
    public void UpdateDamage(float damage) { _baseDamage = damage; }
    public void UpdateHpPercent(float percent) { _hpPercent = percent; }
    void SetDamageByHp(float percent)
    {
        float hpPercent = percent;
        if (hpPercent < 30)
            hpPercent = 30;
        _dmgIncrease = (100 - hpPercent) / (_hpPercent / 2) + 1; //0 ~ 70, 0 - 1 / 70 - 3
        
    }
}
