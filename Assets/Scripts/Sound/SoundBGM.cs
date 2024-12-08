using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGMTYPE
{
    Lobby,
    Stage1,
    Stage2

}
public class SoundBGM : SoundFactory<eBGMTYPE>
{
    [Header("로비 BGM"), SerializeField] AudioSource _lobbyBGM;
    [Header("스테이지1 BGM"), SerializeField] AudioSource _stage1BGM;
    [Header("스테이지2 BGM"), SerializeField] AudioSource _stage2BGM;

    public override void PlaySound(eBGMTYPE soundType)
    {
        _lobbyBGM.Stop();
        _stage1BGM.Stop();
        _stage2BGM.Stop();
        switch (soundType)
        {
            case eBGMTYPE.Lobby:
                _lobbyBGM.Play();
                break;
            case eBGMTYPE.Stage1:
                _stage1BGM.Play();
                break;
            case eBGMTYPE.Stage2:
                _stage2BGM.Play();
                break;
        }
    }
}
