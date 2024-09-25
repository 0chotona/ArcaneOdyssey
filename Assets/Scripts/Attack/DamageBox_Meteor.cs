using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Meteor : MonoBehaviour
{

    float _damage;
    Vector3 _boxScale = new Vector3(1, 1, 1);
    GameObject _tmpObj;


    [SerializeField] GameObject _explodeObj;
    private void Awake()
    {
        _tmpObj = gameObject;

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ground"))
        {

            GameObject explodeObj = Instantiate(_explodeObj, transform.position, Quaternion.identity);
            Explosion explosion = explodeObj.GetComponent<Explosion>();
            explosion.UpdateDamage(_damage);
            explosion.DestroyObj(1f);
            Destroy(_tmpObj);
        }
    }

    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(float boxSize) { transform.localScale = _boxScale * boxSize; }

}
