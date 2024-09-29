using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    CEnemy _cEnemy;
    [SerializeField] ENEMYTYPE _enemyType;

    [Header("이동"), SerializeField] EnemyMove _enemyMove;
    [Header("체력"), SerializeField] EnemyHealth _enemyHealth;
    [Header("공격"), SerializeField] EnemyAttack _enemyAttack;

    public ENEMYTYPE _EnemyType => _enemyType;

    public void SetStat(int hp, int att, int def, float moveSpeed)
    {
        _enemyHealth.SetEnemyStat(hp, def);
        _enemyMove.SetMoveSpeed(moveSpeed);
        _enemyAttack.SetEnemyAtt(att);
    }
}
