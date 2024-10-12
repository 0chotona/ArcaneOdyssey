using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float _damage;
    GameObject _tmpObj;

    Collider _collider;

    ParticleSystem[] _particles;
    
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
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
    public void DestroyObj(float time)
    {
        _tmpObj = gameObject;
        Destroy(_tmpObj, time);
        
    }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(float scale)
    {
        Vector3 scaleVec = new Vector3(scale, scale, scale);
        transform.localScale = scaleVec;
    }
}
