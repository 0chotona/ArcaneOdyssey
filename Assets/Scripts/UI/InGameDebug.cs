using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InGameDebug : MonoBehaviour
{
    [Header("스킬 업그레이드 버튼"), SerializeField] List<Button> _skillUpgradeButtons;
    [Header("버프 업그레이드 버튼"), SerializeField] List<Button> _buffUpgradeButtons;


    [Header("디버그 버튼"), SerializeField] Button _debugButton;
    [Header("디버그 패널"), SerializeField] GameObject _debugPanel;

    [SerializeField] BuffManager _buffManager;
   
    private void Awake()
    {
        _debugButton.onClick.AddListener(() => Click_Debug());
        for (int i = 0; i < _buffUpgradeButtons.Count; i++)
        {
            int index = i; // 클로저 문제를 피하기 위해 임시 변수 사용
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
        _buffManager.UpgradeLevel(name);
    }
    void Click_Debug()
    {
        _debugPanel.SetActive(!_debugPanel.activeSelf);
    }
}
