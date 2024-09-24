
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoooter : MonoBehaviour
{
    //Header("������ �Ȳ�ġ"), SerializeField] Transform _elbowRTrs;
    [Header("���� �Ȳ�ġ"), SerializeField] Transform _elbowLTrs;
    //[Header("������ ��"), SerializeField] Transform _handRTrs;
    [Header("���� ��"), SerializeField] Transform _handLTrs;

    Animator _anim;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        /*
        _elbowRTrs.position = _anim.GetIKHintPosition(AvatarIKHint.RightElbow);
        _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);

        _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        _anim.SetIKPosition(AvatarIKGoal.RightHand, _handRTrs.position);

        _anim.SetIKRotation(AvatarIKGoal.RightHand, _handRTrs.rotation);
        */
        _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

        _anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        _anim.SetIKPosition(AvatarIKGoal.LeftHand, _handLTrs.position);

        _anim.SetIKRotation(AvatarIKGoal.LeftHand, _handLTrs.rotation);

        _elbowLTrs.position = _anim.GetIKHintPosition(AvatarIKHint.LeftElbow);
        
    }
}

