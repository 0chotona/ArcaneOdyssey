using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCanvasScaler : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    void Start()
    {
        /*
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        
        // ������ ���߱� ���� ���ϴ� ���� �ػ󵵷� ���� ���� ���
        float widthRatio = screenWidth / 1920f; // ���� �ػ��� ���� ����
        float heightRatio = screenHeight / 1080f; // ���� �ػ��� ���� ����

        // ȭ�� ������ �°� Match �� ����
        float match = Mathf.Abs(widthRatio - heightRatio);
        match = Mathf.Clamp01(match); // 0�� 1 ������ ������ ����

        canvasScaler.matchWidthOrHeight = match; // ȭ�� ������ �°� Match �� ����
        */
        //canvasScaler.referenceResolution = new Vector2(screenWidth, screenHeight);
    }
}
