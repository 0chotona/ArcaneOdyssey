using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eITEMTYPE
{
    Upgrade,
    HpPlus,
    Magnet,
    Bomb,
    Jewel
}
public class Item_Type : MonoBehaviour
{
    
    public eITEMTYPE _itemType;
    public float _point;

    public bool _isGotByPlayer = false;
    public bool _isGotByMagnet = false;
    float _moveSpeed = 10f;
    float _magnetSpeed = 50f;
    Transform _playerTrs;




    private void Awake()
    {
        _point = 1;
    }
    /*
    private void Update()
    {

        if (_isGotByPlayer)
            CRT_MoveItem(_moveSpeed);

        if (_isGotByMagnet)
            CRT_MoveItem(_magnetSpeed);
    }
    */
    public void SetPlayerTransform(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    public void MoveItem()
    {
        StartCoroutine(CRT_MoveItem(_moveSpeed));
    }
    public void MoveItemByMagnet()
    {
        StartCoroutine(CRT_MoveItem(_magnetSpeed));
    }
    IEnumerator CRT_MoveItem(float speed)
    {
        while(true)
        {
            GameObject obj = gameObject;
            transform.position = Vector3.MoveTowards(obj.transform.position, _playerTrs.position, speed * Time.deltaTime);
            yield return null;
        }
        
        
    }
}
