using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Bow : MonoBehaviour
{
    float _damage;
    float _speed = 1000;
    Rigidbody _rigid;

    Vector3 _dir;

    Vector3 _boxScale = Vector3.one;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        Shot();
    }
    public void SetDirection(Vector3 dir)
    {
        _dir = dir;
    }
    void Shot()
    {
        transform.rotation = Quaternion.LookRotation(_dir); 
        _rigid.AddForce(_dir * _speed);
    }
    public void UpdateDamage(float damage) { _damage = damage; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
}
