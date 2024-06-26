using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Type : MonoBehaviour
{
    public enum ITEMTYPE
    {
        Upgrade,
        HpPlus,
        Magnet,
        Bomb,
        Jewel
    }
    public ITEMTYPE _itemType;
    public float _point;

    public bool _isGotByPlayer = false;
    public bool _isGotByMagnet = false;
    float _moveSpeed = 10f;
    float _magnetSpeed = 50f;
    Transform _playerTrs;




    private void Awake()
    {
        _point = (GameManager.Instance._Level * 0.1f) + 1;
    }
    private void Update()
    {
        if (_isGotByPlayer)
            MoveItem(_moveSpeed);

        if (_isGotByMagnet)
            MoveItem(_magnetSpeed);
    }
    public void SetPlayerTransform(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    void MoveItem(float speed)
    {
        GameObject obj = gameObject;
        transform.position = Vector3.MoveTowards(obj.transform.position, _playerTrs.position, speed * Time.deltaTime);
        
    }
}
