using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftInfo : MonoBehaviour
{
    [Header("스킬 이미지"), SerializeField] Image _skillImage;
    [Header("이름 텍스트"), SerializeField] TextMeshProUGUI _nameText;
    [Header("레벨 텍스트"),SerializeField] TextMeshProUGUI _levelText;
    [Header("설명 텍스트"), SerializeField] TextMeshProUGUI _descText;

    CSkill _skill;
    CBuff _passive;

    string _name;
    public string _Name => _name;
    string _level;

    public void SetGiftImage(Sprite sprite)
    {
        _skillImage.sprite = sprite;
    }
    public void SetInfoText(string name, int level)
    {
        _name = name;
        _level = level.ToString();
        

        _nameText.text = _name;
        _levelText.text = "LV. " + _level;
    }
    public void SetDescText(string desc)
    {
        _descText.text = desc;
    }
}
