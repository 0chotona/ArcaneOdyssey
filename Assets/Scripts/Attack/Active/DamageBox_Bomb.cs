using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DamageBox_Bomb : MonoBehaviour
{
    float _damage;

    [SerializeField] GameObject _explodeObj;
    GameObject _tmpObj;
    Rigidbody _rigid;

    Vector3 _dir;
    int _count = 0;
    int _maxCount = 3;

    Vector3 _baseDir;
    private void Awake()
    {
        _tmpObj = gameObject;
        _rigid = GetComponent<Rigidbody>();

        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ground"))
        {
            GameObject explodeObj = Instantiate(_explodeObj, transform.position, Quaternion.identity);
            Explosion explosion = explodeObj.GetComponent<Explosion>();
            explosion.UpdateDamage(_damage);
            explosion.DestroyObj(1f);
            _count++;
            if(_count >= _maxCount)
            {
                
                Destroy(_tmpObj);
            }
            
        }
    }
    public void Shoot(float angle, float power)
    {

        _baseDir = transform.forward;
        _baseDir.Normalize();

        // �÷��̾ �ٶ󺸴� ���⿡�� ���� angle��ŭ ȸ���� ���� ���� (x���� ��������)
        Quaternion rotation = Quaternion.AngleAxis(-angle, transform.right); // x���� �������� ȸ��
        _dir = rotation * _baseDir; // ȸ�� ����� ����

        // _dir �������� power�� �����Ͽ� �߻�
        _rigid.AddForce(_dir * power, ForceMode.Impulse);
    }
    public void UpdateDamage(float damage) { _damage = damage; }
}
