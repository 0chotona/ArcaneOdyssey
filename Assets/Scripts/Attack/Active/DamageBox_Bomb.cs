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

        // 플레이어가 바라보는 방향에서 위로 angle만큼 회전한 방향 설정 (x축을 기준으로)
        Quaternion rotation = Quaternion.AngleAxis(-angle, transform.right); // x축을 기준으로 회전
        _dir = rotation * _baseDir; // 회전 적용된 방향

        // _dir 방향으로 power를 적용하여 발사
        _rigid.AddForce(_dir * power, ForceMode.Impulse);
    }
    public void UpdateDamage(float damage) { _damage = damage; }
}
