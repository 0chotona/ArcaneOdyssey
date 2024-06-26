using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Transform _playerTrs;

    NavMeshAgent _navMesh;

    float _moveSpeed;
    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
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
