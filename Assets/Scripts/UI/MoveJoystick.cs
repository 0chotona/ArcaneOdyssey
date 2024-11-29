using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("���̽�ƽ")]
    [SerializeField] RectTransform _joystickGameObject; // ���̽�ƽ GameObject
    [SerializeField] JoyStick _joyStick; // JoyStick ��ũ��Ʈ ����

    void Awake()
    {
        // ���̽�ƽ�� �ʱ� ���¿��� ��Ȱ��ȭ
        if (_joystickGameObject != null)
            _joystickGameObject.gameObject.SetActive(false);
    }

    // �巡�� ���� ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_joystickGameObject != null)
        {
            _joystickGameObject.gameObject.SetActive(true); // ���̽�ƽ Ȱ��ȭ
            _joystickGameObject.anchoredPosition = eventData.position;
        }

        if (_joyStick != null)
        {
            _joyStick.OnBeginDrag(eventData); // JoyStick�� OnBeginDrag ȣ��
        }
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        if (_joyStick != null)
        {
            _joyStick.OnDrag(eventData); // JoyStick�� OnDrag ȣ��
        }
    }

    // �巡�� ���� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_joyStick != null)
        {
            _joyStick.OnEndDrag(eventData); // JoyStick�� OnEndDrag ȣ��
        }

        if (_joystickGameObject != null)
        {
            _joystickGameObject.gameObject.SetActive(false); // ���̽�ƽ ��Ȱ��ȭ
        }
    }
}
