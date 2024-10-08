using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bow : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Bow _damageBox;

    float _destroyTime = 5f;

    private void Awake()
    {
        

        _name = eSKILL.Bow;
    }
    
    public override void AttackInteract()
    {
        float startAngle = (_attCount - 1) * 7.5f; // 각도 계산 수정
        for (int i = 0; i < _attCount; i++)
        {
            float angle = startAngle - i * 15; // 각도 계산 수정
            Quaternion rotation = Quaternion.Euler(0, angle, 0); // 각도를 회전으로 변환

            Vector3 direction;

            if (_attCount == 1)
            {
                direction = transform.forward;
            }
            else
            {
                direction = rotation * transform.forward;
            }

            //Vector3 gap = direction.normalized * _shotDistance; // 방향에 거리를 곱하여 간격 벡터 생성

            GameObject damageBox = Instantiate(_damageBoxObj, transform.position, Quaternion.Euler(direction));
            _damageBox = damageBox.GetComponent<DamageBox_Bow>();
            _damageBox.SetDirection(direction);
            _damageBox.UpdateDamage(_damage);

            Destroy(damageBox, _destroyTime);
        }
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime - _coolTime * BuffController.Instance._CoolTimeBuff);
            AttackInteract();
        }
    }

    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attCount = stat._attCount;
        _coolTime = stat._coolTime;

        
    }

    public override void StartAttack()
    {
        StartCoroutine(CRT_Attack());
    }
}
