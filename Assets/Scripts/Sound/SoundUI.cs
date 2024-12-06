using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eUISOUNDTYPE
{
    Button,
    GameStart,
    GetItem,

}

public class SoundUI : SoundFactory<eUISOUNDTYPE>
{
    
    [Header("버튼 클릭 사운드"), SerializeField] AudioSource _buttonSound;
    [Header("게임 시작 사운드"), SerializeField] AudioSource _gameStartSound;
    [Header("아이템 선택 사운드"), SerializeField] AudioSource _getItemSound;

    public override void PlaySound(eUISOUNDTYPE soundType)
    {
        switch (soundType)
        {
            case eUISOUNDTYPE.Button:
                _buttonSound.Play();
                break;
            case eUISOUNDTYPE.GameStart:
                _gameStartSound.Play();
                break;
            case eUISOUNDTYPE.GetItem:
                _getItemSound.Play();
                break;
        }
    }
}
