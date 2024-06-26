using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int _maxHp = 100;
    int _curHp;

    int _def;

    public int _defIncrease;
    private void Awake()
    {
        _curHp = _maxHp;
    }
    public void GetDamage(int dmg)
    {
        float finalDef = 100 + _def + (_def * 0.01f * _defIncrease);
        int finalDamage = (int)(dmg * 100 / finalDef);
        _curHp -= finalDamage;
        if (_curHp < 0)
            Dead();
    }
    public void GetHeal(float healRate)
    {
        _curHp += (int)(healRate * _maxHp);
        if(_curHp > _maxHp)
            _curHp = _maxHp;
    }
    void Dead()
    {

    }
    public void UpdatePassiveStat()
    {
        _defIncrease += 5;
        
    }
}
