using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassive
{
    int GetKillCount();
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
            Debug.Log("패시브 메소드 없음");
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

    public void UpdateDeathCount()
    {
        _enemySensor.RemoveDestroyEnemy();
        _deathCount++;
        if (_selectedChar._charName == "모모이")
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
    
}
