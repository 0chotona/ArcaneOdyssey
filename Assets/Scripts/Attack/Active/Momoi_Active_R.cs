using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Active_R : MonoBehaviour, IActiveAttackable
{
    [Header("커서 감지 오브젝트"), SerializeField] GameObject _checkMouseObj;

    [Header("총알 발사 위치"), SerializeField] Transform _shootTrs;

    [Header("데미지"), SerializeField] float _damage = 10f;

    [Header("로켓 프리펩"), SerializeField] GameObject _roketPref;
    [Header("총알 속도"), SerializeField] float _speed = 10f;

    [Header("잃은 체력 퍼센트"), SerializeField] float _hpPercent = 70f;

    [Header("쿨타임"), SerializeField] float _coolTime = 10f;

    bool _canActive = true;
    Transform _playerTrs;
    Vector3 _targetPos;
    private void Awake()
    {
        _checkMouseObj.transform.SetParent(null);
        //_cursorObj.SetActive(false);
    }
    public void ActiveInteract()
    {
        if (_canActive)
        {
            _checkMouseObj.SetActive(true);
            Time.timeScale = 0.1f;
            _checkMouseObj.transform.position = _playerTrs.position;
            StartCoroutine(CRT_CoolTime());
            
        }
        
    }
    public void ShootRocket(Vector3 targetPos)
    {
        GameObject rocket = Instantiate(_roketPref, _shootTrs.position, _shootTrs.rotation);
        Momoi_DamageBox_R damageBox = rocket.GetComponent<Momoi_DamageBox_R>();

        float finalDamage = _damage + (_damage * BuffStat.Instance._AttBuff);
        damageBox.UpdateDamage(finalDamage);
        damageBox.UpdateSpeed(_speed);
        damageBox.UpdateHpPercent(_hpPercent);
        damageBox.Shot(targetPos);
        UIManager.Instance.StartRCooltime(_coolTime - _coolTime * BuffStat.Instance._CoolTimeBuff);
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    IEnumerator CRT_CoolTime()
    {
        _canActive = false;
        yield return new WaitForSeconds(_coolTime - _coolTime * BuffStat.Instance._CoolTimeBuff);
        _canActive = true;
    }
}
