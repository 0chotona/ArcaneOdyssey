using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassive
{
    int GetKillCount();
    void SetEnemyDamage(float amount);
    bool IsActive();
    void PassiveInteract();
    void SetPlayerTrs(Transform playerTrs);

}
public class PassiveController
{
    IPassive _passiveMethod;
    public IPassive _PassiveMethod => _passiveMethod;
    public void SetPassiveMethod(IPassive passiveMethod)
    {
        _passiveMethod = passiveMethod;
    }
    public void PerformPassive()
    {
        if (_passiveMethod != null)
            _passiveMethod.PassiveInteract();
        else
            Debug.Log("�нú� �޼ҵ� ����");
    }
}
public class PassiveManager : MonoBehaviour
{
    static PassiveManager _instance;

    public static PassiveManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PassiveManager>();
            return _instance;
        }
    }
    PassiveController _controller = new PassiveController();

    [SerializeField] EnemySensor _enemySensor;
    int _deathCount = 0;
    public int _DeathCount => _deathCount;
    CChar _selectedChar;

    [Header("�÷��̾� ü��"), SerializeField] PlayerHealth _playerHealth;
    public void UpdateDeathCount()
    {
        _enemySensor.RemoveDestroyEnemy();
        _deathCount++;
        if (_selectedChar._charType == eCHARACTER.Momoi)
        {
            if(!_controller._PassiveMethod.IsActive())
            {
                if (_deathCount >= _controller._PassiveMethod.GetKillCount())
                {
                    _deathCount = 0;
                    _controller.PerformPassive();
                }
            }
            
        }
        
        
    }
    public void SetPassiveMethod(IPassive method)
    {
        if (method != null)
            _controller.SetPassiveMethod(method);
    }
    public void SetSelectedChar(CChar selectedChar)
    {
        _selectedChar = selectedChar;
    }
    public void EnemyDamage(float amount)
    {
        if (_playerHealth != null)
        {
            if(_selectedChar._charType == eCHARACTER.Chise)
            {
                _controller._PassiveMethod.SetEnemyDamage(amount);
            }
        }

    }
}
