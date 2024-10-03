using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CChar
{
    public int _id { get; set; }
    public string _charName { get; set; }
    public string _prefName { get; set; }
    public string _modelName { get; set; }
    public string _baseSkillName { get; set; }
    public string _skill1 { get; set; }
    public string _skill2 { get; set; }
    public int _price { get; set; }
    public bool _isPurchased = false;   
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

    SkillData _data;

    CChar _selectedChar = new CChar();

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
        _data = data;
        _charList = _data._CharacterDatas;

        _selectedChar = _charList[0];
        LobbyUIManager.Instance.SpawnModelObj(_selectedChar._modelName);
        LobbyUIManager.Instance.SetProductInfos(_charList);
    }
    public void SetSelectedChar(CChar selectedChar)
    {
        _selectedChar = selectedChar;
        _selected = _selectedChar._charName;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(CRT_SetSkillAwake());
    }
    IEnumerator CRT_SetSkillAwake()
    {
        yield return new WaitForSeconds(0.5f); //���� �� ����
        _playerTrs = SkillManager.Instance._PlayerTrs;
        SkillManager.Instance.SetData(_data);
        SetCharacter();
        SkillManager.Instance.SetSkillAwake();
        
    }
    void SetCharacter()
    {
        GameObject selectedCharacter = Instantiate(GetObjByName(_selectedChar._prefName), _playerTrs);

        AnimController animController = _playerTrs.GetComponent<AnimController>();
        animController.SetAnimator();


        CharPrefInfo charPrefInfo = selectedCharacter.GetComponent<CharPrefInfo>();
        
        charPrefInfo.SetPlayerTrs(_playerTrs);
        Attack charAttack = charPrefInfo.GetCharAttack();
        SkillManager.Instance.SetCharSkillAwake(charAttack, _selectedChar);
        SkillManager.Instance.SetSkillMethod(charPrefInfo._Skill1, charPrefInfo._Skill2);
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
