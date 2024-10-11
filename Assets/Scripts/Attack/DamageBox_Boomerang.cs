using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Boomerang : MonoBehaviour
{
    float _damage;
    public float _speed;


    Vector3 _boxScale = Vector3.one;

    GameObject _tmpObj;

    bool _isMaxLevel = false;

    Vector3 _targetPos;
    [Header("작은 부메랑 오브젝트"), SerializeField] GameObject _smallBoomerang;
    float _angle;

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
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
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
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
            yield return null;
        }
        if(_isMaxLevel)
        {
            //갈라지기 구현
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
}
