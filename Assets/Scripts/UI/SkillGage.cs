using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGage : MonoBehaviour
{
    [Header("슬라이더"), SerializeField] Slider _skillSlider;
    private void Awake()
    {
        if(_skillSlider == null)
        {
            _skillSlider = GetComponent<Slider>();
        }
    }
    public void SetSliderSetting(float max)
    {
        _skillSlider.maxValue = max;
        _skillSlider.value = 0f;
    }
    public void UpdateSkillGage(float value)
    {
        _skillSlider.value = value;
    }
}
