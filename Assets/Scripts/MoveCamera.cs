using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveCamera : MonoBehaviour
{
    [Header("시작 위치"), SerializeField] Vector3 _startPos = new Vector3(0f, 20f, -6f);
    [Header("도착 위치"), SerializeField] Vector3 _targetPos = new Vector3(0f, 10f, -6f);
    [SerializeField] CinemachineVirtualCamera _virtualCam;

    public void MoveStartAction(float dalay)
    {
        StartCoroutine(CRT_StartAction(dalay));
    }
    IEnumerator CRT_StartAction(float delay)
    {
        var transposer = _virtualCam.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 start = _startPos;
        Vector3 target = _targetPos;
        float timer = 0f;

        transposer.m_FollowOffset = start;
        
        while (timer < delay)
        {
            timer += Time.deltaTime;
            float progress = timer / delay;
            transposer.m_FollowOffset = Vector3.Lerp(start, target, progress);

            yield return null;
        }

        // 최종적으로 target 위치로 설정
        transposer.m_FollowOffset = target;
        UIManager.Instance.StartTimer();
    }
}
