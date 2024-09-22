using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public GameObject _effect;
    public GameObject _finalEffect;
    public bool _canAttack;
    public int _level;

    public CSkill _skill;

    public eSKILL _name;

    //데미지 / 공격횟수 / 공격범위 / 쿨타임 / 유지시간
    public float _damage;
    public int _attCount;
    public float _attRange;
    public float _coolTime;
    public float _durTime;
    public float _shotSpeed;

    public abstract void StartAttack();
    public abstract void AttackInteract();
    public abstract IEnumerator CRT_Attack();
    public abstract void UpdateStat(CStat stat);
    public abstract void SetSkill(CSkill skill);
}
