using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    [Header("��ǰ"), SerializeField] GiftInfo[] _giftInfos;
    [SerializeField] Button[] _skillButtons;

    [Header("�ַ� ��ǰ �г�"), SerializeField] GameObject _soloUpgradePanel;
    [Header("�ַ� ��ǰ"), SerializeField] GiftInfo _soloGiftInfo;
    [Header("�ַ� ��ǰ ��ư"), SerializeField] Button _soloSkillButton;

    [Header("Ÿ�̸� �ؽ�Ʈ"), SerializeField] TextMeshProUGUI _timerText;

    [Header("ĳ���� �ʻ�ȭ"), SerializeField] Image _charImage;

    [Header("Hp ��"), SerializeField] Slider _hpSlider;
    //[Header("Hp �� �� �̹���"),SerializeField] Image _fillImage;

    [Header("����ġ ��"), SerializeField] Slider _expSlider;
    [Header("���� ��"), SerializeField] Slider _shieldSlider;
    [Header("E ��ų ��Ÿ�ӹ�"), SerializeField] Image _imgECooltime;
    [Header("R ��ų ��Ÿ�ӹ�"), SerializeField] Image _imgRCooltime;

    [Header("���� ��ų"), SerializeField] List<GameObject> _skillInfoesObj;
    [Header("���� ����"), SerializeField] List<GameObject> _buffInfoesObj;
    List<PossesdItemInfo> _skillInfoes = new List<PossesdItemInfo>();
    List<PossesdItemInfo> _buffInfoes = new List<PossesdItemInfo>();
    /*
    [Header("��ų ������"), SerializeField] List<Image> _skillIcons;
    [Header("��ų ������ â"), SerializeField] List<GameObject> _skillIconsPanels;

    [Header("���� ������"), SerializeField] List<Image> _buffIcons;
    [Header("���� ������ â"), SerializeField] List<GameObject> _buffIconsPanels;
    */
    [Header("��ų ������"), SerializeField] SkillGage _skillGame;

    [Header("��� â"), SerializeField] GameObject _resultPanel;
    [Header("�������� ��ȣ �ؽ�Ʈ"), SerializeField] TextMeshProUGUI _resultStage;
    //[Header("�׽�ī��Ʈ �ؽ�Ʈ"), SerializeField] TextMeshProUGUI _resultDeathCount;
    [Header("��� ��ų ������"), SerializeField] List<Image> _resultSkillIcons;
    [Header("��� ���� ������"), SerializeField] List<Image> _resultBuffIcons;

    [Header("��ų E ������"), SerializeField] Image _skill1Icon;
    [Header("��ų R ������"), SerializeField] Image _skill2Icon;

    [Header("On/Off�� UI"), SerializeField] List<GameObject> _toggleableUIList;
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
    public void SetGiftNames(List<CSkill> skills) //�������ڸ��� ��ǰ �̸� ����
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
    public void SetCharIcon(string iconName)
    {
        if(GetAddressable.Instance._CharIconDic.TryGetValue(iconName, out Sprite charSprite))
        {
            _charImage.sprite = charSprite;
        }
    }
    public void ShowUpgradePanel()
    {
        int roof = 0;

        _upgradePanel.SetActive(true);

        if(_giftNames.Count < 3)
        {
            for (int i = 0; i < 3 - _giftNames.Count; i++)
                _giftNames.Add("ü�� ȸ��");

            
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
        // ������ _giftNames�� �����ϰ� ���纻���� �۾�
        List<string> giftNamesToKeep = new List<string>();

        // possedSkills���� ��� �̸��� ������ Set�� ����
        HashSet<string> skillNamesSet = new HashSet<string>();
        foreach (CSkill skill in possedSkills)
        {
            skillNamesSet.Add(skill._skillText);
        }

        // _giftNames���� possedSkills�� ���� �̸��� ����
        foreach (string name in _giftNames)
        {
            if (skillNamesSet.Contains(name) || !IsSkill(name))
            {
                giftNamesToKeep.Add(name);
            }
        }

        // ���� ����Ʈ�� ���ο� ����Ʈ�� ��ü
        _giftNames = giftNamesToKeep;
    }
    public void RemoveEntireBuffNames(List<CBuff> possedSkills)
    {
        // ������ _giftNames�� �����ϰ� ���纻���� �۾�
        List<string> giftNamesToKeep = new List<string>();

        // possedSkills���� ��� �̸��� ������ Set�� ����
        HashSet<string> buffNamesSet = new HashSet<string>();
        foreach (CBuff skill in possedSkills)
        {
            buffNamesSet.Add(skill._name);
        }

        // _giftNames���� possedSkills�� ���� �̸��� ����
        foreach (string name in _giftNames)
        {
            if (buffNamesSet.Contains(name) || IsSkill(name))
            {
                giftNamesToKeep.Add(name);
            }
        }

        // ���� ����Ʈ�� ���ο� ����Ʈ�� ��ü
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
        if (_skillNames.Contains(name)) //_skills�� ���Եƴ���
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
        if (level5Skills.Count > 0) //���� 5�� ��ų�� ������ ������
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
            if(rndValue < rate) //��ų
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
            else //����
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
}
