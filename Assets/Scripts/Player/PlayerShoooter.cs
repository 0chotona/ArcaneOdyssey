
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoooter : MonoBehaviour
{
    //Header("¿À¸¥ÂÊ ÆÈ²ÞÄ¡"), SerializeField] Transform _elbowRTrs;
    [Header("¿ÞÂÊ ÆÈ²ÞÄ¡"), SerializeField] Transform _elbowLTrs;
    //[Header("¿À¸¥ÂÊ ¼Õ"), SerializeField] Transform _handRTrs;
    [Header("¿ÞÂÊ ¼Õ"), SerializeField] Transform _handLTrs;

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

