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

    GameObject _tmpObj;


    private void Awake()
    {
        _point = 1;
        _tmpObj = gameObject;
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
        while(Vector3.Distance(transform.position, _playerTrs.position) > 0.05f)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, _playerTrs.position, speed * Time.deltaTime);
            
            yield return null;
        }
        Destroy(_tmpObj);
        
    }
}
