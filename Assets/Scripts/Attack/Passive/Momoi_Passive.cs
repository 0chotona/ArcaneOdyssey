using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Passive : MonoBehaviour, IPassive
{
    Transform _playerTrs;
    [Header("ų ��"), SerializeField] int _killCount = 5;
    [Header("���ӽð�"), SerializeField] float _durTime = 8f;
    [Header("��Ÿ�� ���� ����"), SerializeField] float _coolDown = 0.4f;
    [Header("�̵��ӵ� ���� ����"), SerializeField] float _moveSpeed = 1f;
    bool _isActive = false;
    public int GetKillCount()
    {
        return _killCount;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public void PassiveInteract()
    {
        //óġ 70ȸ �޼� �� 8�� ���� ��ų ���� 150 ����, �̵� �ӵ� ���� ���� �� 8�ʿ� ���� ���� ����
        Debug.Log("����� �нú� �ߵ�");
        StartCoroutine(CRT_Passive());
    }

    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    IEnumerator CRT_Passive()
    {
        _isActive = true;
        // Increase _moveSpeedIncrease by 1
        BuffStat.Instance.UpdateBuffStat(eBUFF_TYPE.MoveSpeed_Up, _moveSpeed);
        BuffStat.Instance.UpdateBuffStat(eBUFF_TYPE.CoolTime_Down, _coolDown);
        float elapsedTime = 0;

        // Gradually decrease _moveSpeedIncrease over _durTime
        while (elapsedTime < _durTime)
        {
            elapsedTime += Time.deltaTime;
            float newSpeed = Mathf.Lerp(_moveSpeed, 0, elapsedTime / _durTime);
            BuffStat.Instance.UpdateBuffStat(eBUFF_TYPE.MoveSpeed_Up, newSpeed - BuffStat.Instance._MoveSpeedBuff);

            yield return null;
        }

        BuffStat.Instance.UpdateBuffStat(eBUFF_TYPE.CoolTime_Down, -_coolDown);
        BuffStat.Instance.UpdateBuffStat(eBUFF_TYPE.MoveSpeed_Up, -BuffStat.Instance._MoveSpeedBuff);
        _isActive = false;
    }

}
