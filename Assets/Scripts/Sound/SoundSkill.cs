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

    [Header("ºÎ¸Þ¶û"), SerializeField] AudioSource _boomerang;
    [Header("¾óÀ½ÀÛ·Ä °©¿Ê"), SerializeField] AudioSource _iceArmor;
    [Header("ÀüÅõ Åä³¢ ¼®±Ã"), SerializeField] AudioSource _bunnyCrossbow;
    [Header("±¤ÈÖ ¿ªÀå"), SerializeField] AudioSource _radiantBarrier;
    [Header("±Í¿©¿î ¹ß»ç±â"), SerializeField] AudioSource _cuteLauncher;
    [Header("¸Þ¾Æ¸®Ä¡´Â ¹ÚÁãÄ®³¯"), SerializeField] AudioSource _echoBatBlade;

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
