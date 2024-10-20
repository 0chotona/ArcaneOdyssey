using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_NormalMob : MobFactory
{
    [Header("¸÷ ÇÁ¸®Æé"), SerializeField] List<GameObject> _normalPrefs;
    [Header("½ºÆù À§Ä¡"), SerializeField] List<Transform> _spawnPoses;
    public override void Spawn(eMOBTYPE type)
    {
        int rnd = Random.Range(0, _spawnPoses.Count);
        GameObject mob = GetMobByType(type);
        Instantiate(mob, _spawnPoses[rnd].position, Quaternion.identity);
    }
    public GameObject GetMobByType(eMOBTYPE type)
    {
        GameObject mob = null;
        foreach(GameObject obj in _normalPrefs)
        {
            EnemyInfo info = obj.GetComponent<EnemyInfo>();
            if(info._EnemyType == type)
            {
                mob = obj;
            }
        }
        return mob;
    }
}
