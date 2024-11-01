using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chise_Active_R : MonoBehaviour, IActiveAttackable
{

    [Header("지속시간"), SerializeField] float _durTime = 15f;

    [Header("이동속도 증가 비율"), SerializeField] float _moveSpeedIncrease = 1f;

    [Header("바람 가르기 데미지"), SerializeField] float _damage = 300f;
    [Header("바람 가르기 속도"), SerializeField] float _speed = 50f;
    [Header("바람 가르기 거리"), SerializeField] float _distance = 15f;

    [Header("발사 위치"), SerializeField] Transform _shootTrs;

    [Header("초기 투사체 개수"), SerializeField] int _projectileCount = 3;
    [Header("쿨타임"), SerializeField] float _coolTime = 60f;

    [Header("데미지 박스"), SerializeField] GameObject _damageBox;
    Transform _playerTrs;
    PlayerMove _playerMove;

    bool _isActive = false;
    public bool _IsActive => _isActive;
    bool _canActive = true;
    CBuffStat _buffStat = new CBuffStat();


    ParticleSystem[] _particles;
    public void ActiveInteract()
    {
        if (_canActive)
        {
            StartCoroutine(CRT_DurTime());
            
        }

    }
    public void ShootAttack()
    {
        float startAngle = 0f;
        int count = _projectileCount + (int)_buffStat._ProjectileCount;
        float gap = 360f / count;
        for(int i = 0; i < count; i++)
        {
            float angle = startAngle + gap * i;
            Vector3 targetPos = _shootTrs.position + Quaternion.Euler(0, angle, 0) * _shootTrs.forward * _distance;

            GameObject bullet = Instantiate(_damageBox, _shootTrs.position, _shootTrs.rotation);
            Chise_DamageBox_R damageBox = bullet.GetComponent<Chise_DamageBox_R>();
            damageBox.UpdateDamage(_damage + _damage * _buffStat._Att);
            damageBox.UpdateSpeed(_speed);
            damageBox.Shot(targetPos);
        }
        Debug.Log("슛");
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
        _playerMove = _playerTrs.GetComponent<PlayerMove>();
    }
    IEnumerator CRT_CoolTime()
    {
        _canActive = false;

        float durTime = _durTime + _durTime * _buffStat._Dur;
        float coolTime = _coolTime + _coolTime * _buffStat._CoolTime;
        yield return new WaitForSeconds(durTime + coolTime);
        _canActive = true;
    }
    IEnumerator CRT_DurTime()
    {
        _isActive = true;
        float durTime = _durTime + _durTime * _buffStat._Dur;
        _playerMove.UpdatePassiveSpeed(_moveSpeedIncrease);
        yield return new WaitForSeconds(durTime);
        _isActive = false;
        _playerMove.UpdatePassiveSpeed(0f);
        StartCoroutine(CRT_CoolTime());
        UIManager.Instance.StartRCooltime(_coolTime - _coolTime * BuffStat.Instance._CoolTimeBuff);
    }
    public void ShowParticle()
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in _particles)
        {
            particle.Play();
        }
    }
    public void SetBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }
}
