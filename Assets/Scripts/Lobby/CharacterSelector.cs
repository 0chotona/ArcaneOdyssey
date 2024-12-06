using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum eCHARACTER
{
    Momoi,
    Chise
}
public class CChar
{
    public int _id { get; set; }
    public string _charName { get; set; }
    public eCHARACTER _charType { get; set; }
    public string _prefName { get; set; }
    public string _modelName { get; set; }
    public string _baseSkillName { get; set; }
    public string _skill1 { get; set; }
    public string _skill2 { get; set; }
    public int _price { get; set; }
    public bool _isPurchased = false;

    string _iconEName;
    public string _IconEName => _iconEName;
    string _iconRName;
    public string _IconRName => _iconRName;
    public void SetActiveIconName(string iconE, string iconR)
    {
        _iconEName = iconE;
        _iconRName = iconR;
    }
}
public class CharacterSelector : MonoBehaviour
{
    static CharacterSelector _instance;

    public static CharacterSelector Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<CharacterSelector>();
            return _instance;
        }

    }
    List<CChar> _charList = new List<CChar>();

    CharacterData _charData;
    SkillData _skillData;
    CharSkillData _charSkillData;

    CChar _selectedChar = new CChar();
    public CChar _SelectedChar => _selectedChar;

    [SerializeField] string _selected;

    [Header("ĳ���� ������"), SerializeField] List<GameObject> _characterPrefs;
    

    Transform _playerTrs;
    private void Awake()
    {
        var obj = FindObjectsOfType<CharacterSelector>();
        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    public void SetData(SkillData data)
    {
        _skillData = data;
    }
    public void SetData(CharSkillData data)
    {
        _charSkillData = data;
    }
    public void SetData(CharacterData data)
    {
        _charData = data;
        _charList = _charData._CharacterDatas;

        _selectedChar = _charList[0];
        LobbyUIManager.Instance.SpawnModelObj(_selectedChar._modelName);
        LobbyUIManager.Instance.SetProductInfos(_charList);
    }
    /*
    public void SetData(EnemyData data)
    {
        _enemyData = data;
    }
    */
    public void SetSelectedChar(CChar selectedChar)
    {
        _selectedChar = selectedChar;
        _selected = _selectedChar._charName;
    }
    public void StartGame()
    {
        StartCoroutine(CRT_SetSkillAwake());
    }
    IEnumerator CRT_SetSkillAwake()
    {
        yield return new WaitForSeconds(0.5f); //���� �� ����
        GameManager.Instance.SetCharacter(_selectedChar);
        //GameManager.Instance.SetMob(_enemyData);
        _playerTrs = SkillManager.Instance._PlayerTrs;
        SkillManager.Instance.SetData(_skillData);
        SkillManager.Instance.SetData(_charSkillData);
        SetCharacter();
        SkillManager.Instance.SetSkillAwake();
        //UIManager.Instance.SetCharIcon(_selectedChar._modelName);
        UIManager.Instance.SetSkillIcon(_selectedChar._IconEName, _selectedChar._IconRName);
    }
    void SetCharacter()
    {
        GameObject selectedCharacter = Instantiate(GetObjByName(_selectedChar._prefName), _playerTrs);

        AnimController animController = _playerTrs.GetComponent<AnimController>();
        animController.SetAnimator();


        CharPrefInfo charPrefInfo = selectedCharacter.GetComponent<CharPrefInfo>();
        
        Attack charAttack = charPrefInfo.GetCharAttack();
        charPrefInfo.SetPlayerTrs(_playerTrs);
        charAttack.SetPlayerTrs(_playerTrs);
        SkillManager.Instance.SetCharSkillAwake(charAttack, _selectedChar);
        SkillManager.Instance.SetSkillMethod(charPrefInfo._Skill1, charPrefInfo._Skill2);
        PassiveManager.Instance.SetPassiveMethod(charPrefInfo._Passive);
        PassiveManager.Instance.SetSelectedChar(_selectedChar);
    }
    GameObject GetObjByName(string name)
    {
        GameObject selectedCharacter = null;
        foreach (GameObject obj in _characterPrefs)
        {
            if (obj.name == name)
            {
                selectedCharacter = obj;
                break;
            }
        }
        return selectedCharacter;
    }

    
}
