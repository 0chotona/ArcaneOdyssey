using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Active_R : MonoBehaviour, IActiveAttackable
{
    [Header("커서 감지 오브젝트"), SerializeField] GameObject _checkMouseObj;
    GetDrag _getDrag;

    Transform _playerTrs;
    private void Awake()
    {
        _getDrag = _checkMouseObj.GetComponent<GetDrag>();
        _checkMouseObj.transform.SetParent(null);
        //_cursorObj.SetActive(false);
    }
    public void ActiveInteract()
    {
        _checkMouseObj.SetActive(true);
        Time.timeScale = 0.1f;
        _checkMouseObj.transform.position = _playerTrs.position;
    }

    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
    
}
