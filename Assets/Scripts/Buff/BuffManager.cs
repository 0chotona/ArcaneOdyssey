using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class CBuff
{
    public string _name { get; set; }

    public int _level { get; set; }

    public float _rate { get; set; }

    public bool _isGot { get; set; }
    public CBuff(string name, int level, float rate, bool isGot)
    {
        _name = name;
        _level = level;
        _rate = rate;
        _isGot = isGot;
    }
}
public enum eBUFF_TYPE
{
    Attack_Up,
    Range_Up,
    CoolTime_Down,
    Def_Up,
    MoveSpeed_Up,
    MaxHP_Up, 
    CriRate_Up
}
public class BuffManager : MonoBehaviour
{
    Dictionary<eBUFF_TYPE, CBuff> _passives;
    public Dictionary<eBUFF_TYPE, CBuff> _Passives => _passives;

    List<CBuff> _passiveNames = new List<CBuff>();
    public List<CBuff> _PassiveNames => _passiveNames;

    [SerializeField] BuffController _passiveController;

    private void Awake()
    {
        _passives = new Dictionary<eBUFF_TYPE, CBuff>();

        _passives.Add(eBUFF_TYPE.Attack_Up, new CBuff("공격력 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.Range_Up, new CBuff("공격 범위 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.CoolTime_Down, new CBuff("쿨타임 감소", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.Def_Up, new CBuff("방어력 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.MoveSpeed_Up, new CBuff("이동속도 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.MaxHP_Up, new CBuff("최대체력 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.CriRate_Up, new CBuff("치명타 확률 증가", 0, 0.05f, false));

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
        _passiveController.UpdateBuffStat(GetTypeByName(name), _passives[GetTypeByName(name)]._rate);
        UIManager.Instance.RemoveGiftName(name);
    }
    public bool IsMaxLevel(string name)
    {
        if (_passives[GetTypeByName(name)]._level >= 5)
            return true;
        else
            return false;
    }

    eBUFF_TYPE GetTypeByName(string name)
    {
        eBUFF_TYPE type = 0;
        foreach(var obj in _passives)
        {
            if (obj.Value._name == name)
                type = obj.Key;
        }
        return type;
    }
    
}
