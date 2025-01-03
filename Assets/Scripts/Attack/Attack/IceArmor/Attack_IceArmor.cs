using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

/*
 *  피해량: 피해량이 증가합니다.
롤아이콘-스킬가속 스킬 가속: 재사용 대기시간이 감소합니다.
롤아이콘-범위크기 신규 범위 크기: 폭발 범위가 증가합니다.
롤아이콘-지속시간 신규 지속시간: 빙결 지속 시간이 증가합니다. 진화 시 보호막의 지속 시간도 증가합니다.
롤아이콘-체력 신규 최대 체력: 피해량이 증가하고 진화 시 보호막 흡수량도 증가합니다.
롤아이콘-물리방어력 신규 방어력: 피해량이 증가하고 진화할 수 있습니다.
*/
public class Attack_IceArmor : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_IceArmor _damageBox;

    [Header("콜라이더"), SerializeField] Collider _collider;
    [Header("폭발 오브젝트"), SerializeField] GameObject _explodeObj;

    [Header("이펙트"), SerializeField] ParticleSystem _effectParticle;
    [Header("쉴드 량"), SerializeField] float _shieldAmount = 50f;
    [Header("쉴드 지속시간"), SerializeField] float _shieldDur = 5f;
    [SerializeField] PlayerHealth _playerHealth;
    ParticleSystem[] _effectParticles;


    [Header("발사 위치"), SerializeField] Transform _shootTrs;

    [Header("최대 Hp당 데미지"), SerializeField] float _dmgPerMaxHp = 100f;

    public bool _isCoolTime = false;
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
        SoundManager.Instance.PlaySound(eSKILLSOUNDTYPE.IceArmor);
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
        yield return new WaitForSeconds(0.05f);
        
        _playerHealth.SetInvincible(false);
        _collider.enabled = false;
        //_playerHealth.SetShield(_shieldAmount + _buffStat._MaxHp * _shieldPerMaxHp, _shieldDur);
        yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
        _collider.enabled = true;
        _playerHealth.SetInvincible(true);
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
        _playerHealth.SetInvincible(true);
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
            if (!_isMaxLevel)
            {
                StopAllCoroutines();
                _playerHealth.SetInvincible(false);
                _playerHealth.SetIceArmor(_shieldAmount + _buffStat._MaxHp * _shieldPerMaxHp); //여기가 문제
            }
            _isMaxLevel = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(!_isMaxLevel)
            {
                PlayParticles();
                AttackInteract();
                StartCoroutine(CRT_Attack());
            }
            else
            {
                if(!_isCoolTime && _playerHealth.IsShieldBroken())
                {
                    PlayParticles();
                    AttackInteract();
                    StartCoroutine(CRT_SetShield(_coolTime - _coolTime * _buffStat._CoolTime));
                }
            }
            
        }
    }
    IEnumerator CRT_SetShield(float coolTime)
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(coolTime);
        _isCoolTime = false;
        _playerHealth.SetIceArmor(_shieldAmount + _buffStat._MaxHp * _shieldPerMaxHp);
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
