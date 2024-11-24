using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _itemObjs;
    [Header("보석 아이템"), SerializeField] GameObject _jewelObj;
    [Header("아이템 드롭 확률"), SerializeField] float _jewelRate = 0.7f;
    [Header("보석 색"), SerializeField] Color[] _jewelColor = new Color[] { Color.green, Color.cyan, Color.yellow, Color.red };

    [Header("업그레이드 아이템"), SerializeField] GameObject _upgradeObj;
    Transform _playerTrs;
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
        //GetComponent<EnemySpawner>().SetPlayerTrs(playerTrs);
    }
    public void SetMagnet()
    {
        foreach(Transform child in transform)
        {
            Item_Type itemType = child.GetComponent<Item_Type>();
            if (itemType != null)
            {
                itemType.SetPlayerTransform(_playerTrs);
                itemType.MoveItemByMagnet();
            }
            else
                continue;
        }
    }
    public void SpawnItem(eITEMTYPE itemType, Vector3 pos)
    {
        switch (itemType)
        {
            case eITEMTYPE.Jewel:
                float rnd = Random.value;
                if (rnd < _jewelRate)
                {
                    GameObject jewel = Instantiate(_jewelObj, pos, Quaternion.identity);
                    Renderer renderer = jewel.GetComponent<Renderer>();
                    renderer.material.color = _jewelColor[(int)(GameManager.Instance._Level * 0.1f)];

                }
                break;
            case eITEMTYPE.Upgrade:
                GameObject upgrade = Instantiate(_upgradeObj, pos, Quaternion.identity);
                break;
        }

    }
}

