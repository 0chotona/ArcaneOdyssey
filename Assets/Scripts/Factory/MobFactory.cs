using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobFactory : MonoBehaviour
{
    public abstract void Spawn(eMOBTYPE type);
}
