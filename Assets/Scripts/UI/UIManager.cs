using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();
            return _instance;
        }

    }

    [SerializeField] SkillManager _skillManager;
    [SerializeField] PassiveManager _passiveManager;

    [SerializeField] GameObject _upgradePanel;
    [SerializeField] TextMeshProUGUI[] _giftTexts;
    [SerializeField] TextMeshProUGUI[] _levelTexts;
    [SerializeField] Button[] _skillButtons;

    [SerializeField] TextMeshProUGUI _timerText;

    float _curTime = 60;

    public bool _isPause = false;

    List<string> _skillNames = new List<string>();
    List<string> _passiveNames = new List<string>();

    List<string> _giftNames = new List<string>();
    private void Start()
    {
        

        

        _skillButtons[0].onClick.AddListener(() => OnSelectSkill(0));
        _skillButtons[1].onClick.AddListener(() => OnSelectSkill(1));
        _skillButtons[2].onClick.AddListener(() => OnSelectSkill(2));
    }
    private void Update()
    {
        _curTime -= Time.deltaTime;

        _timerText.text = ((int)_curTime).ToString();
    }
    public void SetGiftNames(List<string> skillNames)
    {
        _skillNames = skillNames;
        _passiveNames = _passiveManager._PassiveNames;
        _giftNames.AddRange(_skillNames);
        _giftNames.AddRange(_passiveNames);
    }
    public void ShowUpgradePanel()
    {
        int roof = 0;

        _upgradePanel.SetActive(true);

        if(_giftNames.Count < 3)
        {
            for (int i = 0; i < 3 - _giftNames.Count; i++)
                _giftNames.Add("체력 회복");

            
        }
        Debug.Log(_giftNames.Count);
        List<int> rndList = new List<int>();
        int rnd = Random.Range(0, _giftNames.Count);

        for (int i = 0; i < _giftTexts.Length;) //3
        {

            if (rndList.Contains(rnd))
                rnd = Random.Range(0, _giftNames.Count);
            else
            {
                rndList.Add(rnd);
                i++;
            }

            roof++;
            if(roof > 100)
            {
                Debug.Log("2");
                break;
            }
        }
        for(int i = 0; i < _giftTexts.Length; i++)
        {
            string giftName = _giftNames[rndList[i]];
            _giftTexts[i].text = giftName;
            _levelTexts[i].text = IsSkill(giftName) ? "LV. " + (_skillManager.GetLevel(giftName) + 1) : 
                                                                    "LV. " + (_passiveManager.GetLevel(giftName) + 1);
        }
        Time.timeScale = 0;

    }
    void OnSelectSkill(int index)
    {
        Time.timeScale = 1;

        string upgradeGiftName = _giftTexts[index].text;
        if (IsSkill(upgradeGiftName))
            _skillManager.UpgradeLevel(upgradeGiftName);
        else
            _passiveManager.UpgradeLevel(upgradeGiftName);

        if(IsSkill(upgradeGiftName))
        {
            if (_skillManager.IsMaxLevel(upgradeGiftName))
                _giftNames.Remove(upgradeGiftName);
        }
        else
        {
            if (_passiveManager.IsMaxLevel(upgradeGiftName))
                _giftNames.Remove(upgradeGiftName);
        }
            
        _upgradePanel.SetActive(false);
    }
    public void SetTimerString(int timer)
    {
        _curTime = timer;
    }
    public IEnumerator CRT_BossCountDown()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        
    }
    bool IsSkill(string name)
    {
        if (_skillNames.Contains(name))
            return true;
        else if (_passiveNames.Contains(name))
            return false;

        return false;
    }
}
