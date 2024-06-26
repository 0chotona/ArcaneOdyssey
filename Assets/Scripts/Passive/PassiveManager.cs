using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Passive
{
    public string _name { get; set; }

    public int _level { get; set; }

    public float _rate { get; set; }

    public bool _isGot { get; set; }
    public Passive(string name, int level, float rate, bool isGot)
    {
        _name = name;
        _level = level;
        _rate = rate;
        _isGot = isGot;
    }
}
public enum PASSIVE_TYPE
{
    Attack_Up,
    Range_Up,
    CoolTime_Down,
    Exp_Up,
    Def_Up,
    MoveSpeed_Up
}
public class PassiveManager : MonoBehaviour
{
    Dictionary<PASSIVE_TYPE, Passive> _passives;

    List<string> _passiveNames = new List<string>();
    public List<string> _PassiveNames => _passiveNames;

    [SerializeField] PassiveController _passiveController;

    private void Awake()
    {
        _passives = new Dictionary<PASSIVE_TYPE, Passive>();

        _passives.Add(PASSIVE_TYPE.Attack_Up, new Passive("공격력 증가", 0, 5f, false));
        _passives.Add(PASSIVE_TYPE.Range_Up, new Passive("공격 범위 증가", 0, 5f, false));
        _passives.Add(PASSIVE_TYPE.CoolTime_Down, new Passive("쿨타임 감소", 0, 5f, false));
        _passives.Add(PASSIVE_TYPE.Exp_Up, new Passive("경험치 획득량 증가", 0, 5f, false));
        _passives.Add(PASSIVE_TYPE.Def_Up, new Passive("방어력 증가", 0, 5f, false));
        _passives.Add(PASSIVE_TYPE.MoveSpeed_Up, new Passive("이동속도 증가", 0, 5f, false));

        foreach (var passive in _passives.Values)
        {
            _passiveNames.Add(passive._name);
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
    }
    public bool IsMaxLevel(string name)
    {
        if (_passives[GetTypeByName(name)]._level >= 5)
            return true;
        else
            return false;
    }

    PASSIVE_TYPE GetTypeByName(string name)
    {
        PASSIVE_TYPE type = 0;
        foreach(var obj in _passives)
        {
            if (obj.Value._name == name)
                type = obj.Key;
        }
        return type;
    }
    
}
