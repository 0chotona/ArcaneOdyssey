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

    [Header("ĳ���� ������"), SerializeField] List<GameObject> _characterPrefs;
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
         * 1. SkillManager���� List<GameObject> _attackObjs ����.
         * 2. ĳ���� ������ 2�� ����� (Attack�� ��ӹ޴� Ŭ������ ������Ʈ�� ���ϰ� �ִ� ���� ������Ʈ A ����)
         * 3. A�� SkillManager�� _attacks�� �ֱ�
         * 4. _attackObjs�� ������Ʈ Attack�� eSKILL���� _name�� _skillDic.ElementAt(i).Value._skillName�� ���� ������� _attacks�� �ֱ�
         * 5. AddSkill(_selectedSkill._skillName)�� �Ἥ _possedSkills�� _selectedSkill�ֱ�
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
