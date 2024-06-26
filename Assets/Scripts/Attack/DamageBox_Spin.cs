using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Spin : MonoBehaviour
{
    int _damage;
    Vector3 _boxScale = new Vector3(3, 1, 3);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
    public void UpdateDamage(int damage) { _damage = damage; }
    public void UpdateScale(float boxSize) { transform.localScale = _boxScale * boxSize; }
}
