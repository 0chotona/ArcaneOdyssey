using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Passive : MonoBehaviour, IPassive
{
    Transform _playerTrs;
    [Header("킬 수"), SerializeField] int _killCount = 5;
    [Header("지속시간"), SerializeField] float _durTime = 8f;
    [Header("쿨타임 감소 비율"), SerializeField] float _coolDown = 0.4f;
    [Header("이동속도 증가 비율"), SerializeField] float _moveSpeed = 1f;
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
        //처치 70회 달성 시 8초 동안 스킬 가속 150 증가, 이동 속도 대폭 증가 후 8초에 걸쳐 점차 감소
        Debug.Log("모모이 패시브 발동");
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
        BuffController.Instance.UpdateBuffStat(eBUFF_TYPE.MoveSpeed_Up, _moveSpeed);
        BuffController.Instance.UpdateBuffStat(eBUFF_TYPE.CoolTime_Down, _coolDown);
        float elapsedTime = 0;

        // Gradually decrease _moveSpeedIncrease over _durTime
        while (elapsedTime < _durTime)
        {
            elapsedTime += Time.deltaTime;
            float newSpeed = Mathf.Lerp(_moveSpeed, 0, elapsedTime / _durTime);
            BuffController.Instance.UpdateBuffStat(eBUFF_TYPE.MoveSpeed_Up, newSpeed - BuffController.Instance._MoveSpeedBuff);

            yield return null;
        }

        BuffController.Instance.UpdateBuffStat(eBUFF_TYPE.CoolTime_Down, -_coolDown);
        BuffController.Instance.UpdateBuffStat(eBUFF_TYPE.MoveSpeed_Up, 0);
        _isActive = false;
    }

}
