using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuffStat
{
    float _att;
    public float _Att => _att;
    float _def;
    public float _Def => _def;
    float _maxHp;
    public float _MaxHp => _maxHp;
    float _hpRegen;
    public float _HpRegen => _hpRegen;
    float _moveSpeed;
    public float _MoveSpeed => _moveSpeed;
    float _itemPickRange;
    public float _ItemPickRange => _itemPickRange;
    float _range;
    public float _Range => _range;
    float _dur;
    public float _Dur => _dur;
    float _criRate;
    public float _CriRate => _criRate;

    float _coolTime;
    public float _CoolTime => _coolTime;
    float _expGain;
    public float _ExpGain => _expGain;
    float _projectileCount;
    public float _ProjectileCount => _projectileCount;
    public void UpdateAtt(float att) { _att += att; }
    public void UpdateDef(float def) { _def += def; }
    public void UpdateMaxHp(float maxHp) { _maxHp += maxHp; }
    public void UpdateHpRegen(float hpRegen) { _hpRegen += hpRegen; }
    public void UpdateMoveSpeed(float moveSpeed) { _moveSpeed += moveSpeed; }
    public void UpdateItemPickRange(float itemPickRange) { _itemPickRange += itemPickRange; }
    public void UpdateRange(float range) { _range += range; }
    public void UpdateDur(float dur) { _dur += dur; }
    public void UpdateCriRate(float criRate) { _criRate += criRate; }
    public void UpdateCoolTime(float coolTime) { _coolTime += coolTime; }
    public void UpdateExpGain(float expGain) { _expGain += expGain; }
    public void UpdateProjectileCount(float projectileCount) { _projectileCount += projectileCount; }
}
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

    CBuffStat _buffStat = new CBuffStat();
    public CBuffStat _BuffStat => _buffStat;

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
    float _projectileCountBuff;
    public float _ProjectileCountBuff => _projectileCountBuff;

    public void UpdateBuffStat(eBUFF_TYPE type, float value)
    {
        switch (type)
        {
            case eBUFF_TYPE.Attack_Up:
                _buffStat.UpdateAtt(value);
                break;
            case eBUFF_TYPE.Def_Up:
                _buffStat.UpdateDef(value);
                break;
            case eBUFF_TYPE.MaxHP_Up:
                _buffStat.UpdateMaxHp(value);
                _playerHealth.UpdateMaxHpBuff(_buffStat._MaxHp);
                break;
            case eBUFF_TYPE.HPRegen_Up:
                _buffStat.UpdateHpRegen(value);
                _playerHealth.UpdateHpRegenBuff(_buffStat._HpRegen);
                break;
            case eBUFF_TYPE.MoveSpeed_Up:
                _buffStat.UpdateMoveSpeed(value);
                break;
            case eBUFF_TYPE.ItemPickupRange_Up:
                _buffStat.UpdateItemPickRange(value);
                _playerItem.UpdateItemPickupRange(_buffStat._ItemPickRange);
                break;
            case eBUFF_TYPE.Range_Up:
                _buffStat.UpdateRange(value);
                break;
            case eBUFF_TYPE.Duration_Up:
                _buffStat.UpdateDur(value);
                break;
            case eBUFF_TYPE.CriRate_Up:
                _buffStat.UpdateCriRate(value);
                break;
            case eBUFF_TYPE.CoolTime_Down:
                _buffStat.UpdateCoolTime(value);
                break;
            case eBUFF_TYPE.ExpGain_Up:
                _buffStat.UpdateExpGain(value);
                _playerItem.UpdateExpGain(_buffStat._ExpGain);
                break;
            case eBUFF_TYPE.ProjectileCount_Up:
                _buffStat.UpdateProjectileCount(value);
                break;
        }
        SkillManager.Instance.SetBuffStat(_buffStat);
    }
}
