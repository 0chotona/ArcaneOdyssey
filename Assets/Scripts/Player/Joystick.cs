using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : InputController, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform _lever;
    [SerializeField] Transform _arrowTrs;
    RectTransform _rectTransform;
    [SerializeField] float _leverRange = 50;

    public override void Update()
    {
        
    }
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    void ControlLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - _rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < _leverRange ? inputDir : inputDir.normalized * _leverRange;
        _lever.anchoredPosition = clampedDir;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlLever(eventData);

    }
    public void OnDrag(PointerEventData eventData)
    {
        _inputDir = (_lever.anchoredPosition / _leverRange).normalized;

        float angle = Mathf.Atan2(_inputDir.y, _inputDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        _arrowTrs.rotation = rotation;

        ControlLever(eventData);

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _lever.anchoredPosition = Vector2.zero;
        _inputDir = Vector2.zero;
    }
}