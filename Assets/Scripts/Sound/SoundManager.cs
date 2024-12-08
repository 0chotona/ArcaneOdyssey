using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SoundManager>();
            return instance;
        }
    }

    [Header("BGM 사운드"), SerializeField] SoundFactory<eBGMTYPE> _bgmSound;

    [Header("UI 사운드"), SerializeField] SoundFactory<eUISOUNDTYPE> _uiSound;
    [Header("스킬 사운드"), SerializeField] SoundFactory<eSKILLSOUNDTYPE> _skillSound;
    [Header("캐릭터 사운드"), SerializeField] SoundFactory<eCHARSOUNDTYPE> _charSound;
    private void Awake()
    {
        var obj = FindObjectsOfType<SoundManager>();
        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    /*
    public void PlaySound(eUISOUNDTYPE type)
    {
        _uiSound.PlaySound(type);
    }
    public void PlaySound(eSKILLSOUNDTYPE type)
    {
        _skillSound.PlaySound(type);
    }
    public void PlaySound(eCHARSOUNDTYPE type)
    {
        _charSound.PlaySound(type);
    }
    */
    public void PlaySound<T>(T soundType) where T : Enum
    {
        if (typeof(T) == typeof(eUISOUNDTYPE))
        {
            _uiSound.PlaySound((eUISOUNDTYPE)(object)soundType);
        }
        else if (typeof(T) == typeof(eSKILLSOUNDTYPE))
        {
            _skillSound.PlaySound((eSKILLSOUNDTYPE)(object)soundType);
        }
        else if (typeof(T) == typeof(eCHARSOUNDTYPE))
        {
            _charSound.PlaySound((eCHARSOUNDTYPE)(object)soundType);
        }
        else
        {
            Debug.LogWarning($"Unsupported sound type: {typeof(T)}");
        }
    }
    public void PlayBGM(eBGMTYPE type)
    {
        _bgmSound.PlaySound(type);
    }
    public void PlayClickButton()
    {
        _uiSound.PlaySound(eUISOUNDTYPE.Button);
    }
}
