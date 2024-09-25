using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Explosion : MonoBehaviour
{
    float _damage;
    GameObject _tmpObj;

    Collider _collider;

    ParticleSystem[] _particles;
    private void Awake()
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _particles)
            particle.Play();

        _tmpObj = gameObject;
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
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
    public void DestroyObj(float time)
    {
        Destroy(_tmpObj, time);
        
    }
    public void UpdateDamage(float damage) { _damage = damage; }
}
