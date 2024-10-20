using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_BossMob : MobFactory
{
    [Header("º¸½º ¸÷ ÇÁ¸®Æé"), SerializeField] List<GameObject> _bossPrefs;
    public override void Spawn(eMOBTYPE type)
    {
        throw new System.NotImplementedException();
    }

}
