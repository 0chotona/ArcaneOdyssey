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
    bool _isSlow = false;
    public bool _IsSlow => _isSlow;

    Coroutine _resetCountCor;
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
    public void GetSlow(float time, float amount)
    {
        StartCoroutine(CRT_GetSlow(time, amount));
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
    IEnumerator CRT_GetSlow(float time, float rate)
    {
        float slowSpeed = _moveSpeed * rate;
        SetMoveSpeed(slowSpeed);
        _isSlow = true;
        yield return new WaitForSeconds(time);
        _isSlow = false;
        SetMoveSpeed(_moveSpeed);

    }
    IEnumerator CRT_GetAirborne(float time, float speed)
    {
        float halfTime = time / 2f;
        float elapsedTime = 0f;

        // 위로 올라가는 부분
        while (elapsedTime < halfTime)
        {
            //transform.position += Vector3.up * speed * Time.deltaTime;
            _navMesh.baseOffset += speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 시간 초기화
        elapsedTime = 0f;

        // 아래로 내려오는 부분
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
        _moveSpeed = moveSpeed;
        _navMesh.speed = _moveSpeed;
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
