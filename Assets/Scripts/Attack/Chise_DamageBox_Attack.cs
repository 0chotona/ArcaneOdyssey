using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chise_DamageBox_Attack : MonoBehaviour
{
    float _damage;
    Vector3 _boxScale = Vector3.one;
    bool _isMaxLevel = false;

    public void UpdateIsMaxLevel(bool isMaxLevel) { _isMaxLevel = isMaxLevel; }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(Vector3 scale) 
    { 
        _boxScale = scale;
        transform.localScale = _boxScale;
    }
}
