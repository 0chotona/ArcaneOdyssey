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
        
        // 비율을 맞추기 위해 원하는 기준 해상도로 나눈 비율 계산
        float widthRatio = screenWidth / 1920f; // 기준 해상도의 가로 비율
        float heightRatio = screenHeight / 1080f; // 기준 해상도의 세로 비율

        // 화면 비율에 맞게 Match 값 설정
        float match = Mathf.Abs(widthRatio - heightRatio);
        match = Mathf.Clamp01(match); // 0과 1 사이의 값으로 제한

        canvasScaler.matchWidthOrHeight = match; // 화면 비율에 맞게 Match 값 조정
        */
        //canvasScaler.referenceResolution = new Vector2(screenWidth, screenHeight);
    }
}
