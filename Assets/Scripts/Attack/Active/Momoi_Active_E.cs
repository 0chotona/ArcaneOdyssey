using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Active_E : MonoBehaviour, IActiveAttackable
{
    
    [Header("�Ѿ� �߻� ��ġ"), SerializeField] Transform _shootTrs;

    [Header("����"), SerializeField] float _angle = 15f;
    [Header("�Ÿ�"), SerializeField] float _distance = 20f;

    [Header("���� Ƚ��"), SerializeField] int _attCount = 4;
    [Header("���� ����"), SerializeField] float _timeGap = 0.2f;

    [Header("������"), SerializeField] float _damage = 10f;

    [Header("���� ������"), SerializeField] GameObject _roketPref;
    [Header("�Ѿ� �ӵ�"), SerializeField] float _speed = 10f;

    [Header("��Ÿ��"), SerializeField] float _coolTime = 10f;

    bool _canActive = true;
    public void ActiveInteract()
    {
        if(_canActive)
        {
            StartCoroutine(CRT_ShootRocket());
            StartCoroutine(CRT_CoolTime());
            UIManager.Instance.StartECooltime(_coolTime - _coolTime * BuffController.Instance._CoolTimeBuff);
        }
        
    }

    public void SetPlayerTrs(Transform playerTrs) { }
    

    IEnumerator CRT_ShootRocket()
    {
        for(int i = 0; i < _attCount; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // �� ������ ���� ������ �� ������ ���
                float startAngle = -1.5f * _angle + j * _angle;
                float endAngle = startAngle + _angle;

                // ���� ������ ���� ������ ����
                float randomAngle = Random.Range(startAngle, endAngle);

                // ���� ���� ������ Vector3�� ��ȯ (�÷��̾� �չ����� �������� ȸ��)
                Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * _shootTrs.forward;


                Vector3 targetPos = _shootTrs.position + randomDirection * _distance;
                SpawnRocket(targetPos);
                // ���ϴ� �ൿ�� ���� (��: Gizmos�� �׸���, �Ѿ� �߻� ��)
                //Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + randomDirection * _distance);
                yield return new WaitForSeconds(_timeGap);
            }

        }
        
    }
    void SpawnRocket(Vector3 targetPos)
    {
        

        GameObject rocket = Instantiate(_roketPref, _shootTrs.position, _shootTrs.rotation);
        Momoi_DamageBox_E damageBox = rocket.GetComponent<Momoi_DamageBox_E>();
        damageBox.UpdateDamage(_damage);
        damageBox.UpdateSpeed(_speed);
        damageBox.Shot(targetPos);
    }
    IEnumerator CRT_CoolTime()
    {
        _canActive = false;
        yield return new WaitForSeconds(_coolTime - _coolTime * BuffController.Instance._CoolTimeBuff);
        _canActive = true;
    }
}
