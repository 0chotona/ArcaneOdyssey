using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameDebug : MonoBehaviour
{
    [Header("���׷��̵� ��ư"), SerializeField] GameObject _upgradeButtons;

    [Header("��ų �׷�"), SerializeField] Transform _skillTrs;
    [Header("���� �׷�"), SerializeField] Transform _buffTrs;

    [Header("��ų ���׷��̵� ��ư"), SerializeField] List<Button> _skillUpgradeButtons;
    [Header("���� ���׷��̵� ��ư"), SerializeField] List<Button> _buffUpgradeButtons;


    [Header("����� ��ư"), SerializeField] Button _debugButton;
    [Header("����� �г�"), SerializeField] GameObject _debugPanel;

    [Header("x ����"), SerializeField] float _xDist = 160f;
    [Header("y ����"), SerializeField] float _yDist = 60f;

    [Header("x ����"), SerializeField] float _xGap = 20f;
    [Header("y ����"), SerializeField] float _yGap = 10f;

   
    private void Start()
    {

        StartCoroutine(CRT_CreateButton());




        _debugPanel.SetActive(false);
        /*
        _buffUpgradeButtons[0].onClick.AddListener(() => UpgradeBuff(_buffManager._Passives.ElementAt(0).Value._name));
        _buffUpgradeButtons[1].onClick.AddListener(() => UpgradeBuff("���� ����"));
        _buffUpgradeButtons[2].onClick.AddListener(() => UpgradeBuff("�ִ�ü�� ����"));
        _buffUpgradeButtons[3].onClick.AddListener(() => UpgradeBuff("ü����� ����"));
        _buffUpgradeButtons[4].onClick.AddListener(() => UpgradeBuff("�̵��ӵ� ����"));
        _buffUpgradeButtons[5].onClick.AddListener(() => UpgradeBuff("ȹ����� ����"));
        _buffUpgradeButtons[6].onClick.AddListener(() => UpgradeBuff("���� ���� ����"));
        _buffUpgradeButtons[7].onClick.AddListener(() => UpgradeBuff("���ӽð� ����"));
        _buffUpgradeButtons[8].onClick.AddListener(() => UpgradeBuff("ġ��Ÿ Ȯ�� ����"));
        _buffUpgradeButtons[9].onClick.AddListener(() => UpgradeBuff("��Ÿ�� ����"));
        _buffUpgradeButtons[10].onClick.AddListener(() => UpgradeBuff("����ġ ȹ�淮 ����"));
        _buffUpgradeButtons[11].onClick.AddListener(() => UpgradeBuff("����ü ����"));
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
