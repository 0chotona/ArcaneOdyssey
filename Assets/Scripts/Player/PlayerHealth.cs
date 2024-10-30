using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("최대 Hp"), SerializeField] float _maxHp = 1500f;
    [Header("체력 리젠 간격"), SerializeField] float _hpRegenGap = 1f;
    float _curHp;

    float _hpRegenAmount = 0f;

    float _def = 20f;


    public bool _isInvincible = false;
    float _shieldHp = 0f;
    float _tmpShieldHp = 0f;
    float _iceArmorHp = 0f;


    float _defBuff = 0f;
    float _maxHpBuff = 0f;

    private List<float> _temporaryShields = new List<float>();

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

            // Temporary shields consume first
            for (int i = 0; i < _temporaryShields.Count && finalDamage > 0; i++)
            {
                if (_temporaryShields[i] > finalDamage)
                {
                    _temporaryShields[i] -= finalDamage;
                    finalDamage = 0;
                }
                else
                {
                    finalDamage -= _temporaryShields[i];
                    _temporaryShields[i] = 0;
                }
            }

            // Remove consumed shields
            _temporaryShields.RemoveAll(shield => shield <= 0);

            // Use main shield if any damage remains
            if (finalDamage > 0 && _shieldHp > 0)
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

            // Apply remaining damage to HP
            if (finalDamage > 0)
            {
                _curHp -= finalDamage;
                UIManager.Instance.UpdateHpBar(_maxHp + (_maxHp * _maxHpBuff), _curHp);

                if (_curHp <= 0)
                    Dead();
            }

            UIManager.Instance.UpdateShieldBar(_shieldHp + _temporaryShields.Sum());
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
    public void SetIceArmor(float amount)
    {
        _iceArmorHp = amount;
        _shieldHp = _iceArmorHp + _tmpShieldHp;
        UIManager.Instance.UpdateShieldBar(_shieldHp);
    }
    IEnumerator CRT_SetShield(float amount, float durTime)
    {
        float finalAmount = amount;
        if(finalAmount > (_maxHp * 0.02f))
        {
            finalAmount = _maxHp * 0.02f;
        }
        _temporaryShields.Add(finalAmount);
        UIManager.Instance.UpdateShieldBar(_shieldHp + _temporaryShields.Sum());

        yield return new WaitForSeconds(durTime);

        if (_temporaryShields.Contains(finalAmount))
        {
            _temporaryShields.Remove(finalAmount);
            UIManager.Instance.UpdateShieldBar(_shieldHp + _temporaryShields.Sum());
        }
    }
    public bool IsShieldBroken()
    {
        if(_shieldHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void SetInvincible(bool isInvincible)
    {
        _isInvincible = isInvincible;
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
