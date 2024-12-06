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

    [Header("UI 사운드"), SerializeField] SoundFactory<eUISOUNDTYPE> _uiSound;
    [Header("스킬 사운드"), SerializeField] SoundFactory<eSKILLSOUNDTYPE> _skillSound;
    private void Awake()
    {
        var obj = FindObjectsOfType<SoundManager>();
        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    public void PlaySound(eUISOUNDTYPE type)
    {
        _uiSound.PlaySound(type);
    }
    public void PlaySound(eSKILLSOUNDTYPE type)
    {
        _skillSound.PlaySound(type);
    }
    public void PlayClickButton()
    {
        _uiSound.PlaySound(eUISOUNDTYPE.Button);
    }
}
