using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Boomerang : MonoBehaviour
{
    float _damage;
    public float _speed;

    GameObject _tmpObj;

    bool _isMaxLevel = false;

    Vector3 _targetPos;
    [Header("���� �θ޶� ������Ʈ"), SerializeField] GameObject _smallBoomerang;
    float _angle;
    float _smallDistance = 20f;

    float _backDistance;
    Transform _playerTrs;
    public void Shot(Vector3 targetPos)
    {
        _tmpObj = gameObject;
        _targetPos = targetPos;
        transform.rotation = Quaternion.LookRotation(_targetPos - transform.position);
        StartCoroutine(CRT_MoveBullet());
    }
    IEnumerator CRT_MoveBullet()
    {
        while (Vector3.Distance(transform.position, _targetPos) > 0.05f)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _targetPos);

            float speedModifier = Mathf.Clamp(distanceToTarget / 10.0f, 0.1f, 1.0f); // �Ÿ� ��� �ӵ� ����
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * speedModifier * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(CRT_BackBullet());
    }
    IEnumerator CRT_BackBullet()
    {
        Vector3 dir = (_playerTrs.position - transform.position).normalized;
        Vector3 targetPos = transform.position + dir * _backDistance;

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPos);

            float speedModifier = Mathf.Clamp(1.0f - (distanceToPlayer / _backDistance), 0.1f, 1.0f);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * (1.0f + speedModifier) * Time.deltaTime);
            yield return null;
        }

        if (_isMaxLevel)
        {
            Vector3 finalDir = (targetPos - transform.position).normalized;

            Quaternion dir1 = Quaternion.Euler(0f, _angle, 0f);
            GameObject boomerang1 = Instantiate(_smallBoomerang, transform.position, Quaternion.identity);
            DamageBox_Boomerang_L damageBox1 = boomerang1.GetComponent<DamageBox_Boomerang_L>();
            damageBox1.UpdateDamage(_damage);
            damageBox1.UpdateSpeed(_speed);
            damageBox1.Shot(transform.position + dir1 * dir * _smallDistance);

            Quaternion dir2 = Quaternion.Euler(0f, -_angle, 0f);
            GameObject boomerang2 = Instantiate(_smallBoomerang, transform.position, Quaternion.identity);
            DamageBox_Boomerang_L damageBox2 = boomerang2.GetComponent<DamageBox_Boomerang_L>();
            damageBox2.UpdateDamage(_damage);
            damageBox2.UpdateSpeed(_speed);
            damageBox2.Shot(transform.position + dir2 * dir * _smallDistance);
        }

        Destroy(_tmpObj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
            
        }
    }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateIsMaxLevel(bool isMaxLevel) { _isMaxLevel = isMaxLevel; }
    public void SetAngle(float angle) { _angle = angle; }
    public void SetPlayerTrs(Transform playerTrs) { _playerTrs = playerTrs; }
    public void SetDistance(float distance) { _backDistance = distance; }
    public void SetSpeed(float speed) { _speed = speed; }
    public void SetSmallDistance(float distance) { _smallDistance = distance; }
    
}
