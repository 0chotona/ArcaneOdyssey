using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chise_Passive : MonoBehaviour, IPassive
{
    Transform _playerTrs;
    PlayerHealth _playerHealth;

    [Header("½¯µå Áö¼Ó½Ã°£"), SerializeField] float _shieldDur = 1f;
    [Header("½¯µå ºñÀ²"), SerializeField] float _shieldRate = 0.25f;
    float _damageAmount = 0f;

    public int GetKillCount()
    {
        return 0;
    }

    public bool IsActive()
    {
        return true;
    }

    public void PassiveInteract()
    {
        _playerHealth.SetShield(_damageAmount * _shieldRate, _shieldDur);
    }

    public void SetEnemyDamage(float amount)
    {
        _damageAmount = amount;
        PassiveInteract();
    }

    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
        _playerHealth = _playerTrs.GetComponentInChildren<PlayerHealth>();
    }
}
