using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("조이스틱")]
    [SerializeField] RectTransform _joystickGameObject; // 조이스틱 GameObject
    [SerializeField] JoyStick _joyStick; // JoyStick 스크립트 참조

    void Awake()
    {
        // 조이스틱을 초기 상태에서 비활성화
        if (_joystickGameObject != null)
            _joystickGameObject.gameObject.SetActive(false);
    }

    // 드래그 시작 시
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_joystickGameObject != null)
        {
            _joystickGameObject.gameObject.SetActive(true); // 조이스틱 활성화
            _joystickGameObject.anchoredPosition = eventData.position;
        }

        if (_joyStick != null)
        {
            _joyStick.OnBeginDrag(eventData); // JoyStick의 OnBeginDrag 호출
        }
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (_joyStick != null)
        {
            _joyStick.OnDrag(eventData); // JoyStick의 OnDrag 호출
        }
    }

    // 드래그 종료 시
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_joyStick != null)
        {
            _joyStick.OnEndDrag(eventData); // JoyStick의 OnEndDrag 호출
        }

        if (_joystickGameObject != null)
        {
            _joystickGameObject.gameObject.SetActive(false); // 조이스틱 비활성화
        }
    }
}
