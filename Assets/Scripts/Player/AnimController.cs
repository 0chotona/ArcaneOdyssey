using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public float _moveSpeed = 0;
    Animator _anim;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        _anim.SetFloat("moveSpeed", _moveSpeed);
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
