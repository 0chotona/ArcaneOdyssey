using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public float _moveSpeed = 0;
    Animator _anim;
    private void Update()
    {
        if(_anim != null)
            _anim.SetFloat("moveSpeed", _moveSpeed);
    }
    public void SetAnimator()
    {
        _anim = GetComponentInChildren<Animator>();
    }
    public void SetAttackAnimation(int level)
    {
        _anim.SetTrigger("isAttack");
        _anim.SetInteger("SwordLevel", level);
    }
    public void SetAttackAnimation(bool isFinished)
    {
        _anim.SetBool("isFinished", isFinished);
    }
}
