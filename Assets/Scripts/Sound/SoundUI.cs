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
    
    [Header("��ư Ŭ�� ����"), SerializeField] AudioSource _buttonSound;
    [Header("���� ���� ����"), SerializeField] AudioSource _gameStartSound;
    [Header("������ ���� ����"), SerializeField] AudioSource _getItemSound;

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
