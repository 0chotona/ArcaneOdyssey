using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_BossMob : MobFactory
{
    [Header("���� �� ������"), SerializeField] List<GameObject> _bossPrefs;
    public override void Spawn(eMOBTYPE type)
    {
        throw new System.NotImplementedException();
    }

}
