using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSensor : MonoBehaviour
{
    [SerializeField] MapSpawner _mapSpawner;

    [SerializeField] Vector3 _vectorIdx;

    [SerializeField] Transform _playerTrs;

    float _sideDist;
    private void Awake()
    {
        _sideDist = transform.lossyScale.x;
    }

    /*
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 quadToPlayer = _playerTrs.position - transform.position;

            Vector3 targetPos = Vector3.zero;

            if (quadToPlayer.x > _sideDist * 0.5f)
                targetPos.x += _sideDist * 2f;
            else
                targetPos.x -= _sideDist * 2f;

            if (quadToPlayer.z > _sideDist * 0.5f)
                targetPos.z += _sideDist * 2f;
            else
                targetPos.z -= _sideDist * 2f;
        }
    }
    */
    private void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("MapSensor"))
        {
            Vector3 quadToPlayer = _playerTrs.position - transform.position;
            Vector3 targetDir = Vector3.zero;
            if (quadToPlayer.x > 0) //6
            {
                if(quadToPlayer.x - _sideDist * 0.5f > 0)
                {
                    targetDir.x = 1;
                }
                else if(quadToPlayer.x - _sideDist * 0.5f < 0)
                {
                    targetDir.x = 0;
                }
            }
            else
            {
                if (quadToPlayer.x + _sideDist * 0.5f < 0)
                {
                    targetDir.x = -1;
                }
                else if (quadToPlayer.x + _sideDist * 0.5f > 0)
                {
                    targetDir.x = 0;
                }
            }
            if (quadToPlayer.z > 0)
            {
                if (quadToPlayer.z - _sideDist * 0.5f > 0)
                {
                    targetDir.z = 1;
                }
                else if (quadToPlayer.z - _sideDist * 0.5f < 0)
                {
                    targetDir.z = 0;
                }
            }
            else
            {
                if (quadToPlayer.z + _sideDist * 0.5f < 0)
                {
                    targetDir.z = -1;
                }
                else if (quadToPlayer.z + _sideDist * 0.5f > 0)
                {
                    targetDir.z = 0;
                }
            }

            /*
            if (quadToPlayer.x - _sideDist * 0.5f > 0)
                targetDir.x = 1;
            else
                targetDir.x = -1;

            if (quadToPlayer.z - _sideDist * 0.5f > 0)
                targetDir.z = 1;
            else
                targetDir.z = -1;
            */
            _mapSpawner.SpawnMap(targetDir);
            /*
            Vector3 targetPos = Vector3.zero;

            if (quadToPlayer.x > _sideDist * 0.5f)
                targetPos.x += _sideDist * 2f;
            else
                targetPos.x -= _sideDist * 2f;

            if(quadToPlayer.z > _sideDist * 0.5f)
                targetPos.z += _sideDist * 2f;
            else
                targetPos.z -= _sideDist * 2f;
            */
            
        }
    }
    
}
