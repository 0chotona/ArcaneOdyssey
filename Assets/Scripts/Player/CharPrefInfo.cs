using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPrefInfo : MonoBehaviour
{
    [Header("캐릭터 전용 공격"), SerializeField] GameObject _charAttackObj;

    public Attack GetCharAttack()
    {
        Attack attack = _charAttackObj.GetComponent<Attack>();
        return attack;
    }
}
