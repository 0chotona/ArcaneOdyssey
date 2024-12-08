using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSetter : MonoBehaviour
{
    [Header("BGM е╦ют"), SerializeField] eBGMTYPE _bgmType;
    private void OnEnable()
    {
        SoundManager.Instance.PlayBGM(_bgmType);
        /*
        switch (DataHandler.Instance._CurStage)
        {
            case 1:
                SoundManager.Instance.PlayBGM(eBGMTYPE.Stage1);
                break;
            case 2:
                Debug.Log(2);
                break;
        }
        */
    }
}
