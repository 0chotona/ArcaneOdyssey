using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PossesdItemInfo : MonoBehaviour
{
    [Header("아이콘"), SerializeField] Image _itemIcon;
    [Header("레벨"), SerializeField] TextMeshProUGUI _levelText;

    public Image _ItemIcon => _itemIcon;

    [SerializeField] string _name;
    int _level;
    public int _Level => _level;

    eSKILL _skill;
    eBUFF_TYPE _buff;
    public void SetIcon(Sprite sprite)
    {
        _itemIcon.sprite = sprite;
    }
    public void SetLevel(bool isSkill)
    {
        if(isSkill)
        {
            CSkill cSkill = SkillManager.Instance._SkillDic[_skill];
            _name = cSkill._skillText;
            _level = cSkill._level;
        }
        else
        {
            CBuff cBuff = BuffManager.Instance._Passives[_buff];
            _name = cBuff._name;
            _level = cBuff._level;
        }
        

        _levelText.text = _level.ToString();
    }
    public void SetItem(eSKILL skill)
    {
        _skill = skill;
    }
    public void SetItem(eBUFF_TYPE buff)
    {
        _buff = buff;
    }
    public void SetName(string name)
    {
        _name = name;
    }
    public bool IsFoundItem(string foundName)
    {
        return _name == foundName;
    }
}
