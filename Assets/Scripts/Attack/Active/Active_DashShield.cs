using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_DashShield : MonoBehaviour, IActiveAttackable
{
    bool _canAttack = false;
    [Header("쿨타임"), SerializeField] float _coolTime = 10f;
    [Header("이펙트"), SerializeField] ParticleSystem _effectParticle;

    Transform _playerTrs;

    PlayerMove _playerMove;
    PlayerHealth _playerHealth;

    [Header("대쉬 시간"), SerializeField] float _dashTime = 1f;
    [Header("대쉬 스피드"), SerializeField] float _dashSpeed = 3f;

    [Header("쉴드 량"), SerializeField] float _shieldAmount = 50f;
    [Header("쉴드 지속시간"), SerializeField] float _shieldDur = 3f;

    ParticleSystem[] _effectParticles;

    private void Awake()
    {
        _effectParticles = _effectParticle.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _effectParticles)
            particle.Stop();
        StartCoroutine(CRT_SetCoolTime());
    }
    IEnumerator CRT_SetCoolTime()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_coolTime);

        _canAttack = true;
    }
    public void SetPlayer(PlayerMove playerMove, PlayerHealth playerHealth)
    {
        _playerMove = playerMove;
        _playerHealth = playerHealth;
    }
    public void ActiveInteract()
    {
        ShowParticle();
        _playerMove.Dash(_dashTime, _dashSpeed);
        _playerHealth.SetShield(_shieldAmount, _shieldDur);
    }
    void ShowParticle()
    {
        foreach (ParticleSystem particle in _effectParticles)
            particle.Play();
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs; 
        _playerMove = _playerTrs.GetComponent<PlayerMove>();
        _playerHealth = _playerTrs.GetComponentInChildren<PlayerHealth>();

    }
}
