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
    Def_Up,
    MaxHP_Up,
    HPRegen_Up,
    MoveSpeed_Up,
    ItemPickupRange_Up,
    Range_Up,
    Duration_Up,
    CriRate_Up,
    CoolTime_Down,
    ExpGain_Up,
    ProjectileCount_Up
}
public class BuffManager : MonoBehaviour
{
    Dictionary<eBUFF_TYPE, CBuff> _passives;
    public Dictionary<eBUFF_TYPE, CBuff> _Passives => _passives;

    List<CBuff> _passiveNames = new List<CBuff>();
    public List<CBuff> _PassiveNames => _passiveNames;

    [SerializeField] BuffStat _buffStat;

    [Header("소유 가능 버프 수"), SerializeField] int _buffSlotLimit = 6;
    List<eBUFF_TYPE> _possedBuffs = new List<eBUFF_TYPE>();

    private void Awake()
    {
        _passives = new Dictionary<eBUFF_TYPE, CBuff>();

        _passives.Add(eBUFF_TYPE.Attack_Up, new CBuff("공격력 증가", 0, 0.1f, false));
        _passives.Add(eBUFF_TYPE.Def_Up, new CBuff("방어력 증가", 0, 5f, false));
        _passives.Add(eBUFF_TYPE.MaxHP_Up, new CBuff("최대체력 증가", 0, 0.1f, false));
        _passives.Add(eBUFF_TYPE.HPRegen_Up, new CBuff("체력재생 증가", 0, 3f, false));
        _passives.Add(eBUFF_TYPE.MoveSpeed_Up, new CBuff("이동속도 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.ItemPickupRange_Up, new CBuff("획득범위 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.Range_Up, new CBuff("공격 범위 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.Duration_Up, new CBuff("지속시간 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.CriRate_Up, new CBuff("치명타 확률 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.CoolTime_Down, new CBuff("쿨타임 감소", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.ExpGain_Up, new CBuff("경험치 획득량 증가", 0, 0.05f, false));
        _passives.Add(eBUFF_TYPE.ProjectileCount_Up, new CBuff("투사체 증가", 0, 0.5f, false));

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
        if(CanAddBuff())
        {
            if (!_possedBuffs.Contains(GetBuffByName(name)))
            {
                _possedBuffs.Add(GetBuffByName(name));
            }
            _passives[GetTypeByName(name)]._level++;
            _passives[GetTypeByName(name)]._isGot = true;
            _buffStat.UpdateBuffStat(GetTypeByName(name), _passives[GetTypeByName(name)]._rate);
            UIManager.Instance.RemoveGiftName(name);
            if(!CanAddBuff())
            {
                List<CBuff> possedBuffs = new List<CBuff>();
                foreach(eBUFF_TYPE buff in _possedBuffs)
                {
                    possedBuffs.Add(_passives[buff]);
                }
                UIManager.Instance.RemoveEntireBuffNames(new List<CBuff>(possedBuffs));
            }
        }
        else
        {
            if (_possedBuffs.Contains(GetBuffByName(name)))
            {
                if (!IsMaxLevel(name))
                {
                    _passives[GetTypeByName(name)]._level++;
                }
            }

            _buffStat.UpdateBuffStat(GetTypeByName(name), _passives[GetTypeByName(name)]._rate);
            UIManager.Instance.RemoveGiftName(name);
        }

    }
    public eBUFF_TYPE GetBuffByName(string name)
    {
        eBUFF_TYPE eBuff = 0;
        foreach (eBUFF_TYPE buffType in _passives.Keys)
        {
            if (_passives[buffType]._name == name)
            {
                eBuff = buffType;
            }
        }
        return eBuff;
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
    bool CanAddBuff()
    {
        if (_possedBuffs.Count < _buffSlotLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
