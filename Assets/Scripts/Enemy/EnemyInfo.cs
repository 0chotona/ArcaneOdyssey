using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    CEnemy _cEnemy;
    [SerializeField] eNORMALMOB_TYPE _enemyType;

    [Header("�̵�"), SerializeField] EnemyMove _enemyMove;
    [Header("ü��"), SerializeField] EnemyHealth _enemyHealth;
    [Header("����"), SerializeField] EnemyAttack _enemyAttack;

    public eNORMALMOB_TYPE _EnemyType => _enemyType;

    public void SetStat(CEnemy enemy)
    {
        _enemyHealth.SetEnemyStat(enemy._Hp, enemy._Def);
        _enemyMove.SetMoveSpeed(enemy._MoveSpeed);
        _enemyAttack.SetEnemyAtt(enemy._Att);
    }
}
