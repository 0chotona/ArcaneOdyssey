using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("���̽�ƽ")]
    [SerializeField] RectTransform _joystickGameObject; // ���̽�ƽ GameObject
    [SerializeField] JoyStick _joyStick;               // JoyStick ��ũ��Ʈ ����
    [SerializeField] Canvas _parentCanvas;             // ���̽�ƽ�� ���� Canvas
    RectTransform _canvasRectTransform;


    [SerializeField] Vector2 _testVec;


    bool _canDrag = true;
    void Awake()
    {
        if (_joystickGameObject != null)
            _joystickGameObject.gameObject.SetActive(false); // ���̽�ƽ �ʱ� ��Ȱ��ȭ

        if (_parentCanvas != null)
            _canvasRectTransform = GetComponent<RectTransform>();
    }

    void SetJoystickPosition(PointerEventData eventData)
    {
        if (_joystickGameObject != null && _canvasRectTransform != null)
        {
            // ��ġ ��ǥ�� ĵ���� ���� ��ǥ�� ��ȯ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );

            // ���̽�ƽ ��ġ ����
            _joystickGameObject.anchoredPosition = localPoint;
            _testVec = localPoint;
        }
    }

    // �巡�� ���� ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_canDrag)
        {
            if (_joystickGameObject != null)
            {
                _joystickGameObject.gameObject.SetActive(true); // ���̽�ƽ Ȱ��ȭ
                SetJoystickPosition(eventData);                // ���̽�ƽ ��ġ ����
            }

            if (_joyStick != null)
            {
                _joyStick.OnBeginDrag(eventData);              // JoyStick�� OnBeginDrag ȣ��
            }
        }
        
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            if (_joyStick != null)
            {
                _joyStick.OnDrag(eventData); // JoyStick�� OnDrag ȣ��
            }

        }
            
    }

    // �巡�� ���� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        if(_canDrag)
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

    public void SetCanDrag(bool canDrag)
    {
        _canDrag = canDrag;
    }
}
