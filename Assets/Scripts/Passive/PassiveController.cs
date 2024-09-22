using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveController : MonoBehaviour
{

    [SerializeField] SkillManager _skillManager;
    [SerializeField] PlayerItem _playerItem;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMove _playerMove;

    public void UpdateStat(ePASSIVE_TYPE type)
    {
        switch(type)
        {
            case ePASSIVE_TYPE.Attack_Up:
            case ePASSIVE_TYPE.Range_Up:
            case ePASSIVE_TYPE.CoolTime_Down:
                SkillManager.Instance.UpdatePassiveStat(type);
                break;
            case ePASSIVE_TYPE.Def_Up:
                _playerHealth.UpdatePassiveStat();
                break;
            case ePASSIVE_TYPE.MoveSpeed_Up:
                _playerMove.UpdatePassiveStat();
                break;
        }
    }
}
