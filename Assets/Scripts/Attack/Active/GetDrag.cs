using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDrag : MonoBehaviour
{
    [Header("범위 오브젝트"), SerializeField] GameObject _cursorObj;
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
        mousePosition.z = 10f; // 카메라와의 거리 설정
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // y좌표를 1로 고정
        worldPosition.y = 1f;

        // _cursorObj의 위치를 마우스가 클릭한 위치로 업데이트
        _cursorObj.transform.position = worldPosition;

    }
    private void OnMouseUp()
    {
        if (_cursorObj.activeSelf)
        {
            Time.timeScale = 1;
            Debug.Log(_cursorObj.transform.position);
            _cursorObj.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
