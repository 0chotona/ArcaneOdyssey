using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Attack : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    Momoi_DamageBox_Attack _damageBox;

    [Header("ÃÑ¾Ë ¹ß»ç À§Ä¡"), SerializeField] Transform _shootTrs;
    [Header("ÃÑ¾Ë ÀÌÆåÆ® ¹ß»ç À§Ä¡"), SerializeField] Transform _shootLineTrs;
    [Header("¹ß»ç °£°Ý"), SerializeField] float _shootGap = 0.15f;
    [Header("¹ß»ç ÀÌÆåÆ®"), SerializeField] ParticleSystem _shootEffect;
    [Header("ÃÑ¾Ë µÎ²²"), SerializeField] float _thickness = 0.1f;
    Vector3 _dir;
    [Header("ÃÑ¾Ë °Å¸®"), SerializeField] float _distance = 15f;
    [Header("ÃÑ¾Ë ¼Óµµ"), SerializeField] float _speed = 10f;

    private void OnEnable()
    {
        _name = eSKILL.Gun;
    }
    private void Start()
    {
        StartCoroutine(CRT_Attack());
        _isMaxLevel = false;
        _shootEffect.Stop();
    }
    public override void AttackInteract()
    {
        Vector3 targetPos = _shootTrs.position + _shootTrs.forward * _distance;
        GameObject bullet = Instantiate(_damageBoxObj, _shootTrs.position, _shootTrs.rotation);
        Momoi_DamageBox_Attack damageBox = bullet.GetComponent<Momoi_DamageBox_Attack>();
        damageBox.UpdateDamage(_damage);
        damageBox.UpdateSpeed(_speed);
        damageBox.UpdateIsMaxLevel(_isMaxLevel);
        damageBox.Shot(targetPos);

    }
    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override IEnumerator CRT_Attack()
    {
        _canAttack = true;
        while (true)
        {

            yield return new WaitForSeconds(_coolTime - _attCount * _shootGap);
            _dir = transform.forward;
            for (int i = 0; i < _attCount; i++)
            {
                _shootEffect.Play();
                AttackInteract();
                yield return new WaitForSeconds(_shootGap);
            }
                

        }

    }
    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attCount = stat._attCount;
        _attRange = stat._attRange;
        _coolTime = stat._coolTime;
        if(_level >= 6)
            _isMaxLevel = true;
    }
    public override void StartAttack() { return; }
    
}
