using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] SkillManager _skillManager;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] ItemSpawner _itemSpawner;

    [SerializeField] GameObject _bombSensor;

    [Header("범위"), SerializeField] float _itemPickRange = 1f;
    float _itemPickRangeBuff = 0f;

    SphereCollider _collider;
    float _healAmount = 100f;
    public float _curPoint = 0;
    [Header("1레벨 최대 포인트"), SerializeField] float _baseMaxPoint = 2;
    [Header("레벨 당 포인트 차이"), SerializeField] float _pointOffset = 3;
    float _maxPoint = 5;


    float _expGainBuff;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _itemPickRange + _itemPickRangeBuff;
        _expGainBuff = 0;
        _itemSpawner.SetPlayerTrs(transform);

        _maxPoint = _baseMaxPoint + GameManager.Instance._Level * _pointOffset;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))  
        {
            Item_Type itemType = other.GetComponent<Item_Type>();
            itemType.SetPlayerTransform(transform);
            itemType.MoveItem();
            switch (itemType._itemType)
            {
                case eITEMTYPE.Upgrade:
                    UIManager.Instance.UpgradeLevel5();
                    break;
                case eITEMTYPE.HpPlus:
                    _playerHealth.GetHeal(_healAmount);
                    break;
                case eITEMTYPE.Magnet:
                    _itemSpawner.SetMagnet();
                    break;
                case eITEMTYPE.Bomb:
                    StartCoroutine(CRT_Bomb());
                    break;
                case eITEMTYPE.Jewel:
                    _curPoint += itemType._point + (itemType._point * _expGainBuff);
                    if(_curPoint >= _maxPoint)
                    {
                        float remainPoint = _curPoint - _maxPoint;
                        GameManager.Instance.UpgradeLevel();
                        _curPoint = remainPoint;
                        _maxPoint = _baseMaxPoint + GameManager.Instance._Level * _pointOffset;

                    }
                    UIManager.Instance.UpdateExpBar(_maxPoint, _curPoint);
                    break;
            }
            //Destroy(other.gameObject);
            
        }
    }
    IEnumerator CRT_Bomb()
    {
        _bombSensor.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        _bombSensor.SetActive(false);
    }
    public void UpdateItemPickupRange(float value)
    {
        _itemPickRangeBuff = _itemPickRange * value;
        _collider.radius = _itemPickRange + _itemPickRangeBuff;
    }
    public void UpdateExpGain(float value)
    {
        _expGainBuff = value;
    }



}
