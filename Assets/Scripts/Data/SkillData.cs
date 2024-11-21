using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
/*
 * SharkHunterMine (������� �ؾ� ���)
UrbanTransit (������ ���� ���߱���)
FootprintTrap (���ڱ� �ߵ� ��ġ)
LionRain (������ ��)
EchoBatBlade (�޾Ƹ�ġ�� ����Į��)
T_Ber (Ƽ.��)
GatlingRabbitGun (��Ʋ�� �䳢 ��)
AnimalMine (���� ����)
FlamingLongbow (Ÿ������ �ܱ�)
BunnyMegaExplosion (�䳢 �ʰ��� ����)
StaticSword (����ƽ�� ��)
VortexArmor (�ҿ뵹�� �尩)
YumiBot (���̺�)
Exterminator (������)
*/
public enum eSKILL
{
    MeowGun,
    BrokenWing,
    Boomerang,
    WhirlBlade,
    IceArmor,
    BunnyCrossbow,
    RadiantBarrier,
    CuteLauncher,
    EchoBatBlade,
    GatlingRabbitGun,
    FootprintTrap
}
public enum eSKILL_COLUMN
{
    ID,
    Skill_Name,
    Enum_Name,
    Icon_Name,
    Level,
    Damage,
    AttRange,
    CoolTime,
    DurTime,
    CriRate,
    ProjectileCount,
    SynergyType
}
public enum eCHARSKILL_COLUMN
{
    ID,
    Skill_Name,
    Enum_Name,
    Icon_Name,
    Level,
    Damage,
    AttRange,
    CoolTime,
    DurTime,
    CriRate,
    ProjectileCount,
    SynergyType,
    Char_Pref_Name
}
public enum eCHARACTER_COLUMN
{
    ID,
    Char_Name,
    Char_Type,
    Pref_Name,
    Model_Name,
    BaseSkill_Name,
    Skill1_Name,
    Skill2_Name,
    Price,
    IconE_Name, 
    IconR_Name,
    IsPurchased
}
public interface ICSVDataConverter
{
    void ConvertToDictionary(List<string> rows);
}
public class SkillData : ICSVDataConverter
{
    List<CSkill> _skillDatas = new List<CSkill>();
    public List<CSkill> _SkillDatas => _skillDatas;

