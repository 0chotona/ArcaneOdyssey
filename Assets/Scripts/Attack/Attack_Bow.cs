using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bow : AttackController
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Bow _damageBox;


    Skill _skill;

    private void Awake()
    {
        StartCoroutine(CRT_Attack());

        _name = "화살 공격";
    }
    
    public override void Attack()
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
            _damageBox.UpdateSpeed(_shotSpeed);
            _damageBox.UpdateScale(_attRange);

            Destroy(damageBox, _durTime);
        }
    }
    public override void UpdateStat(Skill skill)
    {
        _level = skill._level;
        switch (_level)
        {
            case 1:
                _damage = 4;
                _durTime = 1.5f;
                _attRange = 0.7f;
                _coolTime = 6f;
                _shotSpeed = 0.7f;
                _attCount = 1;
                break;
            case 2:
                _damage = 6;
                _durTime = 2f;
                _attRange = 0.7f;
                _coolTime = 6f;
                _shotSpeed = 0.7f;
                _attCount = 1;
                break;
            case 3:
                _damage = 6;
                _durTime = 3f;
                _attRange = 1f;
                _coolTime = 6f;
                _shotSpeed = 1f;
                _attCount = 3;
                break;
            case 4:
                _damage = 8;
                _durTime = 1.5f;
                _attRange = 1f;
                _coolTime = 5.5f;
                _shotSpeed = 1f;
                _attCount = 3;
                break;
            case 5:
                _damage = 8;
                _durTime = 2f;
                _attRange = 1.3f;
                _coolTime = 5.5f;
                _shotSpeed = 1.3f;
                _attCount = 5;
                break;
            case 6:
                _damage = 12;
                _durTime = 3f;
                _attRange = 1.3f;
                _coolTime = 5f;
                _shotSpeed = 1.3f;
                _attCount = 8;
                break;
        }
        //_damageBox.UpdateDamage(_damage);
        skill.UpdateStat(_level, _damage, _attCount, _attRange, _coolTime, _durTime, _shotSpeed);
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime);
            Attack();
        }
    }

    public override void SetSkill(Skill skill)
    {
        _skill = skill;
    }
}
