using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStat : MonoBehaviour
{
    static BuffStat _instance;

    public static BuffStat Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<BuffStat>();
            return _instance;
        }

    }
    [SerializeField] SkillManager _skillManager;
    [SerializeField] PlayerItem _playerItem;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMove _playerMove;

    float _attBuff;
    public float _AttBuff => _attBuff;
    float _defBuff;
    public float _DefBuff => _defBuff;
    float _maxHpBuff;
    public float _MaxHpBuff => _maxHpBuff;
    float _hpRegenBuff;
    public float _HpRegenBuff => _hpRegenBuff;
    float _moveSpeedBuff;
    public float _MoveSpeedBuff => _moveSpeedBuff;
    float _itemPickRangeBuff;
    public float _ItemPickRangeBuff => _itemPickRangeBuff;
    float _rangeBuff;
    public float _RangeBuff => _rangeBuff;
    float _durBuff;
    public float _DurBuff => _durBuff;
    float _criRateBuff;
    public float _CriRateBuff => _criRateBuff;

    float _coolTimeBuff;
    public float _CoolTimeBuff => _coolTimeBuff;
    float _expGainBuff;
    public float _ExpGainBuff => _expGainBuff;

    [Header("기본 치명타"), SerializeField] float  _baseCriRate = 0.05f;
    [Header("치명타 데미지"), SerializeField] float _baseCriDmg = 1.5f;
    public float _BaseCriDmg => _baseCriDmg;

    public void UpdateBuffStat(eBUFF_TYPE type, float value)
    {
        switch (type)
        {
            case eBUFF_TYPE.Attack_Up:
                _attBuff += value;
                break;
            case eBUFF_TYPE.Def_Up:
                _defBuff += value;
                break;
            case eBUFF_TYPE.MaxHP_Up:
                _maxHpBuff += value;
                _playerHealth.UpdateMaxHpBuff(_maxHpBuff);
                break;
            case eBUFF_TYPE.HPRegen_Up:
                _hpRegenBuff += value;
                _playerHealth.UpdateHpRegenBuff(_hpRegenBuff);
                break;
            case eBUFF_TYPE.MoveSpeed_Up:
                _moveSpeedBuff += value;
                break;
            case eBUFF_TYPE.ItemPickupRange_Up:
                _itemPickRangeBuff += value;
                _playerItem.UpdateItemPickupRange(_itemPickRangeBuff);
                break;
            case eBUFF_TYPE.Range_Up:
                _rangeBuff += value;
                break;
            case eBUFF_TYPE.Duration_Up:
                _durBuff += value;
                break;
            case eBUFF_TYPE.CriRate_Up:
                _criRateBuff += value;
                break;
            case eBUFF_TYPE.CoolTime_Down:
                _coolTimeBuff += value;
                break;
            case eBUFF_TYPE.ExpGain_Up:
                _expGainBuff += value;
                _playerItem.UpdateExpGain(_expGainBuff);
                break;
        }
    }
    public bool IsCritical()
    {
        float rnd = Random.Range(0f, 1f);
        if (rnd < _baseCriRate + _CriRateBuff)
        {
            Debug.Log("치명타 터짐 / _baseCriRate : " + _baseCriRate + "_CriRateBuff : " + _CriRateBuff + " rnd : " + rnd);
            
            return true;
        }
        else
            return false;
    }
}
