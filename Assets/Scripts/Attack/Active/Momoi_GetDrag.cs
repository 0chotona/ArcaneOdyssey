using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_GetDrag : MonoBehaviour
{
    [Header("���� ������Ʈ"), SerializeField] GameObject _cursorObj;
    [Header("��Ƽ�� R"), SerializeField] Momoi_Active_R _activeR;


    private void Awake()
    {
        _cursorObj.transform.SetParent(null);
        _cursorObj.SetActive(false);
    }
    private void OnMouseDown()
    {
        _cursorObj.SetActive(true);
    }
    void OnMouseDrag()
    {
        Vector3 worldPosition = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
        {
            worldPosition = hit.point;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // ī�޶���� �Ÿ� ����
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        
        worldPosition.y = 1f;

        _cursorObj.transform.position = worldPosition;

    }
    private void OnMouseUp()
    {
        if (_cursorObj.activeSelf)
        {
            Time.timeScale = 1;
            Debug.Log(_cursorObj.transform.position);

            _activeR.ShootRocket(_cursorObj.transform.position);
            _cursorObj.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
