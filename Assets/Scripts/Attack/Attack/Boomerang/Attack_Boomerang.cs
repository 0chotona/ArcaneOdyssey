using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
피해량: 피해량이 증가합니다.
스킬 가속: 재사용 대기시간이 감소합니다.
치명타 확률: 치명타가 적중할 확률이 증가합니다.
투사체 개수: 투사체 수가 증가합니다.
 * */
public class Attack_Boomerang : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Boomerang _damageBox;

    [SerializeField] SkillManager _skillManager;

    [Header("거리"), SerializeField] float _distance = 8f;
    [Header("돌아가는 거리"), SerializeField] float _backDistance = 16f;
    [Header("작은 투사체 각도"), SerializeField] float _angle = 30f;
    [Header("작은 투사체 거리"), SerializeField] float _smallDistance = 8f;

    [Header("속도"), SerializeField] float _speed = 20f;
    [Header("적 센서"), SerializeField] EnemySensor _enemySensor;

    [Header("간격"), SerializeField] float _gap = 0.1f;

    private void OnEnable()
    {
        _name = eSKILL.Boomerang;
    }

    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attCount = stat._attCount;
        _attRange = stat._attRange;
        _coolTime = stat._coolTime;
        if (_level >= 6)
        {
            _isMaxLevel = true;
        }
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
            if (_level > 0)
            {
                for (int i = 0; i < _attCount + _buffStat._ProjectileCount; i++)
                {
                    AttackInteract();
                    yield return new WaitForSeconds(_gap);
                }
            }
        }
    }

    
    public override void AttackInteract()
    {
        Transform nearestEnemy = _enemySensor.GetNearestEnemy();
        Vector3 dir = (nearestEnemy != null) ? (nearestEnemy.position - transform.position).normalized : transform.forward;
        Vector3 targetPos = transform.position + dir * _distance;

        Vector3 spawnPos = transform.position;
        spawnPos.y = 1f;

        GameObject damageBox = Instantiate(_damageBoxObj, spawnPos, Quaternion.identity);

        _damageBox = damageBox.GetComponent<DamageBox_Boomerang>();

        float finalDamage = _damage + _damage * _buffStat._Att;
        bool isCritical = SkillManager.Instance.IsCritical();
        if (isCritical)
        {
            finalDamage *= SkillManager.Instance._BaseCriDmg;
        }
        _damageBox.UpdateDamage(finalDamage);
        _damageBox.UpdateIsMaxLevel(_isMaxLevel);
        _damageBox.SetAngle(_angle);
        _damageBox.SetPlayerTrs(transform);
        _damageBox.SetSpeed(_speed);
        _damageBox.SetDistance(_backDistance);
        _damageBox.SetSmallDistance(_smallDistance);

        targetPos.y = 1f;
        _damageBox.Shot(targetPos);
    }
    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override void StartAttack()
    {
        StartCoroutine(CRT_Attack());
    }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }
}
