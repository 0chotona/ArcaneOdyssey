using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chise_Active_E : MonoBehaviour, IActiveAttackable
{
    [Header("돌진 속도"), SerializeField] float _dashSpeed = 1f;
    [Header("돌진 거리"), SerializeField] float _dashDistance = 5f;


    [Header("쿨타임"), SerializeField] float _coolTime = 8f;
    [Header("스턴 지속시간"), SerializeField] float _stunDur = 2f;
    [Header("데미지"), SerializeField] float _damage = 150f;

    [Header("콜라이더"), SerializeField] Collider _collider;

    Transform _playerTrs;
    PlayerMove _playerMove;
    bool _canActive = true;
    CBuffStat _buffStat = new CBuffStat();


    ParticleSystem[] _particles;
    private void Awake()
    {
        _collider.enabled = false;
        ShowParticle(false);
    }
    public void ActiveInteract()
    {
        if(_canActive)
        {
            Vector3 targetPos = _playerTrs.position + _playerTrs.forward * _dashDistance;
            StartCoroutine(CRT_DashAttack(targetPos, _dashSpeed));
            StartCoroutine(CRT_CoolTime());
            UIManager.Instance.StartECooltime(_coolTime - _coolTime * BuffStat.Instance._CoolTimeBuff);

            SoundManager.Instance.PlaySound(eCHARSOUNDTYPE.Chise_E);
        }

    }
    IEnumerator CRT_DashAttack(Vector3 targetPos, float speed)
    {
        yield return _playerMove.StartCoroutine(_playerMove.CRT_Dash(targetPos, speed));
        Attack();
    }

    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
        _playerMove = _playerTrs.GetComponent<PlayerMove>();
    }
    IEnumerator CRT_CoolTime()
    {
        _canActive = false;
        yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
        _canActive = true;
    }
    void Attack()
    {
        StartCoroutine(CRT_Attack());
    }
    IEnumerator CRT_Attack()
    {
        ShowParticle(true);
        _collider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        _collider.enabled = false;
    }
    public void ShowParticle(bool isShow)
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _particles)
        {
            if(isShow)
            {
                particle.Play();
            }
            else
            {
                particle.Stop();
            }
        }
    }
    public void SetBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyMove enemyMove = other.GetComponent<EnemyMove>();
            enemyMove.GetFroze(_stunDur + _buffStat._Dur);

            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
}
