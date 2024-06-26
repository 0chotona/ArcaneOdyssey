using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _itemObjs;

    Transform _playerTrs;
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
        GetComponent<EnemySpawner>().SetPlayerTrs(playerTrs);
    }
    public void SetMagnet()
    {
        foreach(Transform child in transform)
        {
            Item_Type itemType = child.GetComponent<Item_Type>();
            if (itemType != null)
            {
                itemType.SetPlayerTransform(_playerTrs);
                itemType._isGotByMagnet = true;
            }
            else
                continue;
        }
    }
}

