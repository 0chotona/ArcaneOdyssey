using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] SkillManager _skillManager;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] ItemSpawner _itemSpawner;

    [SerializeField] GameObject _bombSensor;

    float _healRate = 0.3f;
    public float _curPoint = 0;
    float _pointOffset = 5;
    float _maxPoint = 5;


    int _expIncrease;

    private void Awake()
    {
        _expIncrease = 0;
        _itemSpawner.SetPlayerTrs(transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))  
        {
            Item_Type itemType = other.GetComponent<Item_Type>();
            itemType.SetPlayerTransform(transform);
            itemType._isGotByPlayer = true;
            switch (itemType._itemType)
            {
                case Item_Type.ITEMTYPE.Upgrade:
                    UIManager.Instance.ShowUpgradePanel();
                    break;
                case Item_Type.ITEMTYPE.HpPlus:
                    _playerHealth.GetHeal(_healRate);
                    break;
                case Item_Type.ITEMTYPE.Magnet:
                    _itemSpawner.SetMagnet();
                    break;
                case Item_Type.ITEMTYPE.Bomb:
                    StartCoroutine(CRT_Bomb());
                    break;
                case Item_Type.ITEMTYPE.Jewel:
                    _curPoint += itemType._point + (itemType._point * 0.01f * _expIncrease);
                    if(_curPoint >= _maxPoint)
                    {
                        float remainPoint = _curPoint - _maxPoint;
                        GameManager.Instance.UpgradeLevel();
                        _curPoint = remainPoint;
                        _maxPoint = GameManager.Instance._Level * _pointOffset;

                    }
                    UIManager.Instance.UpdateExpBar(_maxPoint, _curPoint);
                    break;
            }
            Destroy(other.gameObject);
            
        }
    }
    IEnumerator CRT_Bomb()
    {
        _bombSensor.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        _bombSensor.SetActive(false);
    }
    public void UpdatePassiveStat()
    {
        _expIncrease += 5;
    }



}
