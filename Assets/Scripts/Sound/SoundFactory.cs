using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundFactory<T> : MonoBehaviour where T : Enum
{
    public abstract void PlaySound(T soundType);
}
