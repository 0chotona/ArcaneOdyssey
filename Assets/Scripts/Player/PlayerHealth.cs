using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("최대 Hp"), SerializeField] float _maxHp = 500f;
    [Header("체력 리젠 간격"), SerializeField] float _hpRegenGap = 1f;
    float _curHp;

    float _hpRegenAmount = 0f;

    float _def = 20f;


    bool _isInvincible = false;
    float _shieldHp = 0f;

    float _defBuff = 0f;
    float _maxHpBuff = 0f;
    private void Awake()
    {
        _curHp = _maxHp + _maxHpBuff;
        UIManager.Instance.UpdateHpBar(_maxHp + (_maxHp * _maxHpBuff), _curHp);
        StartCoroutine(CRT_HpRegen());
    }
    public void GetDamage(int dmg)
    {
        if (!_isInvincible)
        {
            float finalDef = 100 + _def + _defBuff;
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
                UIManager.Instance.UpdateHpBar(_maxHp + (_maxHp * _maxHpBuff), _curHp);
                
                if (_curHp <= 0)
                    Dead();
            }
            UIManager.Instance.UpdateShieldBar(_shieldHp);
        }
    }
    public void GetHeal(float healAmount)
    {
        _curHp += healAmount;
        if(_curHp > _maxHp + (_maxHp * _maxHpBuff))
            _curHp = _maxHp + (_maxHp * _maxHpBuff);

        UIManager.Instance.UpdateHpBar(_maxHp + (_maxHp * _maxHpBuff), _curHp);
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
    public void UpdateDef(float value)
    {
        _defBuff = value;
    }
    public void UpdateMaxHpBuff(float value)
    {
        _maxHpBuff = value;
        UIManager.Instance.UpdateHpBar(_maxHp + (_maxHp * _maxHpBuff), _curHp);
    }
    public void UpdateHpRegenBuff(float value)
    {
        _hpRegenAmount = value;
    }
    IEnumerator CRT_HpRegen()
    {
        while(true)
        {
            yield return new WaitForSeconds(_hpRegenGap);
            GetHeal(_hpRegenAmount);
        }
        
    }
}
