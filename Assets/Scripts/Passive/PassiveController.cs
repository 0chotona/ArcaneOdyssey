using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveController : MonoBehaviour
{

    [SerializeField] SkillManager _skillManager;
    [SerializeField] PlayerItem _playerItem;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMove _playerMove;

    public void UpdateStat(PASSIVE_TYPE type)
    {
        switch(type)
        {
            case PASSIVE_TYPE.Attack_Up:
            case PASSIVE_TYPE.Range_Up:
            case PASSIVE_TYPE.CoolTime_Down:
                _skillManager.UpdatePassiveStat(type);
                break;
            case PASSIVE_TYPE.Exp_Up:
                _playerItem.UpdatePassiveStat();
                break;
            case PASSIVE_TYPE.Def_Up:
                _playerHealth.UpdatePassiveStat();
                break;
            case PASSIVE_TYPE.MoveSpeed_Up:
                _playerMove.UpdatePassiveStat();
                break;
        }
    }
}
