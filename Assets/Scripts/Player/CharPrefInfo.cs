using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPrefInfo : MonoBehaviour
{
    [Header("ĳ���� ���� ����"), SerializeField] GameObject _charAttackObj;

    [Header("��ų 1"), SerializeField] Transform _skill1;
    [Header("��ų 2"), SerializeField] Transform _skill2;
    [Header("�нú�"), SerializeField] Transform _passiveObj;

    public IActiveAttackable _Skill1;
    public IActiveAttackable _Skill2;

    IPassive _passive;
    public IPassive _Passive => _passive;

    Transform _playerTrs;

    private void Awake()
    {
        if(_skill1 != null)
            _Skill1 = _skill1.GetComponent<IActiveAttackable>();
        if(_skill2 != null)
            _Skill2 = _skill2.GetComponent<IActiveAttackable>();

        if(_passiveObj != null)
            _passive = _passiveObj.GetComponent<IPassive>();
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
        _Skill1.SetPlayerTrs(playerTrs);
        _Skill2.SetPlayerTrs(playerTrs);
        _passive.SetPlayerTrs(playerTrs);
    }
    public Attack GetCharAttack()
    {
        Attack attack = _charAttackObj.GetComponent<Attack>();
        return attack;
    }
}
