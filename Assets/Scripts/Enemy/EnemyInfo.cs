using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    CEnemy _cEnemy;
    [SerializeField] ENEMYTYPE _enemyType;

    EnemyMove _enemyMove;
    EnemyHealth _enemyHealth;
    EnemyAttack _enemyAttack;

    public ENEMYTYPE _EnemyType => _enemyType;

    private void Awake()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }
    public void SetStat(int hp, int att, int def, float moveSpeed)
    {
        _enemyHealth.SetEnemyStat(hp, def);
        _enemyMove.SetMoveSpeed(moveSpeed);
        _enemyAttack.SetEnemyAtt(att);
    }
}
