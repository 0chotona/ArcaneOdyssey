using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public int _level { get; set; }

    //데미지 / 공격횟수 / 공격범위 / 쿨타임 / 유지시간
    public string _name { get; set; }
    public int _damage { get; set; }
    public int _attCount { get; set; }
    public float _attRange { get; set; }
    public float _coolTime { get; set; }
    public float _durTime { get; set; }
    public float _shotSpeed { get; set; }

    public PASSIVE_TYPE _synergyType { get; set; }
    /*
    public Skill(string name)
    {
        _name = name;
    }
    */
    public Skill(string name, int level, int damage, int attCount, float attRange, float coolTime, float durTime, float shotSpeed)
    {
        _name = name;
        _level = level;
        _damage = damage;
        _attCount = attCount;
        _attRange = attRange;
        _coolTime = coolTime;
        _durTime = durTime;
        _shotSpeed = shotSpeed;
    }
    /*
    public void UpdateDamage(int damage) { _damage = damage; }
    public void UpdateAttCount(int attCount) { _attCount = attCount; }
    public void UpdateAttRange(float attRange) { _attRange = attRange; }
    public void UpdateCoolTime(float cumTime) { _coolTime = cumTime; }
    public void UpdateDurTime(float durTime) { _durTime = durTime; }
    public void UpdateShotSpeed(float shotSpeed) { _shotSpeed = shotSpeed; }
    */
    public void UpdateStat(int level, int damage, int attCount, float attRange, float coolTime, float durTime, float shotSpeed)
    {
        _level = level;
        _damage = damage;
        _attCount = attCount;
        _attRange = attRange;
        _coolTime = coolTime;
        _durTime = durTime;
        _shotSpeed = shotSpeed;
    }

    public void SetPassiveSynergy(PASSIVE_TYPE type)
    {
        _synergyType = type;
    }

}

public class SkillManager : MonoBehaviour
{
    //List<Skill> _skills;
    Dictionary<string, Skill> _skills;
    List<string> _skillNames = new List<string>();

    int _attIncrease;
    int _rangeIncrease;
    int _coolTimeDecrease;

    List<Skill> _skillList = new List<Skill>();
    [SerializeField] List<AttackController> _attackControllers;

    private void Awake()
    {
        _skillList.Add(new Skill("기본 베기",1, 5, 1, 0.8f, 2f, 0, 0));
        _skillList.Add(new Skill("회전구",0, 4, 2, 0.7f, 5f, 1f, 1f));
        _skillList.Add(new Skill("회전 베기", 0, 5, 1, 1f, 9f, 1f, 0f));
        _skillList.Add(new Skill("화살 공격", 0, 4, 1, 0.7f, 6f, 1.5f, 0.7f));
        _skillList.Add(new Skill("부메랑", 0, 5, 1, 0.8f, 5, 0, 0));
        _skillList.Add(new Skill("운석 충돌", 0, 9, 1, 0.8f, 7, 0, 0));

        _skillList[0].SetPassiveSynergy(PASSIVE_TYPE.MoveSpeed_Up);
        _skillList[1].SetPassiveSynergy(PASSIVE_TYPE.Def_Up);
        _skillList[2].SetPassiveSynergy(PASSIVE_TYPE.Range_Up);
        _skillList[3].SetPassiveSynergy(PASSIVE_TYPE.Exp_Up);
        _skillList[4].SetPassiveSynergy(PASSIVE_TYPE.CoolTime_Down);
        _skillList[5].SetPassiveSynergy(PASSIVE_TYPE.Attack_Up);


        for (int i = 0; i < _skillList.Count; i++)
            _skillNames.Add(_skillList[i]._name);
        UIManager.Instance.SetGiftNames(_skillNames);
        _skills = new Dictionary<string, Skill>();
        SetSkillAwake();

        _attIncrease = 0;
        _rangeIncrease = 0;
        _coolTimeDecrease = 0;
    }
    private void Start()
    {
        for (int i = 0; i < _attackControllers.Count; i++)
            _attackControllers[i].SetSkill(_skillList[i]);
    }
    void SetSkillAwake()
    {
        /*
         *  _damage = 4;
                _durTime = 1.5f;
                _attRange = 0.7f;
                _coolTime = 6f;
                _shotSpeed = 0.7f;
                _attCount = 1;
        */
        for (int i = 0; i < _skillList.Count; i++)
            _skills.Add(_skillList[i]._name, _skillList[i]);
    }
    public void UpdateStat(string name, int level, int damage, int attCount, float attRange, float coolTime, float durTime, float shotSpeed)
    {
        _skills[name].UpdateStat(level, damage, attCount, attRange, coolTime, durTime, shotSpeed);
    }
    public void UpgradeLevel(string name)
    { 
        _skills[name]._level++; 
        foreach(var skill in _attackControllers)
        {
            if(skill._name == name)
                skill.UpdateStat(_skills[name]);
        }
    }
    public List<string> GetNames() { return _skillNames; }
    public int GetLevel(string name) { return _skills[name]._level; }
    public int GetDamage(string name) { return _skills[name]._damage + (int)(_skills[name]._damage * 0.01f * _attIncrease); }
    public int GetAttCount(string name) { return _skills[name]._attCount; }
    public float GetAttRange(string name) { return _skills[name]._attRange + (int)(_skills[name]._attRange * 0.01f * _rangeIncrease); }
    public float GetCoolTime(string name) { return _skills[name]._coolTime + (_skills[name]._coolTime * 0.01f * _coolTimeDecrease); }
    public float GetDurTime(string name) { return _skills[name]._durTime; }
    public float GetShotSpeed(string name) { return _skills[name]._shotSpeed; }

    public bool IsMaxLevel(string name)
    {
        if (_skills[name]._level >= 5)
            return true;
        else
            return false;
    }
    Skill FindSkillByName(string name)
    {
        foreach(var skill in _skillList)
        {
            if (skill._name == name)
                return skill;
        }
        return null;
    }
    public void UpdatePassiveStat(PASSIVE_TYPE type)
    {
        switch(type)
        {
            case PASSIVE_TYPE.Attack_Up:
                _attIncrease += 5;
                break;
            case PASSIVE_TYPE.Range_Up:
                _rangeIncrease += 5;
                break;
            case PASSIVE_TYPE.CoolTime_Down:
                _coolTimeDecrease -= 5;
                break;
        }
    }
}
