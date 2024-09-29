using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYTYPE
{
    Zubat,
    Golbat,
    Crobat
}
public class CEnemy
{
    int _stage;
    public int _Stage => _stage;

    int _level;
    public int _Level => _level;

    int _hp;
    public int _Hp => _hp;

    int _att;
    public int _Att => _att;

    int _def;
    public int _Def => _def;

    float _moveSpeed;
    public float _MoveSpeed => _moveSpeed;

    
    

    public CEnemy(int stage, int level, int hp, int att, int def, float moveSpeed)
    {
        _stage = stage;
        _level = level;
        _hp = hp;
        _att = att;
        _def = def;
        _moveSpeed = moveSpeed;
    }
}
public class EnemyData : MonoBehaviour
{
    Dictionary<ENEMYTYPE, CEnemy> _enemyDatas = new Dictionary<ENEMYTYPE, CEnemy>();
    public Dictionary<ENEMYTYPE, CEnemy> _EnemyDatas => _enemyDatas;

    private void Awake()
    {
        SetEnemyData();
    }

    void SetEnemyData()
    {
        _enemyDatas.Add(ENEMYTYPE.Zubat, new CEnemy(1, 0, 10, 10, 5, 2f));
        _enemyDatas.Add(ENEMYTYPE.Golbat, new CEnemy(1, 1, 20, 20, 10, 3f));
        _enemyDatas.Add(ENEMYTYPE.Crobat, new CEnemy(1, 2, 30, 30, 15, 4f));
    }
}
