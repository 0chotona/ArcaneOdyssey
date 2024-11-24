using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_BossMob : MobFactory
{
    [Header("보스 몹 프리펩"), SerializeField] List<GameObject> _bossPrefs;
    [Header("아이템 스포너"), SerializeField] ItemSpawner _itemSpawner;
    Dictionary<eNORMALMOB_TYPE, CEnemy> _enemyDatas = new Dictionary<eNORMALMOB_TYPE, CEnemy>();
    public override void Spawn(eNORMALMOB_TYPE type, Vector3 pos)
    {
        GameObject mob = GetMobByPrefName(_enemyDatas[type]._PrefName);
        GameObject spawnedMob = Instantiate(mob, pos, Quaternion.identity);
        SetSpawner(spawnedMob);
        EnemyInfo info = spawnedMob.GetComponent<EnemyInfo>();
        info.SetStat(_enemyDatas[type]);

        EnemyMove move = spawnedMob.GetComponent<EnemyMove>();
        move.SetPlayerTrs(_playerTrs);
    }
    public GameObject GetMobByPrefName(string prefName)
    {
        GameObject mob = null;
        foreach (GameObject obj in _bossPrefs)
        {
            if (obj.name == prefName)
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

    public override void SetSpawner(GameObject obj)
    {
        EnemyInfo enemyInfo = obj.GetComponent<EnemyInfo>();
        CEnemy cEnemy = _enemyDatas[enemyInfo._EnemyType];
        //enemyInfo.SetStat(cEnemy._Hp, cEnemy._Att + _curLevel, cEnemy._Def + _curLevel, cEnemy._MoveSpeed);

        obj.GetComponent<EnemyHealth>().SetSpawner(transform, _itemSpawner);
        obj.GetComponent<EnemyMove>().SetPlayerTrs(_playerTrs);

    }
}
