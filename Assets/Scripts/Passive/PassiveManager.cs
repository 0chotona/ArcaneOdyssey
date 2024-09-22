using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class CPassive
{
    public string _name { get; set; }

    public int _level { get; set; }

    public float _rate { get; set; }

    public bool _isGot { get; set; }
    public CPassive(string name, int level, float rate, bool isGot)
    {
        _name = name;
        _level = level;
        _rate = rate;
        _isGot = isGot;
    }
}
public enum ePASSIVE_TYPE
{
    Attack_Up,
    Range_Up,
    CoolTime_Down,
    Def_Up,
    MoveSpeed_Up,
    MaxHP_Up
}
public class PassiveManager : MonoBehaviour
{
    Dictionary<ePASSIVE_TYPE, CPassive> _passives;
    public Dictionary<ePASSIVE_TYPE, CPassive> _Passives => _passives;

    List<CPassive> _passiveNames = new List<CPassive>();
    public List<CPassive> _PassiveNames => _passiveNames;

    [SerializeField] PassiveController _passiveController;

    private void Awake()
    {
        _passives = new Dictionary<ePASSIVE_TYPE, CPassive>();

        _passives.Add(ePASSIVE_TYPE.Attack_Up, new CPassive("공격력 증가", 0, 5f, false));
        _passives.Add(ePASSIVE_TYPE.Range_Up, new CPassive("공격 범위 증가", 0, 5f, false));
        _passives.Add(ePASSIVE_TYPE.CoolTime_Down, new CPassive("쿨타임 감소", 0, 5f, false));
        _passives.Add(ePASSIVE_TYPE.Def_Up, new CPassive("방어력 증가", 0, 5f, false));
        _passives.Add(ePASSIVE_TYPE.MoveSpeed_Up, new CPassive("이동속도 증가", 0, 5f, false));
        _passives.Add(ePASSIVE_TYPE.MaxHP_Up, new CPassive("최대체력 증가", 0, 5f, false));

        foreach (var passive in _passives.Values)
        {
            _passiveNames.Add(passive);
        }
    }
    public int GetLevel(string name)
    {
        int level = 0;
        foreach(var obj in _passives.Values)
        {
            if(obj._name == name)
                level = obj._level;
        }
        return level;
    }
    public void UpgradeLevel(string name)
    {
        _passives[GetTypeByName(name)]._level++;
        _passives[GetTypeByName(name)]._isGot = true;
        _passiveController.UpdateStat(GetTypeByName(name));
        UIManager.Instance.RemoveGiftName(name);
    }
    public bool IsMaxLevel(string name)
    {
        if (_passives[GetTypeByName(name)]._level >= 5)
            return true;
        else
            return false;
    }

    ePASSIVE_TYPE GetTypeByName(string name)
    {
        ePASSIVE_TYPE type = 0;
        foreach(var obj in _passives)
        {
            if (obj.Value._name == name)
                type = obj.Key;
        }
        return type;
    }
    
}
