using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("ÃÖ´ë Hp"), SerializeField] float _maxHp = 500f;
    float _curHp;

    float _def = 10f;

    public float _defIncrease;
    public float _maxHpIncrease;

    bool _isInvincible = false;
    float _shieldHp = 0f;

    private void Awake()
    {
        _curHp = _maxHp;
        UIManager.Instance.UpdateHpBar(_maxHp, _maxHp);
    }
    public void GetDamage(int dmg)
    {
        if (!_isInvincible)
        {
            float finalDef = 100 + _def + (_def * 0.01f * _defIncrease);
            float finalDamage = dmg * 100 / finalDef;

            if (_shieldHp > 0)
            {
                if (_shieldHp >= finalDamage)
                {
                    _shieldHp -= finalDamage;
                    finalDamage = 0;
                }
                else
                {
                    finalDamage -= _shieldHp; 
                    _shieldHp = 0;
                }
            }

            if (finalDamage > 0)
            {
                _curHp -= finalDamage;
                UIManager.Instance.UpdateHpBar(_maxHp, _curHp);
                
                if (_curHp <= 0)
                    Dead();
            }
            UIManager.Instance.UpdateShieldBar(_shieldHp);
        }
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
    public void SetShield(float amount, float time)
    {
        StartCoroutine(CRT_SetShield(amount ,time));
    }
    IEnumerator CRT_SetShield(float amount, float durTime)
    {
        _shieldHp = amount;
        UIManager.Instance.UpdateShieldBar(_shieldHp);
        yield return new WaitForSeconds(durTime);
        _shieldHp = 0;
        UIManager.Instance.UpdateShieldBar(_shieldHp);
    }
    public void SetInvincible(float time)
    {
        StartCoroutine(CRT_Invincible(time));
    }
    IEnumerator CRT_Invincible(float time)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(time);
        _isInvincible = false;
    }
    public void UpdatePassiveStat()
    {
        _defIncrease += 5;
        
    }
}
