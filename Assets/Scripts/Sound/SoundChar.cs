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
    [Header("����� ���� ����"), SerializeField] AudioSource _momoiAttackSound;
    [Header("����� E ����"), SerializeField] AudioSource _momoiESound;
    [Header("����� R ����"), SerializeField] AudioSource _momoiRSound;
    [Header("����� R ���� ����"), SerializeField] AudioSource _momoiRExplosionSound;

    [Header("ġ�� ���� ����"), SerializeField] AudioSource _chiseAttackSound;
    [Header("ġ�� ��� ����"), SerializeField] AudioSource _chiseAirboneSound;
    [Header("ġ�� E ����"), SerializeField] AudioSource _chiseESound;
    [Header("ġ�� R ����"), SerializeField] AudioSource _chiseRSound;
    [Header("ġ�� R �˱� ����"), SerializeField] AudioSource _chiseRAttackSound;

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
