using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    CEnemy _cEnemy;
    [SerializeField] eMOBTYPE _enemyType;

    [Header("�̵�"), SerializeField] EnemyMove _enemyMove;
    [Header("ü��"), SerializeField] EnemyHealth _enemyHealth;
    [Header("����"), SerializeField] EnemyAttack _enemyAttack;

    public eMOBTYPE _EnemyType => _enemyType;

    public void SetStat(int hp, int att, int def, float moveSpeed)
    {
        _enemyHealth.SetEnemyStat(hp, def);
        _enemyMove.SetMoveSpeed(moveSpeed);
        _enemyAttack.SetEnemyAtt(att);
    }
}
