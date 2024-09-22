using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Meteor : MonoBehaviour
{

    float _damage;
    Vector3 _boxScale = new Vector3(1, 1, 1);
    GameObject _tmpObj;

    bool _isGrounded = false;

    [SerializeField] GameObject _explodeObj;
    private void Awake()
    {
        _tmpObj = gameObject;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isGrounded)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                enemyHealth.LoseDamage(_damage);
            }
        }

        if (other.CompareTag("Ground"))
        {
            _isGrounded = true;

            GameObject explode = Instantiate(_explodeObj, transform.position, Quaternion.identity);
            Destroy(explode, 1f);
            Destroy(_tmpObj, 1.1f);


        }
    }

    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(float boxSize) { transform.localScale = _boxScale * boxSize; }

}
