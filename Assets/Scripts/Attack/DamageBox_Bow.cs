using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Bow : MonoBehaviour
{
    int _damage;
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
    public void UpdateDamage(int damage) { _damage = damage; }
    public void UpdateSpeed(float speed) { _speed *= speed; }
    public void UpdateScale(float boxSize) { transform.localScale = _boxScale * boxSize; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
}
