using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    List<Attack> _attacks;

    public void AddAttack(Attack attack)
    {
        _attacks.Add(attack);
    }
}
