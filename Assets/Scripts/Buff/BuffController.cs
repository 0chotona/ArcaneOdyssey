using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    static BuffController _instance;

    public static BuffController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<BuffController>();
            return _instance;
        }

    }
    [SerializeField] SkillManager _skillManager;
    [SerializeField] PlayerItem _playerItem;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMove _playerMove;

    float _attBuff;
    public float _AttBuff => _attBuff;
    float _rangeBuff;
    public float _RangeBuff => _rangeBuff;
    float _coolTimeBuff;
    public float _CoolTimeBuff => _coolTimeBuff;
    float _moveSpeedBuff;
    public float _MoveSpeedBuff => _moveSpeedBuff;
    

    public void UpdateBuffStat(eBUFF_TYPE type, float value)
    {
        switch (type)
        {
            case eBUFF_TYPE.Attack_Up:
                _attBuff += value;
                break;
            case eBUFF_TYPE.Range_Up:
                _rangeBuff += value;
                break;
            case eBUFF_TYPE.CoolTime_Down:
                _coolTimeBuff += value;
                break;
            case eBUFF_TYPE.MoveSpeed_Up:
                _moveSpeedBuff += value;
                break;
        }
    }
}
