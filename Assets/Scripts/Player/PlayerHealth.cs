using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("√÷¥Î Hp"), SerializeField] float _maxHp = 500f;
    float _curHp;

    float _def = 10f;

    public float _defIncrease;
    public float _maxHpIncrease;



    private void Awake()
    {
        _curHp = _maxHp;
    }
    public void GetDamage(int dmg)
    {
        float finalDef = 100 + _def + (_def * 0.01f * _defIncrease);
        float finalDamage = dmg * 100 / finalDef;
        _curHp -= finalDamage;

        UIManager.Instance.UpdateHpBar(_maxHp, _curHp);
        if (_curHp < 0)
            Dead();
    }
    public void GetHeal(float healRate)
    {
        _curHp += healRate * _maxHp;
        if(_curHp > _maxHp)
            _curHp = _maxHp;

        UIManager.Instance.UpdateHpBar(_maxHp, _curHp);
    }
    void Dead()
    {
        
    }
    public void UpdatePassiveStat()
    {
        _defIncrease += 5;
        
    }
}
