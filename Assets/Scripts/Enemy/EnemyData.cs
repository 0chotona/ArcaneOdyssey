using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMOBTYPE
{
    Cryogonal,
    Bergmite,
    Avalugg,
    Vanillite,
    Vanillish,
    Vanilluxe,

    Regice,
    Glalie,
    Froslass,
    Kyurem
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
    bool _isBoss = false;
    public bool _IsBoss => _isBoss; 

    
    

    public CEnemy(int stage, int level, int hp, int att, int def, float moveSpeed)
    {
        _stage = stage;
        _level = level;
        _hp = hp;
        _att = att;
        _def = def;
        _moveSpeed = moveSpeed;
    }
    public void SetIsBoss()
    {
        _isBoss = true;
    }
}
public class EnemyData : MonoBehaviour
{
    Dictionary<eMOBTYPE, CEnemy> _enemyDatas = new Dictionary<eMOBTYPE, CEnemy>();
    public Dictionary<eMOBTYPE, CEnemy> _EnemyDatas => _enemyDatas;

    private void Awake()
    {
        SetEnemyData();
    }

    void SetEnemyData()
    {
        _enemyDatas.Add(eMOBTYPE.Vanillite, new CEnemy(1, 0, 120, 10, 30, 2f));
        _enemyDatas.Add(eMOBTYPE.Vanillish, new CEnemy(1, 1, 180, 20, 40, 3f));
        _enemyDatas.Add(eMOBTYPE.Vanilluxe, new CEnemy(1, 2, 240, 30, 50, 4f));
        _enemyDatas.Add(eMOBTYPE.Cryogonal, new CEnemy(1, 2, 240, 30, 50, 4f));
    }
}
