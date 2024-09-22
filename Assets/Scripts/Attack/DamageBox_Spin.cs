using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Spin : MonoBehaviour
{
    float _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
    public void UpdateDamage(float damage) { _damage = damage; }
}
