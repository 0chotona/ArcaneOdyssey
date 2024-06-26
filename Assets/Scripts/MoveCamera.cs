using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float _distAway = 9f;
    [SerializeField] float _distUp = 10f;
    [SerializeField] Transform _targetPos;
    Transform _transform;
    private void Awake()
    {
        _transform = transform;
    }
    private void LateUpdate()
    {
        _transform.position = _targetPos.position + Vector3.up * _distUp - Vector3.forward * _distAway;
        _transform.LookAt(_targetPos);
    }
}
