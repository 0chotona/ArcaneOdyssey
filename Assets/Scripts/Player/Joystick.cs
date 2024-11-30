using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : InputController, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform _lever;        // ���̽�ƽ ����
    [SerializeField] Transform _arrowTrs;        // ȭ��ǥ Transform
    [SerializeField] float _leverRange = 50f;    // ���� �̵� ����
    RectTransform _rectTransform;                       // �Է� ���� (�׻� ũ�� 1)

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void ControlLever(PointerEventData eventData)
    {
        // ��ũ�� ��ǥ�� UI�� ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        // �巡�׵� ���� ���
        Vector2 direction = localPoint;
        Vector2 clampedDir = Vector2.ClampMagnitude(direction, _leverRange);

        // ���� ��ġ ������Ʈ
        _lever.anchoredPosition = clampedDir;

        // �Է� ���� ������Ʈ (ũ�� 1�� ����ȭ)
        _inputDir = clampedDir.sqrMagnitude > 0 ? clampedDir.normalized : Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlLever(eventData);  // �巡�� ���� �� ���� ��ġ ������Ʈ
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);  // �巡�� �� ���� ��ġ ������Ʈ

        // ȭ��ǥ ȸ��
        float angle = Mathf.Atan2(_inputDir.y, _inputDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        _arrowTrs.rotation = rotation;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� ������ �Է� �ʱ�ȭ
        _lever.anchoredPosition = Vector2.zero;
        _inputDir = Vector2.zero;
    }
}
