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
    List<CChar> _charList = new List<CChar>();

    SkillData _data;

    CChar _selectedChar = new CChar();

    [SerializeField] string _selected;

    [Header("캐릭터 프리펩"), SerializeField] List<GameObject> _characterPrefs;
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
        yield return new WaitForSeconds(0.5f);
        SkillManager.Instance.SetSkillAwake(_data, _selectedChar);
        SetCharacter();
    }
    void SetCharacter()
    {
        GameObject selectedCharacter = Instantiate(GetObjByName(_selectedChar._prefName), SkillManager.Instance._PlayerTrs);
        /*
         * 1. SkillManager에서 List<GameObject> _attackObjs 선언.
         * 2. 캐릭터 프리펩 2개 만들기 (Attack을 상속받는 클래스를 컴포넌트에 지니고 있는 게임 오브젝트 A 보유)
         * 3. A를 SkillManager의 _attacks에 넣기
         * 4. _attackObjs의 컴포넌트 Attack의 eSKILL변수 _name과 _skillDic.ElementAt(i).Value._skillName를 비교해 순서대로 _attacks에 넣기
         * 5. AddSkill(_selectedSkill._skillName)를 써서 _possedSkills에 _selectedSkill넣기
         */

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
