using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    

    [SerializeField] Transform _playerTrs;

    float _planeSize = 70;
    Vector3 _objSize;

    Vector3 _playerBasePos;

    [SerializeField] GameObject[] _mapObjs; //123, 369, 789, 147
    
    private void Awake()
    {
        _objSize = new Vector3(_planeSize * 0.5f, 0, _planeSize * 0.5f);
        _playerBasePos = Vector3.zero;
    }
    /*
    public void SpawnMap(Vector3 dir)
    {
        
        _playerBasePos = _playerBasePos + (dir * _planeSize);
        foreach (GameObject map in _mapObjs)
        {
            if (Vector3.Distance(map.transform.position, _playerBasePos) >= _planeSize * 2)
                map.transform.position += dir * _planeSize * 3;
        }
    }
    */
    public void SpawnMap(Vector3 dir)
    {
        _playerBasePos = _playerBasePos + (dir * _planeSize);
        foreach (GameObject map in _mapObjs)
        {
            if (Vector3.Distance(map.transform.position, _playerBasePos) >= _planeSize * 2)
                map.transform.position += dir * _planeSize * 3;
        }
    }

}
