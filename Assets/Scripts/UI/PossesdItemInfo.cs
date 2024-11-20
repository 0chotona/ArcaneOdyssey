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
    public string _Level => _levelText.text;

    string _name;
    public string _Name => _name;
    public void SetIcon(Sprite sprite)
    {
        _itemIcon.sprite = sprite;
    }
    public void SetLevel(int level)
    {
        _levelText.text = level.ToString();
    }
    public void SetLevel()
    {

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
