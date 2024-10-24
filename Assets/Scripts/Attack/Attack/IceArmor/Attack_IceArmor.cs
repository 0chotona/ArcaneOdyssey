using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

/*
 *  ���ط�: ���ط��� �����մϴ�.
�Ѿ�����-��ų���� ��ų ����: ���� ���ð��� �����մϴ�.
�Ѿ�����-����ũ�� �ű� ���� ũ��: ���� ������ �����մϴ�.
�Ѿ�����-���ӽð� �ű� ���ӽð�: ���� ���� �ð��� �����մϴ�. ��ȭ �� ��ȣ���� ���� �ð��� �����մϴ�.
�Ѿ�����-ü�� �ű� �ִ� ü��: ���ط��� �����ϰ� ��ȭ �� ��ȣ�� ������� �����մϴ�.
�Ѿ�����-�������� �ű� ����: ���ط��� �����ϰ� ��ȭ�� �� �ֽ��ϴ�.
*/
public class Attack_IceArmor : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_IceArmor _damageBox;

    [Header("�ݶ��̴�"), SerializeField] Collider _collider;
    [Header("���� ������Ʈ"), SerializeField] GameObject _explodeObj;

    [Header("����Ʈ"), SerializeField] ParticleSystem _effectParticle;
    [Header("���� ��"), SerializeField] float _shieldAmount = 50f;
    [Header("���� ���ӽð�"), SerializeField] float _shieldDur = 5f;
    [SerializeField] PlayerHealth _playerHealth;
    ParticleSystem[] _effectParticles;


    [Header("�߻� ��ġ"), SerializeField] Transform _shootTrs;

    [Header("�ִ� Hp�� ������"), SerializeField] float _dmgPerMaxHp = 100f;
    float _shieldPerMaxHp = 0.5f;
    private void Awake()
    {
        _collider.enabled = false;
        _effectParticles = _effectParticle.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _effectParticles)
        {
            particle.Stop();
        }
    }
    private void OnEnable()
    {
        _name = eSKILL.IceArmor;
    }
    public override void AttackInteract()
    {
        

        GameObject explodeObj = Instantiate(_explodeObj, _shootTrs.position, Quaternion.identity);
        Explosion_IceArmor explosion = explodeObj.GetComponent<Explosion_IceArmor>();

        float finalDamage = _damage + _damage * _buffStat._Att + _buffStat._Def + _buffStat._MaxHp * _dmgPerMaxHp;
        explosion.UpdateDamage(finalDamage);
        explosion.UpdateDur(_durTime + _durTime * _buffStat._Dur);
        explosion.UpdateScale(_attRange + _attRange * _buffStat._Range);
        explosion.ShowParticle();
        explosion.Explode();
        explosion.DestroyObj(1f);
    }

    public override IEnumerator CRT_Attack()
    {

        _collider.enabled = false;
        _playerHealth.SetShield(_shieldAmount + _buffStat._MaxHp * _shieldPerMaxHp, _shieldDur);
        yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
        _collider.enabled = true;
        StopParticles();
        StartCoroutine(CRT_PlayParticles());
    }
    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override void StartAttack()
    {
        _collider.enabled = true;
        StartCoroutine(CRT_PlayParticles());
    }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;

    }

    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _coolTime = stat._coolTime;
        _attRange = stat._attRange;
        _durTime = stat._durTime;
        
        if (_level >= 6)
        {
            _isMaxLevel = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PlayParticles();
            AttackInteract();
            StartCoroutine(CRT_Attack());
        }
    }
    IEnumerator CRT_PlayParticles()
    {
        PlayParticles();
        yield return new WaitForSeconds(0.35f);
        PauseParticles();

    }
    void PlayParticles()
    {
        foreach (ParticleSystem particle in _effectParticles)
        {
            particle.Play();
        }
    }
    void PauseParticles()
    {
        foreach (ParticleSystem particle in _effectParticles)
        {
            particle.Pause();
        }
    }
    void StopParticles()
    {
        foreach (ParticleSystem particle in _effectParticles)
        {
            particle.Stop();
        }
    }

    public override void SetPlayerTrs(Transform playerTrs) { }
}
