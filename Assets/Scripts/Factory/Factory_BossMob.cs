using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_BossMob : MobFactory
{
    [Header("º¸½º ¸÷ ÇÁ¸®Æé"), SerializeField] List<GameObject> _bossPrefs;
    Dictionary<eNORMALMOB_TYPE, CEnemy> _enemyDatas = new Dictionary<eNORMALMOB_TYPE, CEnemy>();
    public override void Spawn(eNORMALMOB_TYPE type, Vector3 pos)
    {
        GameObject mob = GetMobByType(type);
        GameObject spawnedMob = Instantiate(mob, pos, Quaternion.identity);
        EnemyInfo info = spawnedMob.GetComponent<EnemyInfo>();
        info.SetStat(_enemyDatas[type]);

        EnemyMove move = spawnedMob.GetComponent<EnemyMove>();
        move.SetPlayerTrs(_playerTrs);
    }
    public GameObject GetMobByType(eNORMALMOB_TYPE type)
    {
        GameObject mob = null;
        foreach (GameObject obj in _bossPrefs)
        {
            EnemyInfo info = obj.GetComponent<EnemyInfo>();
            if (info._EnemyType == type)
            {
                mob = obj;
            }
        }
        return mob;
    }

    public override void SetData(Dictionary<eNORMALMOB_TYPE, CEnemy> enemyDatas)
    {
        _enemyDatas = enemyDatas;
    }

    public override void SetTarget(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }
}
