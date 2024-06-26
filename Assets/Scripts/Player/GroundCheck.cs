using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Vector3 _boxSize;
    [SerializeField] float _maxDistance;
    [SerializeField] LayerMask _groundLayer;

    private void OnDrawGizmos()
    {
        Vector3 boxCenter = transform.position;
        Quaternion boxRotation = transform.rotation;

        /*
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(boxCenter, _boxSize * 2); 
        */
        bool isGrounded = Physics.BoxCast(transform.position, _boxSize, -transform.up, transform.rotation, _maxDistance, _groundLayer);

        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(boxCenter - Vector3.up * _maxDistance, _boxSize * 2);
    }
    public bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, _boxSize, -transform.up, transform.rotation, _maxDistance, _groundLayer);
    }
}
