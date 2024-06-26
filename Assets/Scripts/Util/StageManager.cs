using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] GameObject[] _enemyObj;
    [SerializeField] GameObject[] _bossObj;

    [SerializeField] int _stageNum;

    float _waveTerm = 60f;
    float _enemyTerm = 6f;

    public int _curWave = 0;
    int _curEnemy = 0;

    float _timer = 0;
    private void Awake()
    {
        
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > (_curWave + 1) * _waveTerm)
            _curWave++;

        if (_timer > (_curEnemy + 1) * _enemyTerm)
            _curEnemy++;
    }

}
