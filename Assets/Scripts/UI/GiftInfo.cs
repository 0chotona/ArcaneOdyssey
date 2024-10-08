using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftInfo : MonoBehaviour
{
    [Header("�̸� �ؽ�Ʈ"), SerializeField] TextMeshProUGUI _nameText;
    [Header("���� �ؽ�Ʈ"),SerializeField] TextMeshProUGUI _levelText;
    [Header("���� �ؽ�Ʈ"), SerializeField] TextMeshProUGUI _descText;

    CSkill _skill;
    CBuff _passive;

    string _name;
    public string _Name => _name;
    string _level;

    public void SetText(string name, int level)
    {
        _name = name;
        _level = level.ToString();
        

        _nameText.text = _name;
        _levelText.text = "LV. " + _level;
    }
}
