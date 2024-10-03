using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_DashShield : MonoBehaviour, IActiveAttackable
{
    bool _canAttack = false;
    [Header("��Ÿ��"), SerializeField] float _coolTime = 10f;
    [Header("����Ʈ"), SerializeField] ParticleSystem _effectParticle;

    Transform _playerTrs;

    PlayerMove _playerMove;
    PlayerHealth _playerHealth;

    [Header("�뽬 �ð�"), SerializeField] float _dashTime = 1f;
    [Header("�뽬 ���ǵ�"), SerializeField] float _dashSpeed = 3f;

    [Header("���� ��"), SerializeField] float _shieldAmount = 50f;
    [Header("���� ���ӽð�"), SerializeField] float _shieldDur = 3f;

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
