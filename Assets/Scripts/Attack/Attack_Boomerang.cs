using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack_Boomerang : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Boomerang _damageBox;

    [SerializeField] SkillManager _skillManager;

    [Header("거리"), SerializeField] float _distance = 15f;
    [Header("돌아가는 거리"), SerializeField] float _backDistance = 30f;
    [Header("각도"), SerializeField] float _angle = 30f;
    [Header("속도"), SerializeField] float _speed = 10;
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

        GameObject damageBox = Instantiate(_damageBoxObj, transform.position, Quaternion.identity);

        _damageBox = damageBox.GetComponent<DamageBox_Boomerang>();

        _damageBox.UpdateDamage(_damage);
        _damageBox.UpdateIsMaxLevel(_isMaxLevel);
        _damageBox.SetAngle(_angle);
        _damageBox.SetPlayerTrs(transform);
        _damageBox.SetSpeed(_speed);
        _damageBox.SetDistance(_backDistance);
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
