using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int _att;
    [Header("ÄðÅ¸ÀÓ"), SerializeField] float _attCoolTime = 2f;

    Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = true;
    }
    public void SetEnemyAtt(int att)
    {
        _att = att;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerHitbox"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.GetDamage(_att);
            StartCoroutine(CRT_SetAttCoolTime());
        }
    }
    IEnumerator CRT_SetAttCoolTime()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(_attCoolTime);
        _collider.enabled = true;
    }
}
