using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Bomb : MonoBehaviour, IActiveAttackable
{
    [Header("폭탄 데미지 박스"), SerializeField] GameObject _damageBoxObj;
    DamageBox_Bomb _damageBox;

    [Header("데미지"), SerializeField] float _damage = 10f;
    [Header("y 각도"), SerializeField] float _yAngle = 60f;
    [Header("x 각도"), SerializeField] float _xAngle = 30f;
    [Header("발사 파워"), SerializeField] float _shootPower = 50f;

    public void ActiveInteract()
    {
        Quaternion rot = transform.rotation;

        float leftAngle = -_xAngle;
        float rightAngle = _xAngle;

        SpawnBomb(rot);

        Quaternion leftRot = Quaternion.Euler(0, leftAngle, 0) * transform.rotation;
        SpawnBomb(leftRot);

        Quaternion rightRot = Quaternion.Euler(0, rightAngle, 0) * transform.rotation;
        SpawnBomb(rightRot);

    }
    void SpawnBomb(Quaternion rot)
    {
        GameObject bombObj = Instantiate(_damageBoxObj, transform.position, rot);
        _damageBox = bombObj.GetComponent<DamageBox_Bomb>();
        _damageBox.UpdateDamage(_damage);

        
        _damageBox.Shoot(_yAngle, _shootPower);
    }
    public void SetPlayerTrs(Transform playerTrs) { }
}
