using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Boomerang_L : MonoBehaviour
{
    float _damage;
    float _speed;


    GameObject _tmpObj;

    Vector3 _targetPos;

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
        Destroy(_tmpObj);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage * 0.6f);
        }
    }
}
