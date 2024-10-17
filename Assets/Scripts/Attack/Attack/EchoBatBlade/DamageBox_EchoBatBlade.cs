using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageBox_EchoBatBlade : MonoBehaviour
{
    float _damage;
    public float _speed;

    Vector3 _startPos = Vector3.zero;

    bool _isMaxLevel = false;
    GameObject _tmpObj;
    float _distance = 0f;

    Vector3 _targetPos;
    public void UpdateIsMaxLevel(bool isMaxLevel) { _isMaxLevel = isMaxLevel; }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateSpeed(float speed) { _speed = speed; }
    public void Shot(Vector3 targetPos)
    {
        _tmpObj = gameObject;

        _startPos = transform.position;
        _distance = Vector3.Distance(_startPos, targetPos);

        _targetPos = targetPos;
        transform.rotation = Quaternion.LookRotation(_targetPos - transform.position);
        StartCoroutine(CRT_MoveBullet());
    }
    IEnumerator CRT_MoveBullet()
    {
        while (Vector3.Distance(transform.position, _targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
            yield return null;
        }
        Destroy(_tmpObj);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            float rndAngle = Random.Range(0, 360f);
            float distance = Vector3.Distance(_startPos, transform.position);
            _distance -= distance;

            Vector3 targetPos = transform.position + Quaternion.Euler(0, rndAngle, 0) * transform.forward * _distance;
            _targetPos = targetPos;
            transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
        }
    }
}