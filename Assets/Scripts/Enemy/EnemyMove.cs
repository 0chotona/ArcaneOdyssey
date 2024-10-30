using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Transform _playerTrs;

    NavMeshAgent _navMesh;

    float _moveSpeed;
    public bool _isFrozed = false;
    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        
    }
    private void Update()
    {
        if (_playerTrs != null)
        {
            if(!_isFrozed)
                MoveToPlayer();
        }
    }
    public void GetFroze(float time)
    {
        StartCoroutine(CRT_GetFroze(time));
    }
    public void GetAirborne(float time, float speed)
    {
        StartCoroutine(CRT_GetFroze(time));
        StartCoroutine(CRT_GetAirborne(time, speed));
    }
    IEnumerator CRT_GetFroze(float time)
    {
        _isFrozed = true;
        yield return new WaitForSeconds(time);
        _isFrozed = false;
    }
    IEnumerator CRT_GetAirborne(float time, float speed)
    {
        float halfTime = time / 2f;
        float elapsedTime = 0f;

        // ���� �ö󰡴� �κ�
        while (elapsedTime < halfTime)
        {
            //transform.position += Vector3.up * speed * Time.deltaTime;
            _navMesh.baseOffset += speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ð� �ʱ�ȭ
        elapsedTime = 0f;

        // �Ʒ��� �������� �κ�
        while (elapsedTime < halfTime)
        {
            //transform.position += Vector3.down * speed * Time.deltaTime;
            _navMesh.baseOffset -= speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _navMesh.baseOffset = 1f;
    }
    void MoveToPlayer()
    {
        _navMesh.SetDestination(_playerTrs.position);
    }
    public void SetMoveSpeed(float moveSpeed)
    {
        _navMesh.speed = moveSpeed;
    }

    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
        
    }
    public float DistToPlayer()
    {
        float dist = Vector3.Distance(transform.position, _playerTrs.position);
        return dist;
    }
    
}
