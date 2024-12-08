using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eCHARSOUNDTYPE
{
    Momoi_Attack,
    Momoi_E,
    Momoi_R,
    Momoi_R_Explosion,
    Chise_Attack,
    Chise_Airbone,
    Chise_E,
    Chise_R,
    Chise_RAttack

}
public class SoundChar : SoundFactory<eCHARSOUNDTYPE>
{
    [Header("모모이 공격 사운드"), SerializeField] AudioSource _momoiAttackSound;
    [Header("모모이 E 사운드"), SerializeField] AudioSource _momoiESound;
    [Header("모모이 R 사운드"), SerializeField] AudioSource _momoiRSound;
    [Header("모모이 R 폭발 사운드"), SerializeField] AudioSource _momoiRExplosionSound;

    [Header("치세 공격 사운드"), SerializeField] AudioSource _chiseAttackSound;
    [Header("치세 에어본 사운드"), SerializeField] AudioSource _chiseAirboneSound;
    [Header("치세 E 사운드"), SerializeField] AudioSource _chiseESound;
    [Header("치세 R 사운드"), SerializeField] AudioSource _chiseRSound;
    [Header("치세 R 검기 사운드"), SerializeField] AudioSource _chiseRAttackSound;

    public override void PlaySound(eCHARSOUNDTYPE soundType)
    {
        switch (soundType)
        {
            case eCHARSOUNDTYPE.Momoi_Attack:
                _momoiAttackSound.Play();
                break;
            case eCHARSOUNDTYPE.Momoi_E:
                _momoiESound.Play();
                break;
            case eCHARSOUNDTYPE.Momoi_R:
                _momoiRSound.Play();
                break;
            case eCHARSOUNDTYPE.Momoi_R_Explosion:
                _momoiRExplosionSound.Play();
                break;
            case eCHARSOUNDTYPE.Chise_Attack:
                _chiseAttackSound.Play();
                break;
            case eCHARSOUNDTYPE.Chise_Airbone:
                _chiseAirboneSound.Play();
                break;
            case eCHARSOUNDTYPE.Chise_E:
                _chiseESound.Play();
                break;
            case eCHARSOUNDTYPE.Chise_R:
                _chiseRSound.Play();
                break;
            case eCHARSOUNDTYPE.Chise_RAttack:
                _chiseRAttackSound.Play();
                break;
        }
    }
}
