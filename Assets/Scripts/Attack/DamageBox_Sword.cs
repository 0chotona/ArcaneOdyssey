using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Sword : MonoBehaviour
{
    Collider _collider;
    List<EnemyHealth> _enemyHealths;
    float _damage;
    float _range;
    int _count;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _enemyHealths = new List<EnemyHealth>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public List<EnemyHealth> GetEnemy()
    {
        _collider.enabled = true;
        return _enemyHealths;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _enemyHealths.Clear();
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            _enemyHealths.Add(enemyHealth);
            GiveDamage();
            //_collider.enabled = false;
        }
        
    }
    void GiveDamage()
    {
        foreach (EnemyHealth enemyHealth in _enemyHealths)
            enemyHealth.LoseDamage(_damage);
    }
    public void UpdateDamage(float damage)
    {
        _damage = damage;

    }

}
