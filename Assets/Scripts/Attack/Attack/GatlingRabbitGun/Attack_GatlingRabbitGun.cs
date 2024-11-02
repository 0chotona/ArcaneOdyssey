using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 피해량 20 / 45 / 70 / 95 / 120
지속 8 / 8.5 / 9 / 9.5 / 10
범위 725 / 775 / 825 / 875 / 925
*/
public class Attack_GatlingRabbitGun : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_GatlingRabbitGun _damageBox;

    [Header("각도"), SerializeField] float _angle = 10f;


    [Header("회전 속도"), SerializeField] float _spinSpeed = 30f;
    [Header("거리"), SerializeField] float _distance = 8f;

    [Header("데미지 간격"), SerializeField] float _gap = 0.3f;

    [Header("슬로우 비율"), SerializeField] float _slowRate = 0.5f;
    [Header("슬로우 시간"), SerializeField] float _slowDur = 2f;
    [Header("추가 데미지 비율"), SerializeField] float _slowDamageRate = 0.5f;

    [Header("발사 위치"), SerializeField] Transform _shootTrs;

    [SerializeField] Transform _playerTrs;
    private void Awake()
    {

        _damageBox = _damageBoxObj.GetComponent<DamageBox_GatlingRabbitGun>();
        _name = eSKILL.GatlingRabbitGun;
    }

    public override void AttackInteract()
    {
        float finalDamage = _damage + (_damage * _buffStat._Att);
        _damageBox.UpdateDamage(finalDamage);
        _damageBox.SetSlow(_slowRate, _slowDur);
        _damageBox.SetAngle(_angle + _angle * _buffStat._Range, _distance + _distance * _buffStat._Range);
        _damageBox.SetSlowDamage(_slowDamageRate);


        StartCoroutine(CRT_Spin());

    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime + _durTime + _durTime * _buffStat._Dur);
            AttackInteract();
        }
    }
    IEnumerator CRT_Spin()
    {
        float elapsedTime = 0f;
        float interactTimer = 0f;

        while (elapsedTime < _durTime + _durTime * _buffStat._Dur)
        {
            // y축 기준으로 회전
            transform.Rotate(0, _spinSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            interactTimer += Time.deltaTime;

            // _gap 초가 지났다면 _damageBox.GiveInteract() 실행
            if (interactTimer >= _gap)
            {
                _damageBox.GiveInteract();
                interactTimer = 0f; // 타이머 초기화
            }

            yield return null;
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
        _coolTime = stat._coolTime;
        _durTime = stat._durTime;
        _attRange = stat._attRange;

        if (_level >= 6)
        {
            _isMaxLevel = true;
        }
    }

    public override void StartAttack()
    {
        StartCoroutine(CRT_Attack());
    }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }
    public override void SetPlayerTrs(Transform playerTrs) 
    {
    }
}
