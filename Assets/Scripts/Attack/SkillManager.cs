using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class CStat
{
    //데미지 / 공격횟수 / 공격범위 / 쿨타임 / 유지시간

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
    
    public ePASSIVE_TYPE _synergyType { get; set; }
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

    int _attIncrease;
    int _rangeIncrease;
    int _coolTimeDecrease;

    Dictionary<eSKILL, CSkill> _skillDic = new Dictionary<eSKILL, CSkill>();
    [Header("스킬 오브젝트"),SerializeField] List<Attack> _attacks;
    [SerializeField] PassiveManager _passiveManager;
    [Header("플레이어 트랜스폼"), SerializeField] Transform _playerTrs;
    public Transform _PlayerTrs => _playerTrs;
    SkillData _skillData;

    CChar _selectedChar = new CChar();
    CSkill _selectedSkill = new CSkill();
    
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
    public void SetSkillAwake(SkillData skillData, CChar selectedChar)
    {
        _skillData = skillData;

        SetCharSkill(selectedChar);
        _skillDic.Add(_selectedSkill._skillName, _selectedSkill); //_skillDatas에 캐릭 스킬 추가

        
        foreach (CSkill skill in _skillData._SkillDatas)
        {
            _skillDic.Add(skill._skillName, skill);
            _skillDic[skill._skillName]._stat = new CStat();
        }

        



        foreach (CSkill skill in _skillDic.Values)
            _skillNames.Add(skill._skillText); //UI 아이템 선택창에 쓰일 이름 리스트

        UIManager.Instance.SetGiftNames(new List<CSkill>(_skillDic.Values)); //UI 아이템 선택창 이름 리스트 세팅

        
    }
    public void AddSkill(eSKILL name)
    {
        _possedSkills.Add(name, FindSkillByName(name));
        StartAttack(name);
    }
    public void UpgradeLevel(eSKILL name)
    {
        
        if(!IsMaxLevel(name))
        {
            _skillDic[name].UpgradeLevel();
            if (!_possedSkills.ContainsKey(name))
                AddSkill(name);
        }
        else
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
    public void UpdatePassiveStat(ePASSIVE_TYPE type)
    {
        switch(type)
        {
            case ePASSIVE_TYPE.Attack_Up:
                _attIncrease += 5;
                break;
            case ePASSIVE_TYPE.Range_Up:
                _rangeIncrease += 5;
                break;
            case ePASSIVE_TYPE.CoolTime_Down:
                _coolTimeDecrease -= 5;
                break;
        }
    }
    void SetAttackStat()
    {
        List<CSkill> skills = new List<CSkill>(_skillDic.Values);
        for (int i = 0; i < _skillDic.Count; i++)
        {
            _attacks[i].SetSkill(skills[i]);
            _attacks[i].UpdateStat(skills[i]._stat);
        }
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
}
