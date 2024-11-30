using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("조이스틱")]
    [SerializeField] RectTransform _joystickGameObject; // 조이스틱 GameObject
    [SerializeField] JoyStick _joyStick;               // JoyStick 스크립트 참조
    [SerializeField] Canvas _parentCanvas;             // 조이스틱이 속한 Canvas
    RectTransform _canvasRectTransform;


    [SerializeField] Vector2 _testVec;


    bool _canDrag = true;
    void Awake()
    {
        if (_joystickGameObject != null)
            _joystickGameObject.gameObject.SetActive(false); // 조이스틱 초기 비활성화

        if (_parentCanvas != null)
            _canvasRectTransform = GetComponent<RectTransform>();
    }

    void SetJoystickPosition(PointerEventData eventData)
    {
        if (_joystickGameObject != null && _canvasRectTransform != null)
        {
            // 터치 좌표를 캔버스 로컬 좌표로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );

            // 조이스틱 위치 설정
            _joystickGameObject.anchoredPosition = localPoint;
            _testVec = localPoint;
        }
    }

    // 드래그 시작 시
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_canDrag)
        {
            if (_joystickGameObject != null)
            {
                _joystickGameObject.gameObject.SetActive(true); // 조이스틱 활성화
                SetJoystickPosition(eventData);                // 조이스틱 위치 설정
            }

            if (_joyStick != null)
            {
                _joyStick.OnBeginDrag(eventData);              // JoyStick의 OnBeginDrag 호출
            }
        }
        
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            if (_joyStick != null)
            {
                _joyStick.OnDrag(eventData); // JoyStick의 OnDrag 호출
            }

        }
            
    }

    // 드래그 종료 시
    public void OnEndDrag(PointerEventData eventData)
    {
        if(_canDrag)
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

    public void SetCanDrag(bool canDrag)
    {
        _canDrag = canDrag;
    }
}
