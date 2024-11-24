using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobFactory : MonoBehaviour
{
    public Transform _playerTrs;
    public abstract void Spawn(eNORMALMOB_TYPE type, Vector3 pos);
    public abstract void SetData(Dictionary<eNORMALMOB_TYPE, CEnemy> enemyDatas);
    public abstract void SetTarget(Transform playerTrs);
    public abstract void SetSpawner(GameObject mobObj);
}
