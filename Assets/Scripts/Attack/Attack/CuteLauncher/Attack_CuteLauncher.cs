using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_CuteLauncher : Attack
{
    [Header("������ �ڽ�"), SerializeField] GameObject _damageBoxObj;
    [Header("�Ÿ�"), SerializeField] float _distance = 15f;

    [Header("�ӵ�"), SerializeField] float _speed = 10f;
    [Header("�� ����"), SerializeField] EnemySensor _enemySensor;

    [Header("�߻� ����"), SerializeField] float _gap = 0.1f;
    [Header("�߻� Ƚ��"), SerializeField] int _shootCount = 8;

    [Header("�߻� ��ġ"), SerializeField] Transform _shootTrs;

    DamageBox_CuteLauncher _damageBox;
    private void OnEnable()
    {
        _name = eSKILL.CuteLauncher;
    }

    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _projectileCount = stat._projectileCount;
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
                for (int i = 0; i < _projectileCount + _buffStat._ProjectileCount; i++)
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
        Vector3 dir = (nearestEnemy != null) ? (nearestEnemy.position - _shootTrs.position).normalized : _shootTrs.forward;
        Vector3 targetPos = _shootTrs.position + dir * _distance;

        Vector3 spawnPos = _shootTrs.position;
        spawnPos.y = 1f;

        GameObject damageBox = Instantiate(_damageBoxObj, spawnPos, Quaternion.identity);

        _damageBox = damageBox.GetComponent<DamageBox_CuteLauncher>();

        float finalDamage = _damage + _damage * _buffStat._Att;
        bool isCritical = SkillManager.Instance.IsCritical(0f);
        if (isCritical)
        {
            finalDamage *= SkillManager.Instance._BaseCriDmg;
        }
        _damageBox.UpdateDamage(finalDamage);
        _damageBox.UpdateSpeed(_speed);

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

    public override void SetPlayerTrs(Transform playerTrs) { }
}
