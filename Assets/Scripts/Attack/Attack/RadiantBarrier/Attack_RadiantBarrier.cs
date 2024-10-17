using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_RadiantBarrier : Attack
{

    [Header("������ ���� �ð�"), SerializeField] float _damageGapTime = 0.5f;

    [Header("�ִ� Hp�� ������"), SerializeField] float _dmgPerMaxHp = 10f;
    [Header("���� ������"), SerializeField] float _explodeDamage = 200f;



    [Header("����Ʈ"), SerializeField] ParticleSystem _effectParticle;

    [Header("���� ������Ʈ"), SerializeField] GameObject _explodeObj;


    [Header("�ݶ��̴�"), SerializeField] SphereCollider _collider;
    [Header("��ƼŬ �⺻ ������"), SerializeField] Vector3 _baseParticleScale = new Vector3(1f, 1f, 1f);

    List<Transform> _nearEnemyList = new List<Transform>();

    float _baseRadius = 4f;

    private void OnEnable()
    {
        _name = eSKILL.RadiantBarrier;
    }
    private void Awake()
    {
        _effectParticle.Stop();
        _collider.enabled = false;
        _collider.radius = _baseRadius;
        SetParticleScale(1f);
    }
    public override void AttackInteract()
    {
        foreach(Transform enemy in _nearEnemyList)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage + _damage * _buffStat._Att + _buffStat._MaxHp * _dmgPerMaxHp);
        }
    }

    public override IEnumerator CRT_Attack()
    {
        while(true)
        {
            yield return new WaitForSeconds(_damageGapTime);
            RemoveDestroyEnemy();
            AttackInteract();
        }
        
    }

    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override void StartAttack()
    {
        StartCoroutine(CRT_PlayParticle());
        StartCoroutine(CRT_Attack());
        _collider.enabled = true;

    }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }

    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attRange = stat._attRange;
        _durTime = stat._durTime;
        if(_buffStat != null)
        {
            _collider.radius = _attRange * _baseRadius + _buffStat._Range * _baseRadius;
            SetParticleScale(_collider.radius / _baseRadius);
        }
        

        if (_level >= 6)
        {
            _isMaxLevel = true;
        }
    }

    void SetParticleScale(float radius)
    {
        _effectParticle.transform.localScale = _baseParticleScale * radius;
    }
    IEnumerator CRT_PlayParticle()
    {
        _effectParticle.Play();
        yield return new WaitForSeconds(1f);
        _effectParticle.Pause();
    }
    public void RemoveDestroyEnemy()
    {
        _nearEnemyList.RemoveAll(target => target == null);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _nearEnemyList.Add(other.transform);
            if(_isMaxLevel)
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                enemyHealth.SetPlant(true);
                enemyHealth.PlantExplosion(_explodeObj, _explodeDamage + _explodeDamage * _buffStat._Att);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _nearEnemyList.Remove(other.transform);
            if (_isMaxLevel)
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                enemyHealth.SetPlant(false);
            }
        }
    }
}
