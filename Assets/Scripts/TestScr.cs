using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScr : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            // ī�޶󿡼� ���콺 ��ġ�� ���� �߻�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ����ĳ��Ʈ�� ������Ʈ�� �浹���� ���
            if (Physics.Raycast(ray, out hit))
            {
                // �浹�� ������Ʈ�� �̸��� ���
                Debug.Log("Clicked Object: " + hit.collider.gameObject.name);
            }
        }
    }
}
