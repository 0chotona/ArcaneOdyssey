using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] Transform _playerTrs;

    float _planeSize = 70f;
    Vector3 _playerBasePos;

    [SerializeField] GameObject[] _mapObjs; // 9 Map parts

    [Header("가로 간격"), SerializeField] float _xGap = 70f;
    [Header("세로 간격"), SerializeField] float _zGap = 70f;


    private void Awake()
    {
        _playerBasePos = Vector3.zero;
    }

    public void ShiftMap(Vector3 curPos)
    {
        Vector3 direction = curPos - _playerBasePos;
        //Vector3 shiftDir = direction.normalized;

        _playerBasePos = curPos; // Update the center part to the player's current part position
        
        if(direction.x < 0)
        {
            foreach (GameObject map in _mapObjs)
            {
                if (map.transform.position.x - _playerTrs.position.x >= (1.5f * _xGap))
                {
                    map.transform.position -= new Vector3(_xGap * 3, 0, 0);
                    MapPartsInfo mapPart = map.GetComponentInChildren<MapPartsInfo>();
                    mapPart.SetPartPos(new Vector3(mapPart._PartPos.x - 3, mapPart._PartPos.y, mapPart._PartPos.z));
                    //SwitchMapPart()
                }
            }
        }
        else if(direction.x > 0)
        {
            foreach (GameObject map in _mapObjs)
            {
                if (map.transform.position.x - _playerTrs.position.x <= -(1.5f * _xGap))
                {
                    map.transform.position += new Vector3(_xGap * 3, 0, 0);
                    MapPartsInfo mapPart = map.GetComponentInChildren<MapPartsInfo>();
                    mapPart.SetPartPos(new Vector3(mapPart._PartPos.x + 3, mapPart._PartPos.y, mapPart._PartPos.z));
                }
            }
        }
        if (direction.z < 0)
        {
            foreach (GameObject map in _mapObjs)
            {
                if (map.transform.position.z - _playerTrs.position.z >= (1.5f * _zGap))
                {
                    map.transform.position -= new Vector3(0, 0, _zGap * 3);
                    MapPartsInfo mapPart = map.GetComponentInChildren<MapPartsInfo>();
                    mapPart.SetPartPos(new Vector3(mapPart._PartPos.x, mapPart._PartPos.y, mapPart._PartPos.z - 3));
                }
            }
        }
        else if(direction.z > 0)
        {
            foreach (GameObject map in _mapObjs)
            {
                if (map.transform.position.z - _playerTrs.position.z <= -(1.5f * _zGap))
                {
                    map.transform.position += new Vector3(0, 0, _zGap * 3);
                    MapPartsInfo mapPart = map.GetComponentInChildren<MapPartsInfo>();
                    mapPart.SetPartPos(new Vector3(mapPart._PartPos.x, mapPart._PartPos.y, mapPart._PartPos.z + 3));
                }
            }
        }
        /*
        foreach (GameObject map in _mapObjs)
        {
            if (Vector3.Distance(map.transform.position, _playerBasePos) >= _planeSize * 2)
            {
                // Move map parts to the opposite side if they are far away from the new center
                map.transform.position += shiftDir * _planeSize * 3;
            }
        }
        */
    }
    void SwitchMapPart(int index1, int index2)
    {
        GameObject tmpPart = null;
        tmpPart = _mapObjs[index1];
        _mapObjs[index1] = _mapObjs[index2];
        _mapObjs[index2] = tmpPart;
    }
}

