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
public enum eMOB_COLUMN
{
    ID,
    Mob_Name,
    Enum_Name,
    Pref_Name,
    Hp,
    Att,
    Def,
    MoveSpeed,
    IsBoss
}
public class CEnemy
{
    int _id;
    public int _Id => _id;

    string _mobName;
    public string _MobName => _mobName;

    eNORMALMOB_TYPE _enumName;
    public eNORMALMOB_TYPE _EnumName => _enumName;
    string _prefName;
    public string _PrefName => _prefName;
    float _hp;
    public float _Hp => _hp;

    float _att;
    public float _Att => _att;

    float _def;
    public float _Def => _def;

    float _moveSpeed;
    public float _MoveSpeed => _moveSpeed;
    bool _isBoss = false;
    public bool _IsBoss => _isBoss; 

    
    

    public CEnemy(int id, string mobName, eNORMALMOB_TYPE enumName, string prefName, float hp, float att, float def, float moveSpeed, bool isBoss)
    {
        _id = id;
        _mobName = mobName;
        _enumName = enumName;
        _prefName = prefName;
        _hp = hp;
        _att = att;
        _def = def;
        _moveSpeed = moveSpeed;
        _isBoss = isBoss;
    }
}
public class EnemyData : MonoBehaviour, ICSVDataConverter
{
    Dictionary<eNORMALMOB_TYPE, CEnemy> _enemyDatas = new Dictionary<eNORMALMOB_TYPE, CEnemy>();
    public Dictionary<eNORMALMOB_TYPE, CEnemy> _EnemyDatas => _enemyDatas;
    public void ConvertToDictionary(List<string> rows)
    {
        rows.RemoveAt(0); // 첫 줄(컬럼 헤더) 제거
        rows.RemoveAt(rows.Count - 1);  // 마지막 공백 행 제거

        foreach (string row in rows)
        {
            string[] values = row.Split(',');

            int id = int.Parse(values[(int)eMOB_COLUMN.ID]);
            string charName = values[(int)eMOB_COLUMN.Mob_Name];
            eNORMALMOB_TYPE enumName = (eNORMALMOB_TYPE)System.Enum.Parse(typeof(eNORMALMOB_TYPE), values[(int)eMOB_COLUMN.Enum_Name]);
            string prefName = values[(int)eMOB_COLUMN.Pref_Name];
            float hp = float.Parse(values[(int)eMOB_COLUMN.Hp]);
            float att = float.Parse(values[(int)eMOB_COLUMN.Att]);
            float def = float.Parse(values[(int)eMOB_COLUMN.Def]);
            float moveSpeed = float.Parse(values[(int)eMOB_COLUMN.MoveSpeed]);
            bool isBoss = (int.Parse(values[(int)eMOB_COLUMN.IsBoss]) == 1);

            CEnemy mob = new CEnemy(id, charName, enumName, prefName, hp, att, def, moveSpeed, isBoss);

            _enemyDatas.Add(enumName, mob);
        }
    }
    /*
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
        
    }*/
}
