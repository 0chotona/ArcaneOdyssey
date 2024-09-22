using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Boomerang : MonoBehaviour
{
    float _angle = 0;
    float _radius = 4;

    float _damage;
    Vector3 _boxScale = new Vector3(1, 1, 1);

    float _moveSpeed = 1;

    Transform _playerTrs;

    Vector3 _finalPos;
    Vector3 _targetPos;

    float _distance = 5;

    float _stopSec = 2;

    bool _isForward = true;

    GameObject _tmpObj;
    private void Awake()
    {
        _tmpObj = gameObject;

        //_startPos = _playerTrs.position;
        _angle = Random.Range(0, 360);



    }
    private void Start()
    {
        _finalPos = GetRandomAngle(_angle) * _distance + _playerTrs.position;
        StartCoroutine(CRT_Move());
        //Destroy(_tmpObj, _stopSec + 2);
    }
    private void Update()
    {
        MoveBoomerang();

    }
    Vector3 GetRandomAngle(float angle)
    {
        _angle *= Mathf.Deg2Rad;
        float x = Mathf.Sin(_angle);
        float z = Mathf.Cos(_angle);
        Vector3 dir = new Vector3(x, 0, z).normalized;
        return dir;
    }
    void MoveBoomerang()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.timeScale);
    }
    IEnumerator CRT_Move()
    {
        _isForward = true;
        _targetPos = _finalPos;

        yield return new WaitForSeconds(_stopSec);
        _targetPos = _playerTrs.position;
        _isForward = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
        if (!_isForward && other.CompareTag("Player"))
            Destroy(_tmpObj);
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(float boxSize) { transform.localScale = _boxScale * boxSize; }
}
