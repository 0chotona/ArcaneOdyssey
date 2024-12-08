using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    [SerializeField] GameObject _upgradePanel;
    [Header("물품"), SerializeField] GiftInfo[] _giftInfos;
    [SerializeField] Button[] _skillButtons;

    [Header("솔로 물품 패널"), SerializeField] GameObject _soloUpgradePanel;
    [Header("솔로 물품"), SerializeField] GiftInfo _soloGiftInfo;
    [Header("솔로 물품 버튼"), SerializeField] Button _soloSkillButton;

    [Header("타이머 텍스트"), SerializeField] TextMeshProUGUI _timerText;

    [Header("캐릭터 초상화"), SerializeField] Image _charImage;

    [Header("Hp 바"), SerializeField] Slider _hpSlider;
    //[Header("Hp 바 필 이미지"),SerializeField] Image _fillImage;

    [Header("경험치 바"), SerializeField] Slider _expSlider;
    [Header("쉴드 바"), SerializeField] Slider _shieldSlider;
    [Header("E 스킬 쿨타임바"), SerializeField] Image _imgECooltime;
    [Header("R 스킬 쿨타임바"), SerializeField] Image _imgRCooltime;

    [Header("보유 스킬"), SerializeField] List<GameObject> _skillInfoesObj;
    [Header("보유 버프"), SerializeField] List<GameObject> _buffInfoesObj;
    List<PossesdItemInfo> _skillInfoes = new List<PossesdItemInfo>();
    List<PossesdItemInfo> _buffInfoes = new List<PossesdItemInfo>();
    /*
    [Header("스킬 아이콘"), SerializeField] List<Image> _skillIcons;
    [Header("스킬 아이콘 창"), SerializeField] List<GameObject> _skillIconsPanels;

    [Header("버프 아이콘"), SerializeField] List<Image> _buffIcons;
    [Header("버프 아이콘 창"), SerializeField] List<GameObject> _buffIconsPanels;
    */
    [Header("스킬 게이지"), SerializeField] SkillGage _skillGame;

    [Header("결과 창"), SerializeField] GameObject _resultPanel;
    [Header("스테이지 번호 텍스트"), SerializeField] TextMeshProUGUI _resultStage;
    //[Header("테스카운트 텍스트"), SerializeField] TextMeshProUGUI _resultDeathCount;
    [Header("결과 스킬 아이콘"), SerializeField] List<Image> _resultSkillIcons;
    [Header("결과 버프 아이콘"), SerializeField] List<Image> _resultBuffIcons;

    [Header("스킬 E 아이콘"), SerializeField] Image _skill1Icon;
    [Header("스킬 R 아이콘"), SerializeField] Image _skill2Icon;

    [Header("On/Off할 UI"), SerializeField] List<GameObject> _toggleableUIList;


    [Header("조이스틱 터치"), SerializeField] MoveJoystick _moveJoy;
    public SkillGage _SkillGage => _skillGame;


    public bool _isPause = false;

    List<CSkill> _skills = new List<CSkill>();
    List<CBuff> _passives = new List<CBuff>();

    List<string> _giftNames = new List<string>();

    List<string> _skillNames = new List<string>();
    List<string> _passiveNames = new List<string>();

    int _skillCount = 0;
    int _buffCount = 0; 
    
    private void Start()
    {


        _expSlider.value = 0f;

        _skillButtons[0].onClick.AddListener(() => OnSelectSkill(0));
        _skillButtons[1].onClick.AddListener(() => OnSelectSkill(1));
        _skillButtons[2].onClick.AddListener(() => OnSelectSkill(2));

        _soloSkillButton.onClick.AddListener(() => Click_CloseSoloPanel());

        _imgECooltime.fillAmount = 0;
        _imgRCooltime.fillAmount = 0;
        /*
        foreach (GameObject panel in _skillIconsPanels)
        {
            panel.SetActive(false);
        }
        foreach (GameObject panel in _buffIconsPanels)
        {
            panel.SetActive(false);
        }
        */
        foreach (GameObject obj in _skillInfoesObj)
        {
            PossesdItemInfo itemInfo = obj.GetComponent<PossesdItemInfo>();
            obj.SetActive(false);
        }
        foreach (GameObject obj in _buffInfoesObj)
        {
            PossesdItemInfo itemInfo = obj.GetComponent<PossesdItemInfo>();
            obj.SetActive(false);
        }
        _resultPanel.SetActive(false);
        _upgradePanel.SetActive(false);
        _soloUpgradePanel.SetActive(false);
    }
    private void Update()
    {


        if(Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.UpgradeLevel();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            UpgradeLevel5();
        }
    }
    public void StartTimer()
    {
        StartCoroutine(CRT_SetTimer());
    }
    IEnumerator CRT_SetTimer()
    {
        int min = 0;
        int sec = 0;
        string timerText = min.ToString("D2") + ":" + sec.ToString("D2");
        _timerText.text = timerText;

        while(true)
        {
            yield return new WaitForSeconds(1f);
            sec++;
            if(sec >= 60)
            {
                min++;
                sec = 0;
            }
            timerText = min.ToString("D2") + ":" + sec.ToString("D2");
            _timerText.text = timerText;
        }
    }
    public void SetGiftNames(List<CSkill> skills) //시작하자마자 상품 이름 세팅
    {
        _skills = skills;
        _passives = BuffManager.Instance._PassiveNames;
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
    /*
    public void SetCharIcon(string iconName)
    {
        if(GetAddressable.Instance._CharIconDic.TryGetValue(iconName, out Sprite charSprite))
        {
            _charImage.sprite = charSprite;
            if(iconName == null)
                Debug.Log("iconName null");
            if (charSprite == null)
                Debug.Log("charSpr null");
            //Debug.Log("SetCharIcon Load / " + iconName + " / " + charSprite.name);
        }
        else
        {
            Debug.Log("SetCharIcon Can't Load / "  + iconName);
        }
    }
    */
    public void SetCharIcon()
    {
        Sprite tmpSprite = null;

        Dictionary<string, Sprite> charSpriteDic = GetAddressable.Instance._CharIconDic;
        string charIconName = CharacterSelector.Instance._SelectedChar._modelName;
        tmpSprite = charSpriteDic[charIconName];
        _charImage.sprite = tmpSprite;
        /*
        if (GetAddressable.Instance._CharIconDic.TryGetValue(CharacterSelector.Instance._SelectedChar._modelName, out Sprite charSprite))
        {
            Debug.Log(CharacterSelector.Instance._SelectedChar._modelName);
            
            if (charSprite == null)
                Debug.Log("charSpr null");
            else
                Debug.Log(charSprite.name);
            
            _charImage.sprite = charSprite;
            
            if(_charImage.sprite == null)
            {
                Debug.Log("_charImage is null");
            }
            else
            {
                Debug.Log("_charImage.sprite = " +_charImage.sprite.name);
            }
            
            Debug.Log("SetCharIcon Load / " + iconName + " / " + charSprite.name);
        }
        */
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
            int giftLevel = IsSkill(giftName) ? SkillManager.Instance.GetLevel(giftName) + 1 : BuffManager.Instance.GetLevel(giftName) + 1;
            string iconName = IsSkill(giftName) ? SkillManager.Instance.GetIconNameByName(giftName) : BuffManager.Instance.GetIconNameByBuffName(giftName);


            
            if (GetAddressable.Instance._SkillIconDic.TryGetValue(iconName, out Sprite skillSprite))
            {
                _giftInfos[i].SetGiftImage(skillSprite);
            }
            else if (GetAddressable.Instance._BuffIconDic.TryGetValue(iconName, out Sprite buffSprite))
            {
                _giftInfos[i].SetGiftImage(buffSprite);
            }


            _giftInfos[i].SetInfoText(giftName, giftLevel);
            if (IsSkill(giftName))
            {
                _giftInfos[i].SetPanelBack(true);
            }
            else
            {
                _giftInfos[i].SetPanelBack(false);
            }
        }
        Time.timeScale = 0;

    }
    
    void OnSelectSkill(int index)
    {
        Time.timeScale = 1;

        SoundManager.Instance.PlaySound(eUISOUNDTYPE.GetItem);

        string upgradeGiftName = _giftInfos[index]._Name;
        if (IsSkill(upgradeGiftName))
        {
            SkillManager.Instance.UpgradeLevel(SkillManager.Instance.GetSkillByName(upgradeGiftName));
        }
        else
        {
            BuffManager.Instance.UpgradeLevel(upgradeGiftName);
        }
        foreach(PossesdItemInfo itemInfo in _skillInfoes)
        {
            itemInfo.SetLevel(true);
        }
        foreach (PossesdItemInfo itemInfo in _buffInfoes)
        {
            itemInfo.SetLevel(false);
        }
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
            if (BuffManager.Instance.IsMaxLevel(upgradeGiftName))
                _giftNames.Remove(upgradeGiftName);
        }
    }
    public void RemoveEntireSkillNames(List<CSkill> possedSkills)
    {
        // 현재의 _giftNames를 안전하게 복사본으로 작업
        List<string> giftNamesToKeep = new List<string>();

        // possedSkills에서 모든 이름을 가져와 Set에 저장
        HashSet<string> skillNamesSet = new HashSet<string>();
        foreach (CSkill skill in possedSkills)
        {
            skillNamesSet.Add(skill._skillText);
        }

        // _giftNames에서 possedSkills에 없는 이름을 유지
        foreach (string name in _giftNames)
        {
            if (skillNamesSet.Contains(name) || !IsSkill(name))
            {
                giftNamesToKeep.Add(name);
            }
        }

        // 기존 리스트를 새로운 리스트로 교체
        _giftNames = giftNamesToKeep;
    }
    public void RemoveEntireBuffNames(List<CBuff> possedSkills)
    {
        // 현재의 _giftNames를 안전하게 복사본으로 작업
        List<string> giftNamesToKeep = new List<string>();

        // possedSkills에서 모든 이름을 가져와 Set에 저장
        HashSet<string> buffNamesSet = new HashSet<string>();
        foreach (CBuff skill in possedSkills)
        {
            buffNamesSet.Add(skill._name);
        }

        // _giftNames에서 possedSkills에 없는 이름을 유지
        foreach (string name in _giftNames)
        {
            if (buffNamesSet.Contains(name) || IsSkill(name))
            {
                giftNamesToKeep.Add(name);
            }
        }

        // 기존 리스트를 새로운 리스트로 교체
        _giftNames = giftNamesToKeep;
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
        float xPos = -(_hpSlider.GetComponent<RectTransform>().sizeDelta.x * (1 - (curHp / maxHp)));
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
        float fill = 0f;
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
        float fill = 0f;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            fill = curTime / coolTime;
            _imgRCooltime.fillAmount = fill;
            yield return null;
        }

    }
    public void UpdatePossesedIcon(CSkill cSkill)
    {
        _skillInfoesObj[_skillCount].SetActive(true);
        PossesdItemInfo itemInfo = _skillInfoesObj[_skillCount].GetComponent<PossesdItemInfo>();
        if (GetAddressable.Instance._SkillIconDic.TryGetValue(cSkill._iconName, out Sprite iconSprite))
        {
            itemInfo.SetIcon(iconSprite);

        }
        itemInfo.SetItem(cSkill._skillName);
        //itemInfo.SetLevel(cSkill._level);
        //itemInfo.SetName(cSkill._skillText);
        //SetSkillIcon(_skillCount, cSkill._iconName);
        _skillCount++;
        _skillInfoes.Add(itemInfo);
    }
    public void UpdatePossesedIcon(CBuff cBuff)
    {
        _buffInfoesObj[_buffCount].SetActive(true);
        PossesdItemInfo itemInfo = _buffInfoesObj[_buffCount].GetComponent<PossesdItemInfo>();
        if (GetAddressable.Instance._BuffIconDic.TryGetValue(cBuff._IconName, out Sprite iconSprite))
        {
            itemInfo.SetIcon(iconSprite);

        }
        itemInfo.SetItem(cBuff._buffType);
        //itemInfo.SetLevel(cBuff._level);
        //itemInfo.SetName(cBuff._name);
        _buffCount++;
        _buffInfoes.Add(itemInfo);
    }

    public void UpgradeLevel5()
    {
        List<CSkill> level5Skills = new List<CSkill>();
        foreach (CSkill skill in SkillManager.Instance._SkillDic.Values)
        {
            if (skill._level == 5)
            {
                if (BuffManager.Instance._Passives[skill._synergyType]._level > 0)
                {
                    level5Skills.Add(skill);
                }
                
            }
        }

        int rnd = 0;
        if (level5Skills.Count > 0) //레벨 5인 스킬을 가지고 있을때
        {
            rnd = Random.Range(0, level5Skills.Count);
            eSKILL eSkill = level5Skills[rnd]._skillName;
            SkillManager.Instance.UpgradeLevel(eSkill);
            ShowSoloUpgradePanel(eSkill);
        }
        else
        {
            float possesSkillCount = SkillManager.Instance._PossedSkills.Count;
            float possesBuffCount = BuffManager.Instance._PossedBuffs.Count;
            float rate = possesSkillCount / (possesSkillCount + possesBuffCount);
            float rndValue = Random.value;
            if(rndValue < rate) //스킬
            {
                rnd = 0; 
                List<CSkill> upgradSkills = new List<CSkill>();
                foreach (CSkill skill in SkillManager.Instance._PossedSkills.Values)
                {
                    
                    upgradSkills.Add(skill);
                }
                rnd = Random.Range(0, upgradSkills.Count);
                SkillManager.Instance.UpgradeLevel(upgradSkills[rnd]._skillName);
                ShowSoloUpgradePanel(upgradSkills[rnd]._skillName);
            }
            else //버프
            {
                List<eBUFF_TYPE> upgradBuffs = new List<eBUFF_TYPE>();
                foreach (eBUFF_TYPE buff in BuffManager.Instance._PossedBuffs)
                {
                    
                    upgradBuffs.Add(buff);
                    
                }
                rnd = Random.Range(0, upgradBuffs.Count);
                string buffName = BuffManager.Instance.GetNameByType(upgradBuffs[rnd]);
                BuffManager.Instance.UpgradeLevel(buffName);
                ShowSoloUpgradePanel(upgradBuffs[rnd]);
            }
            
            
        }
    }
    void ShowSoloUpgradePanel(eSKILL skillType)
    {
        _soloUpgradePanel.SetActive(true);

        CSkill skill = SkillManager.Instance._SkillDic[skillType];

        string giftName = skill._skillText;
        int giftLevel = skill._level;
        string iconName = skill._iconName;


        if (GetAddressable.Instance._SkillIconDic.TryGetValue(iconName, out Sprite skillSprite))
        {
            _soloGiftInfo.SetGiftImage(skillSprite);
        }
        _soloGiftInfo.SetInfoText(giftName, giftLevel);
        _soloGiftInfo.SetPanelSoloBack();
        Time.timeScale = 0;

    }
    void ShowSoloUpgradePanel(eBUFF_TYPE buffType)
    {
        _soloUpgradePanel.SetActive(true);

        CBuff buff = BuffManager.Instance._Passives[buffType];

        string giftName = buff._name;
        int giftLevel = buff._level;
        string iconName = buff._IconName;


        if (GetAddressable.Instance._BuffIconDic.TryGetValue(iconName, out Sprite buffSprite))
        {
            _soloGiftInfo.SetGiftImage(buffSprite);
        }
        _soloGiftInfo.SetInfoText(giftName, giftLevel);
        _soloGiftInfo.SetPanelSoloBack();
        Time.timeScale = 0;

    }
    void Click_CloseSoloPanel()
    {
        Time.timeScale = 1f;

        SoundManager.Instance.PlaySound(eUISOUNDTYPE.GetItem);

        foreach (PossesdItemInfo itemInfo in _skillInfoes)
        {
            itemInfo.SetLevel(true);
        }
        foreach (PossesdItemInfo itemInfo in _buffInfoes)
        {
            itemInfo.SetLevel(false);
        }
        _soloUpgradePanel.SetActive(false);

    }
    public void ShowResult(int stageNum, int deathCount)
    {
        _resultPanel.SetActive(true);
        _resultStage.text = "STAGE " + stageNum.ToString();
        for(int i = 0; i < _skillInfoesObj.Count; i++)
        {
            PossesdItemInfo itemInfo = _skillInfoesObj[i].GetComponent<PossesdItemInfo>();
            if(itemInfo._ItemIcon.sprite != null)
            {
                _resultSkillIcons[i].sprite = itemInfo._ItemIcon.sprite;
            }
        }
        for (int i = 0; i < _buffInfoesObj.Count; i++)
        {
            PossesdItemInfo itemInfo = _buffInfoesObj[i].GetComponent<PossesdItemInfo>();
            if (itemInfo._ItemIcon.sprite != null)
            {
                _resultBuffIcons[i].sprite = itemInfo._ItemIcon.sprite;
            }
        }
    }
    public void SetSkillIcon(string iconE, string iconR)
    {
        if (GetAddressable.Instance._SkillIconDic.TryGetValue(iconE, out Sprite skillESprite))
        {
            _skill1Icon.sprite = skillESprite;
        }
        if (GetAddressable.Instance._SkillIconDic.TryGetValue(iconR, out Sprite skillRSprite))
        {
            _skill2Icon.sprite = skillRSprite;
        }
    }
    public void SetUIActive(bool isActive)
    {
        foreach(GameObject go in _toggleableUIList)
        {
            go.SetActive(isActive);
        }
    }
    public void SetCanDragJoy(bool canDrag)
    {
        _moveJoy.SetCanDrag(canDrag);
    }

}
