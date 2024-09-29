using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPartsInfo : MonoBehaviour
{
    [Header("¸Ê ÆÄÃ÷ ÁÂÇ¥"), SerializeField] Vector3 _partPos = Vector3.zero;

    public Vector3 _PartPos => _partPos;
    public void SetPartPos(Vector3 partPos)
    {
        _partPos = partPos;
    }
}
