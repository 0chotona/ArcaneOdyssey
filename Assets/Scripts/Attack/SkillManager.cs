using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class CStat
{
    //������ / ����Ƚ�� / ���ݹ��� / ��Ÿ�� / �����ð�

    public float _damage { get; set; }
    public int _attCount { get; set; }
    public float _attRange { get; set; }
    public float _coolTime { get; set; }
    public float _durTime { get; set; }
    public float _shotSpeed { get; set; }

    
}
public class CSkill
{
    public int _id { get; set; }

    public string _skillText { get; set; }
    public eSKILL _skillName { get; set; }
    public string _iconName { get; set; }
    public int _level { get; set; }
    public CStat _stat { get; set; }

    public Dictionary<int, CStat> _skillMap = new Dictionary<int, CStat>();
    
    public eBUFF_TYPE _synergyType { get; set; }
    public string _char_Pref_Name { get; set; }

    public void UpgradeLevel()
    {
        _level++;
        _stat = _skillMap[_level];
    }
    

}

public class SkillManager : MonoBehaviour
{
    static SkillManager _instance;

    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SkillManager>();
            return _instance;
        }

    }
    Dictionary<eSKILL, CSkill> _possedSkills = new Dictionary<eSKILL, CSkill>();
    List<string> _skillNames = new List<string>();

    

    Dictionary<eSKILL, CSkill> _skillDic = new Dictionary<eSKILL, CSkill>();
    public Dictionary<eSKILL, CSkill> _SkillDic => _skillDic;
    List<Attack> _attacks = new List<Attack>();
    [Header("��ų Ʈ������"), SerializeField] Transform _attackTrs;

    [SerializeField] BuffManager _passiveManager;
    [Header("�÷��̾� Ʈ������"), SerializeField] Transform _playerTrs;
    public Transform _PlayerTrs => _playerTrs;
    SkillData _skillData;


    [Header("��Ƽ�� ����"), SerializeField] ActiveAttack _activeAttack;

    CChar _selectedChar = new CChar();
    Attack _selectedCharAttack = null;
    CSkill _selectedSkill = new CSkill();

    CBuffStat _buffStat = new CBuffStat();

    [Header("�⺻ ġ��Ÿ"), SerializeField] float _baseCriRate = 0.05f;
    [Header("ġ��Ÿ ������"), SerializeField] float _baseCriDmg = 1.5f;
    public float _BaseCriDmg => _baseCriDmg;

    private void Start()
    {
        SetAttackStat();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpgradeLevel(_skillDic.ElementAt(0).Value._skillName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UpgradeLevel(_skillDic.ElementAt(1).Value._skillName);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UpgradeLevel(_skillDic.ElementAt(2).Value._skillName);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UpgradeLevel(_skillDic.ElementAt(3).Value._skillName);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            UpgradeLevel(_skillDic.ElementAt(4).Value._skillName);

    }
    public void SetCharSkillAwake(Attack charAttack, CChar selectedChar)
    {
        SetCharSkill(selectedChar);
        _skillDic.Add(_selectedSkill._skillName, _selectedSkill); //_skillDatas�� ĳ�� ��ų �߰�

        _selectedCharAttack = charAttack;
        
    }
    public void SetData(SkillData skillData)
    {
        _skillData = skillData;
    }
    public void SetSkillAwake()
    {

        foreach (CSkill skill in _skillData._SkillDatas)
        {
            _skillDic.Add(skill._skillName, skill);
            _skillDic[skill._skillName]._stat = new CStat();
        }

        



        foreach (CSkill skill in _skillDic.Values)
            _skillNames.Add(skill._skillText); //UI ������ ����â�� ���� �̸� ����Ʈ

        UIManager.Instance.SetGiftNames(new List<CSkill>(_skillDic.Values)); //UI ������ ����â �̸� ����Ʈ ����

        AddAttacks();
        
        AddSkill(_selectedCharAttack._name);
        UpgradeLevel(_selectedCharAttack._name);
        SetBuffStat(_buffStat);
    }
    public void AddSkill(eSKILL name) //��ų ùȹ��
    {
        _possedSkills.Add(name, FindSkillByName(name));
        StartAttack(name);
    }
    public void AddAttacks()
    {
        _attacks.Add(_selectedCharAttack);

        Attack[] attacks = _attackTrs.GetComponentsInChildren<Attack>();
        foreach(CSkill skill in _skillDic.Values)
        {
            foreach (Attack attack in attacks)
            {
                if(skill._skillName == attack._name)
                {
                    _attacks.Add(attack);
                    break;
                }
            }
        }
    }
    public void UpgradeLevel(eSKILL name)
    {
        
        if(!IsMaxLevel(name))
        {
            _skillDic[name].UpgradeLevel();
            if (!_possedSkills.ContainsKey(name))
                AddSkill(name);
        }
        else if(FindSkillByName(name)._level < 6)
        {
            if (_passiveManager._Passives[FindSkillByName(name)._synergyType]._level > 0)
                _skillDic[name].UpgradeLevel();
        }
        SetAttackStat();
        UIManager.Instance.RemoveGiftName(FindSkillByName(name)._skillText);

    }
    void StartAttack(eSKILL name)
    {
        foreach (Attack att in _attacks)
        {
            if (att._name == name)
                att.StartAttack();
        }
    }
    public int GetLevel(string name)
    {
        int level = 0;
        foreach(CSkill skill in _skillDic.Values)
        {
            if(skill._skillText == name)
                level = skill._level;
        }
        return level;
    }
    CSkill FindSkillByName(eSKILL name)
    {
        CSkill skill = null;
        foreach (CSkill sk in _skillDic.Values)
        {
            if (sk._skillName == name)
                skill = sk;
        }
        return skill;
    }

    public eSKILL GetSkillByName(string name)
    {
        eSKILL eSkill = 0;
        foreach (CSkill skill in _skillDic.Values)
        {
            if (skill._skillText == name)
                eSkill = skill._skillName;
        }
        return eSkill;
    }
    public bool IsMaxLevel(eSKILL name)
    {
        if (FindSkillByName(name)._level >= 5)
            return true;
        else
            return false;
    }
    
    void SetAttackStat()
    {
        List<CSkill> skills = new List<CSkill>(_skillDic.Values);
        for (int i = 0; i < _attacks.Count; i++)
        {
            _attacks[i].SetSkill(skills[i]);
            _attacks[i].UpdateStat(skills[i]._stat);
        }
        //SetBuffStat(_buffStat);
    }
    void SetCharSkill(CChar selectedChar)
    {
        _selectedChar = selectedChar;
        CSkill charSkill = null;
        foreach(CSkill skill in _skillData._CharSkillDatas)
        {
            if(skill._char_Pref_Name == _selectedChar._prefName)
            {
                charSkill = skill;
                break;
            }
        }
        _selectedSkill = charSkill;
    }
    public void SetSkillMethod(IActiveAttackable method1, IActiveAttackable method2)
    {
        _activeAttack.SetSkillMethod(method1, method2);
    }
    public void SetBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
        foreach (Attack attack in _attacks)
            attack.UpdateBuffStat(buffStat);

        _activeAttack.SetBuffStat(buffStat);
    }
    public bool IsCritical()
    {
        float rnd = Random.Range(0f, 1f);
        if (rnd < _baseCriRate + _buffStat._CriRate)
        {
            Debug.Log("ġ��Ÿ ���� / _baseCriRate : " + _baseCriRate + "_CriRateBuff : " + _buffStat._CriRate + " rnd : " + rnd);

            return true;
        }
        else
            return false;
    }
}