    public void ConvertToDictionary(List<string> rows)
    {
        rows.RemoveAt(0); // ù ��(�÷� ���) ����
        rows.RemoveAt(rows.Count - 1);  // ������ ���� �� ����

        Dictionary<int, CSkill> skillDictionary = new Dictionary<int, CSkill>();

        foreach (string row in rows)
        {
            string[] values = row.Split(',');
            int id = int.Parse(values[(int)eSKILL_COLUMN.ID]);
            int level = int.Parse(values[(int)eSKILL_COLUMN.Level]);

            CStat stat = new CStat
            {
                _damage = float.Parse(values[(int)eSKILL_COLUMN.Damage]),
                _attRange = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.AttRange]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.AttRange]),
                _coolTime = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.CoolTime]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.CoolTime]),
                _durTime = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.DurTime]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.DurTime]),
                _criRate = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.CriRate]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.CriRate]),
                _projectileCount = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.ProjectileCount]) ? 0 : int.Parse(values[(int)eSKILL_COLUMN.ProjectileCount])
            };

            if (!skillDictionary.ContainsKey(id))
            {
                CSkill skill = new CSkill
                {
                    _id = id,
                    _level = 0,
                    _skillText = values[(int)eSKILL_COLUMN.Skill_Name],
                    _skillName = (eSKILL)System.Enum.Parse(typeof(eSKILL), values[(int)eSKILL_COLUMN.Enum_Name]),
                    _iconName = values[(int)eSKILL_COLUMN.Icon_Name],
                    _synergyType = (eBUFF_TYPE)System.Enum.Parse(typeof(eBUFF_TYPE), values[(int)eSKILL_COLUMN.SynergyType]),
                    _skillMap = new Dictionary<int, CStat>()
                };
                skill._skillMap.Add(level, stat);
                skillDictionary[id] = skill;
            }
            else
            {
                skillDictionary[id]._skillMap.Add(level, stat);
            }
        }

        _skillDatas = new List<CSkill>(skillDictionary.Values);
    }
}
public class CharSkillData : ICSVDataConverter
{
    List<CSkill> _charSkillDatas = new List<CSkill>();
    public List<CSkill> _CharSkillDatas => _charSkillDatas;
    public void ConvertToDictionary(List<string> rows)
    {
        rows.RemoveAt(0); // ù ��(�÷� ���) ����
        // ������ ���� �� ����
        rows.RemoveAt(rows.Count - 1);

        Dictionary<int, CSkill> skillDictionary = new Dictionary<int, CSkill>();

        for (int i = 0; i < rows.Count; i++)
        {
            string[] values = rows[i].Split(',');
            for (int j = 0; j < values.Length; j++)
                values[j] = values[j].Replace("\r", "");

            int id = int.Parse(values[(int)eCHARSKILL_COLUMN.ID]);
            int level = int.Parse(values[(int)eCHARSKILL_COLUMN.Level]);

            CStat stat = new CStat
            {
                _damage = float.Parse(values[(int)eSKILL_COLUMN.Damage]),
                _attRange = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.AttRange]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.AttRange]),
                _coolTime = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.CoolTime]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.CoolTime]),
                _durTime = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.DurTime]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.DurTime]),
                _criRate = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.CriRate]) ? 0 : float.Parse(values[(int)eSKILL_COLUMN.CriRate]),
                _projectileCount = string.IsNullOrEmpty(values[(int)eSKILL_COLUMN.ProjectileCount]) ? 0 : int.Parse(values[(int)eSKILL_COLUMN.ProjectileCount])
            };

            if (!skillDictionary.ContainsKey(id))
            {
                CSkill skill = new CSkill
                {
                    _id = id,
                    _level = 0,
                    _skillText = values[(int)eCHARSKILL_COLUMN.Skill_Name],
                    _skillName = (eSKILL)System.Enum.Parse(typeof(eSKILL), values[(int)eCHARSKILL_COLUMN.Enum_Name]),
                    _iconName = values[(int)eCHARSKILL_COLUMN.Icon_Name],
                    _synergyType = (eBUFF_TYPE)System.Enum.Parse(typeof(eBUFF_TYPE), values[(int)eCHARSKILL_COLUMN.SynergyType]),
                    _skillMap = new Dictionary<int, CStat>(),
                    _char_Pref_Name = values[(int)eCHARSKILL_COLUMN.Char_Pref_Name]
                };
                skill._skillMap.Add(level, stat);
                skillDictionary[id] = skill;
            }
            else
            {
                skillDictionary[id]._skillMap.Add(level, stat);
            }
        }

        _charSkillDatas = new List<CSkill>(skillDictionary.Values);
    }
}
public class CharacterData : ICSVDataConverter
{
    List<CChar> _characterDatas = new List<CChar>();
    public List<CChar> _CharacterDatas => _characterDatas;

    public void ConvertToDictionary(List<string> rows)
    {
        rows.RemoveAt(0); // ù ��(�÷� ���) ����
        rows.RemoveAt(rows.Count - 1);  // ������ ���� �� ����

        foreach (string row in rows)
        {
            string[] values = row.Split(',');

            CChar character = new CChar
            {
                _id = int.Parse(values[(int)eCHARACTER_COLUMN.ID]),
                _charName = values[(int)eCHARACTER_COLUMN.Char_Name],
                _charType = (eCHARACTER)System.Enum.Parse(typeof(eCHARACTER), values[(int)eCHARACTER_COLUMN.Char_Type]),
                _prefName = values[(int)eCHARACTER_COLUMN.Pref_Name],
                _modelName = values[(int)eCHARACTER_COLUMN.Model_Name],
                _baseSkillName = values[(int)eCHARACTER_COLUMN.BaseSkill_Name],
                _skill1 = values[(int)eCHARACTER_COLUMN.Skill1_Name],
                _skill2 = values[(int)eCHARACTER_COLUMN.Skill2_Name],
                _price = int.Parse(values[(int)eCHARACTER_COLUMN.Price]),
                _isPurchased = (int.Parse(values[(int)eCHARACTER_COLUMN.IsPurchased]) == 1)
            };
            string iconE = values[(int)eCHARACTER_COLUMN.IconE_Name];
            string iconR = values[(int)eCHARACTER_COLUMN.IconR_Name];
            character.SetActiveIconName(iconE, iconR);
            _characterDatas.Add(character);
        }
    }
}