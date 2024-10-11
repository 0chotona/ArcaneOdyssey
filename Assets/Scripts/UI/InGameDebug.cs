using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InGameDebug : MonoBehaviour
{
    [Header("��ų ���׷��̵� ��ư"), SerializeField] List<Button> _skillUpgradeButtons;
    [Header("���� ���׷��̵� ��ư"), SerializeField] List<Button> _buffUpgradeButtons;


    [Header("����� ��ư"), SerializeField] Button _debugButton;
    [Header("����� �г�"), SerializeField] GameObject _debugPanel;

    [SerializeField] BuffManager _buffManager;
   
    private void Awake()
    {
        _debugButton.onClick.AddListener(() => Click_Debug());
        for (int i = 0; i < _buffUpgradeButtons.Count; i++)
        {
            int index = i; // Ŭ���� ������ ���ϱ� ���� �ӽ� ���� ���
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
        _buffManager.UpgradeLevel(name);
    }
    void Click_Debug()
    {
        _debugPanel.SetActive(!_debugPanel.activeSelf);
    }
}
