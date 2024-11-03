using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Momoi_DamageBox_Attack : MonoBehaviour
{
    float _damage;
    float _piercedDamage;
    public float _speed;


    Vector3 _boxScale = Vector3.one;

    GameObject _tmpObj;

    bool _isMaxLevel = false;

    Vector3 _targetPos;

    [SerializeField] ParticleSystem _particle;
    private void Awake()
    {
        _particle.Simulate(0.45f, true, true, false); // 0.5초 시점까지 시뮬레이션하고 일시정지
        _particle.Pause();
    }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdatePierceDamage(float pierceDamage) { _piercedDamage = pierceDamage; }
    public void UpdateSpeed(float speed) { _speed = speed; }
    public void UpdateIsMaxLevel(bool isMaxLevel) { _isMaxLevel = isMaxLevel; }
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
        Destroy(_tmpObj);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
            if (!_isMaxLevel)
                Destroy(_tmpObj);
            else
                _damage = _piercedDamage;
        }
    }
}
