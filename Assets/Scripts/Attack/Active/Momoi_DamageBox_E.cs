using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_DamageBox_E : MonoBehaviour
{
    float _damage;
    public float _speed;


    Vector3 _boxScale = Vector3.one;

    GameObject _tmpObj;

    bool _isMaxLevel = false;

    Vector3 _targetPos;
    [Header("폭발 오브젝트"), SerializeField] GameObject _explodeObj;

    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateSpeed(float speed) { _speed = speed; }
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
        SpawnExplode();
        Destroy(_tmpObj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
            SpawnExplode();
            Destroy(_tmpObj);
        }
    }
    void SpawnExplode()
    {
        GameObject explodeObj = Instantiate(_explodeObj, transform.position, Quaternion.identity);
        Explosion explosion = explodeObj.GetComponent<Explosion>();
        explosion.UpdateDamage(_damage);
        explosion.Explode();
        explosion.DestroyObj(1f);
    }
}
