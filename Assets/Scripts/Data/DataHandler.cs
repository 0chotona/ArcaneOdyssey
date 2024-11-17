using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    static DataHandler _instance;

    public static DataHandler Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<DataHandler>();
            return _instance;
        }

    }
    EnemyData _enemyData;
    Dictionary<eNORMALMOB_TYPE, CEnemy> _enemyDic = new Dictionary<eNORMALMOB_TYPE, CEnemy>();
    public Dictionary<eNORMALMOB_TYPE, CEnemy> _EnemyDic => _enemyDic;
    int _curStage = 0;
    public int _CurStage => _curStage;
    private void Awake()
    {
        var obj = FindObjectsOfType<DataHandler>();
        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    public void SetData(EnemyData data)
    {
        _enemyData = data;
        _enemyDic = data._EnemyDatas;
    }
    public void SetCurStage(int stage)
    {
        _curStage = stage;
    }
}
