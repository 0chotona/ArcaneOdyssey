using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScr : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            // 카메라에서 마우스 위치로 레이 발사
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 레이캐스트가 오브젝트에 충돌했을 경우
            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 오브젝트의 이름을 출력
                Debug.Log("Clicked Object: " + hit.collider.gameObject.name);
            }
        }
    }
}
