using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_DashShield : MonoBehaviour, IActiveAttackable
{
    bool _canAttack = false;
    [Header("ƒ≈∏¿”"), SerializeField] float _coolTime = 10f;
    [Header("¿Ã∆Â∆Æ"), SerializeField] ParticleSystem _effectParticle;
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
