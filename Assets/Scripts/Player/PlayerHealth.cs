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
    public float _shieldHp = 0f;
    float _iceArmorHp = 0f;


    float _defBuff = 0f;
    float _maxHpBuff = 0f;
    int _shieldIndex = 0;

    Dictionary<int,float> _temporaryShields = new Dictionary<int, float>();

    private void Awake()
    {
        _curHp = _maxHp + _maxHpBuff;
        UIManager.Instance.UpdateHpBar(_maxHp + (_maxHp * _maxHpBuff), _curHp);
        StartCoroutine(CRT_HpRegen());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(_temporaryShields.Values.Sum());
        }
    }
    public void GetDamage(float dmg)
    {
        if (!_isInvincible)
        {
            float finalDef = 100 + _def + _defBuff;
            float finalDamage = dmg * 100 / finalDef;

            // Temporary shields consume first
            foreach (var key in _temporaryShields.Keys.ToList())
            {
                if (finalDamage <= 0)
                    break;

                if (_temporaryShields[key] > finalDamage)
                {
                    _temporaryShields[key] -= finalDamage;
                    finalDamage = 0;
                }
                else
                {
                    finalDamage -= _temporaryShields[key];
                    _temporaryShields[key] = 0;
                }

                // Remove the entry if its value is zero
                if (_temporaryShields[key] <= 0)
                {
                    _temporaryShields.Remove(key);
                }
            }
            /*
            List<int> keysToRemove = _temporaryShields.Where(pair => pair.Value == 0).Select(pair => pair.Key).ToList();
            foreach (int key in keysToRemove)
            {
                _temporaryShields.Remove(key);
            }
            */
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

            UIManager.Instance.UpdateShieldBar(_shieldHp + _temporaryShields.Values.Sum());
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
        _shieldHp = _iceArmorHp;
        UIManager.Instance.UpdateShieldBar(_shieldHp);
    }
    IEnumerator CRT_SetShield(float amount, float durTime)
    {
        float finalAmount = amount;
        if(finalAmount > (_maxHp * 0.02f))
        {
            finalAmount = _maxHp * 0.02f;
        }
        _temporaryShields.Add(_shieldIndex, finalAmount);
        UIManager.Instance.UpdateShieldBar(_shieldHp + _temporaryShields.Values.Sum());
        int index = _shieldIndex;
        _shieldIndex++;
        yield return new WaitForSeconds(durTime);

        _temporaryShields.Remove(index);
        UIManager.Instance.UpdateShieldBar(_shieldHp + _temporaryShields.Values.Sum());
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
