using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveAttackable
{
    void ActiveInteract();
}
public class ActiveController
{
    IActiveAttackable _attackMethod;
    public void SetAttackMethod(IActiveAttackable attackMethod)
    {
        _attackMethod = attackMethod;
    }
    public IActiveAttackable GetAttackMethod()
    {
        return _attackMethod;
    }
    public void PerformAttack()
    {
        if (_attackMethod != null)
            _attackMethod.ActiveInteract();
        else
            Debug.Log("어택 메소드 없음");
    }
}
public class ActiveAttack : MonoBehaviour
{
    ActiveController _controller;

    
}
