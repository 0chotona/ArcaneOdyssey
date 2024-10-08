using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Active_R : MonoBehaviour, IActiveAttackable
{
    [Header("Ŀ�� ���� ������Ʈ"), SerializeField] GameObject _checkMouseObj;

    [Header("�Ѿ� �߻� ��ġ"), SerializeField] Transform _shootTrs;

    [Header("������"), SerializeField] float _damage = 10f;

    [Header("���� ������"), SerializeField] GameObject _roketPref;
    [Header("�Ѿ� �ӵ�"), SerializeField] float _speed = 10f;

    [Header("���� ü�� �ۼ�Ʈ"), SerializeField] float _hpPercent = 70f;

    [Header("��Ÿ��"), SerializeField] float _coolTime = 10f;

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
        damageBox.UpdateDamage(_damage);
        damageBox.UpdateSpeed(_speed);
        damageBox.UpdateHpPercent(_hpPercent);
        damageBox.Shot(targetPos);
        UIManager.Instance.StartRCooltime(_coolTime - _coolTime * BuffController.Instance._CoolTimeBuff);
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    IEnumerator CRT_CoolTime()
    {
        _canActive = false;
        yield return new WaitForSeconds(_coolTime - _coolTime * BuffController.Instance._CoolTimeBuff);
        _canActive = true;
    }
}
