using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Bomb : MonoBehaviour, IActiveAttackable
{
    [Header("��ź ������ �ڽ�"), SerializeField] GameObject _damageBoxObj;
    DamageBox_Bomb _damageBox;

    [Header("������"), SerializeField] float _damage = 10f;
    [Header("y ����"), SerializeField] float _yAngle = 60f;
    [Header("x ����"), SerializeField] float _xAngle = 30f;
    [Header("�߻� �Ŀ�"), SerializeField] float _shootPower = 50f;

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
