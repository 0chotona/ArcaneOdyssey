using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageBox_FootprintTrap : MonoBehaviour
{
    float _damage;
    Vector3 _boxScale = Vector3.one;
    bool _isMaxLevel = false;

    float _durTime = 0f;

    Attack_FootprintTrap _attack;
    public IObjectPool<GameObject> _pool;
    [SerializeField] Collider _collider;
    [SerializeField] ParticleSystem _particle;
    
    public void UpdateScale(Vector3 scale)
    {
        _boxScale = scale;
        transform.localScale = _boxScale;
    }
    public void SetDamageBox(float durTime)
    {
        _durTime = durTime;
        StartCoroutine(CRT_SetDamageBox(durTime));
    }
    public void SetAttack(Attack_FootprintTrap attack)
    {
        _attack = attack;
    }
    IEnumerator CRT_SetDamageBox(float durTime)
    {
        _particle.Play(true);

        yield return new WaitForSeconds(durTime);
        _particle.Stop(true);
        _pool.Release(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _attack.SetDamageEnemyList(other.transform, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _attack.SetDamageEnemyList(other.transform, false);
        }
    }
}
