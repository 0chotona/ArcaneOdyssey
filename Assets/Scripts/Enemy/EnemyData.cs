using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eNORMALMOB_TYPE
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

    
    

    public CEnemy(int hp, int att, int def, float moveSpeed)
    {
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
    Dictionary<eNORMALMOB_TYPE, CEnemy> _enemyDatas = new Dictionary<eNORMALMOB_TYPE, CEnemy>();
    public Dictionary<eNORMALMOB_TYPE, CEnemy> _EnemyDatas => _enemyDatas;

    private void Awake()
    {
        SetEnemyData();
    }

    void SetEnemyData()
    {
        _enemyDatas.Add(eNORMALMOB_TYPE.Cryogonal, new CEnemy(150, 10, 30, 2f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Bergmite, new CEnemy(250, 15, 30, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Avalugg, new CEnemy(350, 20, 40, 2f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Vanillite, new CEnemy(450, 30, 35, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Vanillish, new CEnemy(550, 35, 40, 3.5f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Vanilluxe, new CEnemy(650, 40, 45, 4f));


        _enemyDatas.Add(eNORMALMOB_TYPE.Regice, new CEnemy(1500, 50, 50, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Glalie, new CEnemy(2500, 80, 70, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Froslass, new CEnemy(3500, 110, 90, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Kyurem, new CEnemy(5000, 150, 110, 3f));
        /*
        _enemyDatas.Add(eNORMALMOB_TYPE.Cryogonal, new CEnemy(350, 10, 30, 2f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Bergmite, new CEnemy(450, 15, 30, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Avalugg, new CEnemy(550, 20, 40, 2f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Vanillite, new CEnemy(650, 30, 35, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Vanillish, new CEnemy(750, 35, 40, 3.5f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Vanilluxe, new CEnemy(850, 40, 45, 4f));


        _enemyDatas.Add(eNORMALMOB_TYPE.Regice, new CEnemy(1500, 50, 50, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Glalie, new CEnemy(2500, 80, 70, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Froslass, new CEnemy(3500, 110, 90, 3f));
        _enemyDatas.Add(eNORMALMOB_TYPE.Kyurem, new CEnemy(5000, 150, 110, 3f));
        */
    }
}
