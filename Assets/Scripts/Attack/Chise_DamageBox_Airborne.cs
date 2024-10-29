using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Chise_DamageBox_Airborne : MonoBehaviour
{
    [Header("콜라이더"), SerializeField] Collider _collider;
    [Header("파티클"), SerializeField] ParticleSystem _particle;

    float _damage;
    Vector3 _boxScale = Vector3.one;
    bool _isMaxLevel = false;

    float _airborneTime = 0f;
    float _airborneSpeed = 0f;

    GameObject _tmpObj;
    private void Awake()
    {
        _collider.enabled = false;
        _tmpObj = gameObject;
    }
    public void SetAirborneSetting(float time, float speed)
    {
        _airborneTime = time;
        _airborneSpeed = speed;
    }
    public void UpdateDamage(float damage) { _damage = damage; }
    public void UpdateScale(Vector3 scale)
    {
        _boxScale = scale;
        transform.localScale = _boxScale;
    }
    public void StartAirborne()
    {
        StartCoroutine(CRT_Airborne());
    }
    IEnumerator CRT_Airborne()
    {
        _particle.Play();
        yield return new WaitForSeconds(1f);
        _collider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        _collider.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(_tmpObj);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
            EnemyMove enemyMove = other.GetComponent<EnemyMove>();
            enemyMove.GetAirborne(_airborneTime, _airborneSpeed);
        }
    }
}
