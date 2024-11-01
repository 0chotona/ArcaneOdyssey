using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chise_DamageBox_Attack : MonoBehaviour
{
    float _damage;
    Vector3 _boxScale = Vector3.one;
    bool _isMaxLevel = false;

    int _skillCombo;

    float _airborneTime = 0f;
    float _airborneSpeed = 0f;
    Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    public void SetAirborneSetting(float time, float speed)
    {
        _airborneTime = time;
        _airborneSpeed = speed;
    }
    public void UpdateCombo(int combo) { _skillCombo = combo; }
    public void UpdateIsMaxLevel(bool isMaxLevel) { _isMaxLevel = isMaxLevel; }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(Vector3 scale) 
    { 
        _boxScale = scale;
        transform.localScale = _boxScale;
    }
    public void UpdateCollider(bool isActive)
    {
        _collider.enabled = isActive;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
            if(_skillCombo == 1)
            {
                EnemyMove enemyMove = other.GetComponent<EnemyMove>();
                enemyMove.GetAirborne(_airborneTime, _airborneSpeed);
                
            }
        }
    }
    
}
