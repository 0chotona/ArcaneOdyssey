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
    IEnumerator CRT_GetFroze(float time)
    {
        _isFrozed = true;
        yield return new WaitForSeconds(time);
        _isFrozed = false;
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
    
}
