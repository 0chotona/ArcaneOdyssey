using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ���ط� 20 / 45 / 70 / 95 / 120
���� 8 / 8.5 / 9 / 9.5 / 10
���� 725 / 775 / 825 / 875 / 925
*/
public class Attack_GatlingRabbitGun : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_GatlingRabbitGun _damageBox;

    [Header("����"), SerializeField] float _angle = 10f;


    [Header("ȸ�� �ӵ�"), SerializeField] float _spinSpeed = 30f;
    [Header("�Ÿ�"), SerializeField] float _distance = 6f;

    [Header("������ ����"), SerializeField] float _gap = 0.3f;

    [Header("���ο� ����"), SerializeField] float _slowRate = 0.5f;
    [Header("���ο� �ð�"), SerializeField] float _slowDur = 2f;
    [Header("�߰� ������ ����"), SerializeField] float _slowDamageRate = 0.5f;

    [Header("�߻� ��ġ"), SerializeField] Transform _shootTrs;
    [Header("��ƼŬ"), SerializeField] ParticleSystem _particle;

    [SerializeField] Transform _playerTrs;
    private void Awake()
    {

        _damageBox = _damageBoxObj.GetComponent<DamageBox_GatlingRabbitGun>();
        _name = eSKILL.GatlingRabbitGun;
        _particle.Stop(true);
    }

    public override void AttackInteract()
    {
        _damageBox.SetIsMaxLevel(_isMaxLevel);
        float finalDamage = _damage + (_damage * _buffStat._Att);
        _damageBox.UpdateDamage(finalDamage);
        _damageBox.SetSlow(_slowRate, _slowDur);
        _damageBox.SetAngle(_angle + _angle * _buffStat._Range, _distance + _distance * _buffStat._Range);
        _damageBox.SetSlowDamage(_slowDamageRate);


        StartCoroutine(CRT_Spin());

    }

    public override IEnumerator CRT_Attack()
    {
        _shootTrs.SetParent(null);
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
        _particle.Play(true);
        while (elapsedTime < _durTime + _durTime * _buffStat._Dur)
        {
            // y�� �������� ȸ��
            _shootTrs.Rotate(0, _spinSpeed * Time.deltaTime, 0);
            _shootTrs.position = _playerTrs.position;
            elapsedTime += Time.deltaTime;
            interactTimer += Time.deltaTime;

            // _gap �ʰ� �����ٸ� _damageBox.GiveInteract() ����
            if (interactTimer >= _gap)
            {
                _damageBox.GiveInteract();
                interactTimer = 0f; // Ÿ�̸� �ʱ�ȭ
            }

            yield return null;
        }
        _particle.Stop(true);
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
