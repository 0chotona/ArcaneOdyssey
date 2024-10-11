using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IActiveAttackable
{
    void ActiveInteract();
    void SetBuffStat(CBuffStat buffStat);
    void SetPlayerTrs(Transform playerTrs);
}
public class ActiveController
{
    IActiveAttackable _skill1Method;
    IActiveAttackable _skill2Method;
    public void SetAttack1Method(IActiveAttackable attackMethod)
    {
        _skill1Method = attackMethod;
    }
    public void SetAttack2Method(IActiveAttackable attackMethod)
    {
        _skill2Method = attackMethod;
    }
    public IActiveAttackable GetAttackMethod()
    {
        return _skill1Method;
    }
    public void SetBuffStat(CBuffStat buffStat)
    {
        _skill1Method.SetBuffStat(buffStat);
        _skill2Method.SetBuffStat(buffStat);
    }
    public void PerformAttack1()
    {
        if (_skill1Method != null)
            _skill1Method.ActiveInteract();
        else
            Debug.Log("어택 메소드 없음");
    }
    public void PerformAttack2()
    {
        if (_skill2Method != null)
            _skill2Method.ActiveInteract();
        else
            Debug.Log("어택 메소드 없음");
    }
}
public class ActiveAttack : MonoBehaviour
{
    ActiveController _controller = new ActiveController();

    [Header("스킬 1 버튼"), SerializeField] Button _skill1Button;
    [Header("스킬 2 버튼"), SerializeField] Button _skill2Button;

    CBuffStat _buffStat = new CBuffStat();
    private void Awake()
    {
        _skill1Button.onClick.AddListener(() => Click_Skill1());
        _skill2Button.onClick.AddListener(() => Click_Skill2());
    }
    public void SetSkillMethod(IActiveAttackable method1, IActiveAttackable method2)
    {
        if(method1 != null)
            _controller.SetAttack1Method(method1);

        if(method2 != null)
            _controller.SetAttack2Method(method2);
    }
    public void SetBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
        _controller.SetBuffStat(buffStat);
    }
    void Click_Skill1()
    {
        _controller.PerformAttack1();
    }
    void Click_Skill2()
    {
        _controller.PerformAttack2();
    }
}
