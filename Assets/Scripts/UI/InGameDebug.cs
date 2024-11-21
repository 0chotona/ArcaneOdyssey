using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameDebug : MonoBehaviour
{
    [Header("업그레이드 버튼"), SerializeField] GameObject _upgradeButtons;

    [Header("스킬 그룹"), SerializeField] Transform _skillTrs;
    [Header("버프 그룹"), SerializeField] Transform _buffTrs;

    [Header("스킬 업그레이드 버튼"), SerializeField] List<Button> _skillUpgradeButtons;
    [Header("버프 업그레이드 버튼"), SerializeField] List<Button> _buffUpgradeButtons;


    [Header("디버그 버튼"), SerializeField] Button _debugButton;
    [Header("디버그 패널"), SerializeField] GameObject _debugPanel;

    [Header("x 길이"), SerializeField] float _xDist = 160f;
    [Header("y 길이"), SerializeField] float _yDist = 60f;

    [Header("x 간격"), SerializeField] float _xGap = 20f;
    [Header("y 간격"), SerializeField] float _yGap = 10f;

   
    private void Start()
    {

        StartCoroutine(CRT_CreateButton());




        _debugPanel.SetActive(false);
        /*
        _buffUpgradeButtons[0].onClick.AddListener(() => UpgradeBuff(_buffManager._Passives.ElementAt(0).Value._name));
        _buffUpgradeButtons[1].onClick.AddListener(() => UpgradeBuff("방어력 증가"));
        _buffUpgradeButtons[2].onClick.AddListener(() => UpgradeBuff("최대체력 증가"));
        _buffUpgradeButtons[3].onClick.AddListener(() => UpgradeBuff("체력재생 증가"));
        _buffUpgradeButtons[4].onClick.AddListener(() => UpgradeBuff("이동속도 증가"));
        _buffUpgradeButtons[5].onClick.AddListener(() => UpgradeBuff("획득범위 증가"));
        _buffUpgradeButtons[6].onClick.AddListener(() => UpgradeBuff("공격 범위 증가"));
        _buffUpgradeButtons[7].onClick.AddListener(() => UpgradeBuff("지속시간 증가"));
        _buffUpgradeButtons[8].onClick.AddListener(() => UpgradeBuff("치명타 확률 증가"));
        _buffUpgradeButtons[9].onClick.AddListener(() => UpgradeBuff("쿨타임 감소"));
        _buffUpgradeButtons[10].onClick.AddListener(() => UpgradeBuff("경험치 획득량 증가"));
        _buffUpgradeButtons[11].onClick.AddListener(() => UpgradeBuff("투사체 증가"));
        */
    }
    void UpgradeSkill(eSKILL name)
    {
        SkillManager.Instance.UpgradeLevel(name);
    }
    void UpgradeBuff(string name)
    {
        BuffManager.Instance.UpgradeLevel(name);
    }
    void UpgradeLevelText(TextMeshProUGUI textMesh, int level)
    {
        textMesh.text = level.ToString();
    }
    void Click_Debug()
    {
        _debugPanel.SetActive(!_debugPanel.activeSelf);
    }
    IEnumerator CRT_CreateButton()
    {
        yield return new WaitForSeconds(0.5f);

        Dictionary<eBUFF_TYPE, CBuff> passives = BuffManager.Instance._Passives;
        RectTransform panelTrs = _debugPanel.GetComponent<RectTransform>();
        panelTrs.sizeDelta = new Vector2(_xGap * 3 + _xDist * 2, (passives.Count + 1) * _yGap + (passives.Count) * _yDist);

        for (int i = 0; i < SkillManager.Instance._SkillDic.Count; i++)
        {
            GameObject button = Instantiate(_upgradeButtons, _skillTrs);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SkillManager.Instance._SkillDic.ElementAt(i).Value._level.ToString();
            button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = SkillManager.Instance._SkillDic.ElementAt(i).Value._skillText;

            _skillUpgradeButtons.Add(button.GetComponent<Button>());

            RectTransform rectTrs = _skillUpgradeButtons[i].GetComponent<RectTransform>();
            Vector3 buttonScale = new Vector3(_xDist, _yDist, 0f);
            rectTrs.sizeDelta = buttonScale;

            Vector3 buttonPos = new Vector3(_xGap, -_yGap - (_yGap + _yDist) * i, 0f);
            rectTrs.anchoredPosition = buttonPos;

            int index = i;
            _skillUpgradeButtons[index].onClick.AddListener(() =>
            UpgradeSkill(SkillManager.Instance._SkillDic.ElementAt(index).Value._skillName));
            _skillUpgradeButtons[index].onClick.AddListener(() =>
            UpgradeLevelText(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>(), SkillManager.Instance._SkillDic.ElementAt(index).Value._level));
        }
        for (int i = 0; i < passives.Count; i++)
        {
            GameObject button = Instantiate(_upgradeButtons, _buffTrs);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = passives.ElementAt(i).Value._level.ToString();
            button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = passives.ElementAt(i).Value._name;

            _buffUpgradeButtons.Add(button.GetComponent<Button>());

            RectTransform rectTrs = _buffUpgradeButtons[i].GetComponent<RectTransform>();
            Vector3 buttonScale = new Vector3(_xDist, _yDist, 0f);
            rectTrs.sizeDelta = buttonScale;

            Vector3 buttonPos = new Vector3(_xGap * 2f + _xDist, -_yGap - (_yGap + _yDist) * i, 0f);
            rectTrs.anchoredPosition = buttonPos;

            int index = i;
            _buffUpgradeButtons[index].onClick.AddListener(() =>
                UpgradeBuff(passives.ElementAt(index).Value._name));
            _buffUpgradeButtons[index].onClick.AddListener(() => 
            UpgradeLevelText(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>(), passives.ElementAt(index).Value._level));
        }
        _debugButton.onClick.AddListener(() => Click_Debug());
        /*
        for (int i = 0; i < _buffUpgradeButtons.Count; i++)
        {
            int index = i; 
            _buffUpgradeButtons[index].onClick.AddListener(() =>
                UpgradeBuff(_buffManager._Passives.ElementAt(index).Value._name)
            );
        }
        for (int i = 0; i < _skillUpgradeButtons.Count; i++)
        {
            int index = i;
            _skillUpgradeButtons[index].onClick.AddListener(() =>
            UpgradeSkill(SkillManager.Instance._SkillDic.ElementAt(index).Value._skillName));
        }
        */
    }
}
