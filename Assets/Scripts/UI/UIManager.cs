using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
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

    [SerializeField] BuffManager _passiveManager;

    [SerializeField] GameObject _upgradePanel;
    [Header("물품"), SerializeField]
    GiftInfo[] _giftInfos;

    [SerializeField] Button[] _skillButtons;

    [SerializeField] TextMeshProUGUI _timerText;

    [Header("Hp 바"), SerializeField] Slider _hpSlider;
    //[Header("Hp 바 필 이미지"),SerializeField] Image _fillImage;

    [Header("경험치 바"), SerializeField] Slider _expSlider;
    [Header("쉴드 바"), SerializeField] Slider _shieldSlider;
    [Header("E 스킬 쿨타임바"), SerializeField] Image _imgECooltime;
    [Header("R 스킬 쿨타임바"), SerializeField] Image _imgRCooltime;

    float _curTime = 60;

    public bool _isPause = false;

    List<CSkill> _skills = new List<CSkill>();
    List<CBuff> _passives = new List<CBuff>();

    List<string> _giftNames = new List<string>();

    List<string> _skillNames = new List<string>();
    List<string> _passiveNames = new List<string>();
    private void Start()
    {


        _expSlider.value = 0f;

        _skillButtons[0].onClick.AddListener(() => OnSelectSkill(0));
        _skillButtons[1].onClick.AddListener(() => OnSelectSkill(1));
        _skillButtons[2].onClick.AddListener(() => OnSelectSkill(2));

        _imgECooltime.fillAmount = 0;
        _imgRCooltime.fillAmount = 0;
    }
    private void Update()
    {
        _curTime -= Time.deltaTime;

        _timerText.text = ((int)_curTime).ToString();
    }
    public void SetGiftNames(List<CSkill> skills)
    {
        _skills = skills;
        _passives = _passiveManager._PassiveNames;
        foreach (CSkill skill in _skills)
        {
            _giftNames.Add(skill._skillText);
            _skillNames.Add(skill._skillText);
        }

        foreach (CBuff passive in _passives)
        {
            _giftNames.Add(passive._name);
            _passiveNames.Add(passive._name);
        }
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

        for (int i = 0; i < _giftInfos.Length;) //3
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
        for(int i = 0; i < _giftInfos.Length; i++)
        {
            string giftName = _giftNames[rndList[i]];
            int giftLevel = IsSkill(giftName) ? SkillManager.Instance.GetLevel(giftName) + 1 : _passiveManager.GetLevel(giftName) + 1;
            _giftInfos[i].SetText(giftName, giftLevel);
        }
        Time.timeScale = 0;

    }
    void OnSelectSkill(int index)
    {
        Time.timeScale = 1;

        string upgradeGiftName = _giftInfos[index]._Name;
        if (IsSkill(upgradeGiftName))
            SkillManager.Instance.UpgradeLevel(SkillManager.Instance.GetSkillByName(upgradeGiftName));
        else
            _passiveManager.UpgradeLevel(upgradeGiftName);

        RemoveGiftName(upgradeGiftName);
            
        _upgradePanel.SetActive(false);
    }
    public void RemoveGiftName(string upgradeGiftName)
    {
        if (IsSkill(upgradeGiftName))
        {
            if (SkillManager.Instance.IsMaxLevel(SkillManager.Instance.GetSkillByName(upgradeGiftName)))
                _giftNames.Remove(upgradeGiftName);
        }
        else
        {
            if (_passiveManager.IsMaxLevel(upgradeGiftName))
                _giftNames.Remove(upgradeGiftName);
        }
    }
    public void SetTimerString(int timer)
    {
        _curTime = timer;
    }
    public IEnumerator CRT_BossCountDown()
    {
        for(int i = 0; i < 10; i++)
        {
            Debug.Log(10 - i);
            yield return new WaitForSeconds(1f);
        }
        
    }
    bool IsSkill(string name)
    {
        if (_skillNames.Contains(name)) //_skills에 포함됐는지
            return true;
        else if (_passiveNames.Contains(name))
            return false;

        return false;
    }
    public void UpdateHpBar(float maxHp, float curHp)
    {
        _hpSlider.maxValue = maxHp;
        _hpSlider.value = curHp;

        RectTransform shieldRectTransform = _shieldSlider.GetComponent<RectTransform>();

        Vector2 newPosition = shieldRectTransform.anchoredPosition;
        float xPos = -(maxHp * (1 - (curHp / maxHp)));
        newPosition.x = xPos;
        shieldRectTransform.anchoredPosition = newPosition;
    }
    public void UpdateExpBar(float maxExp, float curExp)
    {
        _expSlider.maxValue = maxExp;
        _expSlider.value = curExp;
    }
    public void UpdateShieldBar(float amount)
    {
        _shieldSlider.maxValue = _hpSlider.maxValue / 5f;
        if(amount > _shieldSlider.maxValue)
            amount = _shieldSlider.maxValue;
        _shieldSlider.value = amount;
    }
    public void StartECooltime(float coolTime)
    {
        StartCoroutine(CRT_ECoolTime(coolTime));
    }
    IEnumerator CRT_ECoolTime(float coolTime)
    {
        float curTime = coolTime;
        float fill = 0;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            fill = curTime / coolTime;
            _imgECooltime.fillAmount = fill;
            yield return null;
        }
        
    }
    public void StartRCooltime(float coolTime)
    {
        StartCoroutine(CRT_RCoolTime(coolTime));
    }
    IEnumerator CRT_RCoolTime(float coolTime)
    {
        float curTime = coolTime;
        float fill = 0;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            fill = curTime / coolTime;
            _imgRCooltime.fillAmount = fill;
            yield return null;
        }

    }
}
