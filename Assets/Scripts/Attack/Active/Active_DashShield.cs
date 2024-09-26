using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_DashShield : MonoBehaviour, IActiveAttackable
{
    bool _canAttack = false;
    [Header("��Ÿ��"), SerializeField] float _coolTime = 10f;
    [Header("���� ��"), SerializeField] float _damage = 10f;
    [Header("����Ʈ"), SerializeField] ParticleSystem _effectParticle;
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
    public void ActiveInteract()
    {
        throw new System.NotImplementedException();
    }

}
