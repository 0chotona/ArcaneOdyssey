using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eSKILLSOUNDTYPE
{
    Boomerang,
    IceArmor,
    BunnyCrossbow,
    RadiantBarrier,
    CuteLauncher,
    EchoBatBlade
}
public class SoundSkill : SoundFactory<eSKILLSOUNDTYPE>
{

    [Header("�θ޶�"), SerializeField] AudioSource _boomerang;
    [Header("�����۷� ����"), SerializeField] AudioSource _iceArmor;
    [Header("���� �䳢 ����"), SerializeField] AudioSource _bunnyCrossbow;
    [Header("���� ����"), SerializeField] AudioSource _radiantBarrier;
    [Header("�Ϳ��� �߻��"), SerializeField] AudioSource _cuteLauncher;
    [Header("�޾Ƹ�ġ�� ����Į��"), SerializeField] AudioSource _echoBatBlade;

    public override void PlaySound(eSKILLSOUNDTYPE soundType)
    {
        switch (soundType)
        {
            case eSKILLSOUNDTYPE.Boomerang:
                _boomerang.Play();
                break;
            case eSKILLSOUNDTYPE.IceArmor:
                _iceArmor.Play();
                break;
            case eSKILLSOUNDTYPE.BunnyCrossbow:
                _bunnyCrossbow.Play();
                break;
            case eSKILLSOUNDTYPE.RadiantBarrier:
                _radiantBarrier.Play();
                break;
            case eSKILLSOUNDTYPE.CuteLauncher:
                _cuteLauncher.Play();
                break;
            case eSKILLSOUNDTYPE.EchoBatBlade:
                _echoBatBlade.Play();
                break;

        }
    }
}
