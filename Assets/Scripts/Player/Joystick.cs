using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : InputController, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform _lever;        // 조이스틱 레버
    [SerializeField] Transform _arrowTrs;        // 화살표 Transform
    [SerializeField] float _leverRange = 50f;    // 레버 이동 범위
    RectTransform _rectTransform;                       // 입력 방향 (항상 크기 1)

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void ControlLever(PointerEventData eventData)
    {
        // 스크린 좌표를 UI의 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        // 드래그된 방향 계산
        Vector2 direction = localPoint;
        Vector2 clampedDir = Vector2.ClampMagnitude(direction, _leverRange);

        // 레버 위치 업데이트
        _lever.anchoredPosition = clampedDir;

        // 입력 방향 업데이트 (크기 1로 정규화)
        _inputDir = clampedDir.sqrMagnitude > 0 ? clampedDir.normalized : Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlLever(eventData);  // 드래그 시작 시 레버 위치 업데이트
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);  // 드래그 중 레버 위치 업데이트

        // 화살표 회전
        float angle = Mathf.Atan2(_inputDir.y, _inputDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        _arrowTrs.rotation = rotation;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 레버와 입력 초기화
        _lever.anchoredPosition = Vector2.zero;
        _inputDir = Vector2.zero;
    }
}
