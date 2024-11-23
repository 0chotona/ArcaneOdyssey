using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuff
{
    public string _name { get; set; }
    public eBUFF_TYPE _buffType { get; set; }

    public int _level { get; set; }

    public float _rate { get; set; }

    public bool _isGot { get; set; }
    string _iconName;
    public string _IconName => _iconName;
    public CBuff(string name, eBUFF_TYPE buffType, int level, float rate, string iconName, bool isGot)
    {
        _name = name;
        _buffType = buffType;
        _level = level;
        _rate = rate;
        _iconName = iconName;
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
    static BuffManager _instance;

    public static BuffManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<BuffManager>();
            return _instance;
        }

    }
    Dictionary<eBUFF_TYPE, CBuff> _passives;
    public Dictionary<eBUFF_TYPE, CBuff> _Passives => _passives;

    List<CBuff> _passiveNames = new List<CBuff>();
    public List<CBuff> _PassiveNames => _passiveNames;

    [SerializeField] BuffStat _buffStat;

    [Header("���� ���� ���� ��"), SerializeField] int _buffSlotLimit = 6;
    List<eBUFF_TYPE> _possedBuffs = new List<eBUFF_TYPE>();
    public List<eBUFF_TYPE> _PossedBuffs => _possedBuffs;

    private void Awake()
    {
        _passives = new Dictionary<eBUFF_TYPE, CBuff>();

        _passives.Add(eBUFF_TYPE.Attack_Up, new CBuff("���ݷ� ����", eBUFF_TYPE.Attack_Up, 0, 0.1f, "Buff_00", false));
        _passives.Add(eBUFF_TYPE.Def_Up, new CBuff("���� ����", eBUFF_TYPE.Def_Up, 0, 8f, "Buff_01", false));
        _passives.Add(eBUFF_TYPE.MaxHP_Up, new CBuff("�ִ�ü�� ����", eBUFF_TYPE.MaxHP_Up, 0, 0.1f, "Buff_02", false));
        _passives.Add(eBUFF_TYPE.HPRegen_Up, new CBuff("ü����� ����", eBUFF_TYPE.HPRegen_Up, 0, 4f, "Buff_03", false));
        _passives.Add(eBUFF_TYPE.MoveSpeed_Up, new CBuff("�̵��ӵ� ����", eBUFF_TYPE.MoveSpeed_Up, 0, 0.08f, "Buff_04", false));
        _passives.Add(eBUFF_TYPE.ItemPickupRange_Up, new CBuff("ȹ����� ����", eBUFF_TYPE.ItemPickupRange_Up, 0, 0.35f, "Buff_05", false));
        _passives.Add(eBUFF_TYPE.Range_Up, new CBuff("���� ���� ����", eBUFF_TYPE.Range_Up, 0, 0.11f, "Buff_06", false));
        _passives.Add(eBUFF_TYPE.Duration_Up, new CBuff("���ӽð� ����", eBUFF_TYPE.Duration_Up, 0, 0.12f, "Buff_07", false));
        _passives.Add(eBUFF_TYPE.CriRate_Up, new CBuff("ġ��Ÿ Ȯ�� ����", eBUFF_TYPE.CriRate_Up, 0, 0.08f, "Buff_08", false));
        _passives.Add(eBUFF_TYPE.CoolTime_Down, new CBuff("��Ÿ�� ����", eBUFF_TYPE.CoolTime_Down, 0, 0.05f, "Buff_09", false));
        _passives.Add(eBUFF_TYPE.ExpGain_Up, new CBuff("����ġ ȹ�淮 ����", eBUFF_TYPE.ExpGain_Up, 0, 0.1f, "Buff_10", false));
        _passives.Add(eBUFF_TYPE.ProjectileCount_Up, new CBuff("����ü ����", eBUFF_TYPE.ProjectileCount_Up, 0, 0.5f, "Buff_11", false));

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
    public string GetNameByType(eBUFF_TYPE buffType)
    {
        string name = _passives[buffType]._name;
        return name;
    }
    public void UpgradeLevel(string name)
    {
        if(CanAddBuff())
        {
            if (!_possedBuffs.Contains(GetBuffByName(name)))
            {
                _possedBuffs.Add(GetBuffByName(name));
                UIManager.Instance.UpdatePossesedIcon(_passives[GetTypeByName(name)]);
            }
            if (!IsMaxLevel(name))
            {
                //����� ȭ�鿡���� �������� �ΰ� ������ �̰� ���
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
                    _buffStat.UpdateBuffStat(GetTypeByName(name), _passives[GetTypeByName(name)]._rate);
                    UIManager.Instance.RemoveGiftName(name);
                }
            }

           
           
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
    public string GetIconNameByBuffName(string buffName)
    {
        string iconName = null;
        foreach (CBuff buff in _passives.Values)
        {
            if (buff._name == buffName)
            {
                iconName = buff._IconName;
            }
        }
        return iconName;
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
