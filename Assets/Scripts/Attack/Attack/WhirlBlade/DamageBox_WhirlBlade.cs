using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_WhirlBlade : MonoBehaviour
{
    public float _angle = 0;
    public float _startAngle = 0;
    float _radius = 4;

    float _damage;
    Vector3 _boxScale = new Vector3(1, 1, 1);

    public float _speed = 400;

    public Transform _playerTrs;
    private void Awake()
    {
        
    }

    private void Update()
    {
        MoveBoomerang();
    }
    void MoveBoomerang()
    {
        _angle += Time.deltaTime * _speed;
        if (_angle < 360)
        {
            var rad = Mathf.Deg2Rad * _angle;
            var x = _radius * Mathf.Sin(rad);
            var z = _radius * Mathf.Cos(rad);

            transform.position = _playerTrs.position + new Vector3(x, 0, z);
        }
        else
        {
            _angle = 0;
        }
    }
    public void SetStartAngle(float angle)
    {
        _startAngle = angle;
        _angle = _startAngle;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
    public void UpdateRadius(float radius)
    {
        _radius = radius;
    }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(Vector3 boxSize) { transform.localScale = boxSize; }
    public void SetTarget(Transform target)
    {
        _playerTrs = target;
    }
}
