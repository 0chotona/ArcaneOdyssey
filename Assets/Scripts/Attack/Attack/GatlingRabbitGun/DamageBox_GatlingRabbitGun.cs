using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_GatlingRabbitGun : MonoBehaviour
{
    public List<Transform> _nearEnemies = new List<Transform>();

    [Header("�߻� ��ġ"), SerializeField] Transform _shootTrs;
    [Header("�� Layer"), SerializeField] LayerMask _enemyLayer;
    float _damage;
    float _slowAmount;
    float _slowDur;
    float _angle;
    float _distance;
    float _slowDamage;
    bool _isMaxLevel = false;
    public void UpdateDamage(float damage) { _damage = damage; }
    public void SetSlow(float amount, float dur) 
    { 
        _slowAmount = amount;
        _slowDur = dur;
    }
    public void SetAngle(float angle, float distance) 
    { 
        _angle = angle;
        _distance = distance;
    }
    public void SetSlowDamage(float slowDamage) { _slowDamage = slowDamage; }
    public void SetIsMaxLevel(bool IsMaxLevel) { _isMaxLevel = IsMaxLevel; }
    
    public void GiveInteract()
    {
        _nearEnemies.RemoveAll(target => target == null);
        GetEnemiesInFanShape();
        foreach (Transform t in _nearEnemies)
        {
            if(_isMaxLevel)
            {
                GiveSlow(t);
            }
            GiveDamage(t, _damage);
        }
    }
    void GiveDamage(Transform trs, float damage)
    {
        EnemyHealth enemyHealth = trs.GetComponent<EnemyHealth>();
        enemyHealth.LoseDamage(damage);
    }
    void GiveSlow(Transform trs)
    {
        EnemyMove enemyMove = trs.GetComponent<EnemyMove>();
        if(!enemyMove._IsSlow)
        {
            enemyMove.GetSlow(_slowDur, _slowAmount);
            EnemyHealth enemyHealth = trs.GetComponent<EnemyHealth>();
            StartCoroutine(CRT_SetDamageIncrease(enemyHealth, _slowDur));
        }
    }
    IEnumerator CRT_SetDamageIncrease(EnemyHealth enemyHealth, float time)
    {
        enemyHealth.SetDamageIncrease(_slowDamage);
        yield return new WaitForSeconds(time);
        enemyHealth.SetDamageIncrease(-_slowDamage);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
    void GetEnemiesInFanShape()
    {
        List<GameObject> enemiesInRange = new List<GameObject>();

        // ������ �Ÿ���ŭ ��ü ���� ���� �� Ž��
        Collider[] hits = Physics.OverlapSphere(_shootTrs.position, _distance, _enemyLayer);

        foreach (Collider hit in hits)
        {
            // �� ������Ʈ�� Enemy �±׸� ������ �ִ��� Ȯ��
            if (hit.CompareTag("Enemy"))
            {
                Vector3 directionToEnemy = (hit.transform.position - _shootTrs.position).normalized;

                // �÷��̾ �ٶ󺸴� ����� ���� ��ġ�� ��ä�� �ȿ� �ִ��� Ȯ��
                float angleToEnemy = Vector3.Angle(_shootTrs.forward, directionToEnemy);

                // ���� ������ ��ä�� ���� �ȿ� ���� ��� ����Ʈ�� �߰�
                if (angleToEnemy <= _angle)
                {
                    enemiesInRange.Add(hit.gameObject);
                    _nearEnemies.Add(hit.transform);
                }
            }
        }

    }

    // ����׿� ��ä�� �ð�ȭ
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // ��ä���� ���۰� �� ���� ���
        Vector3 leftBoundary = Quaternion.Euler(0, -_angle, 0) * _shootTrs.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, _angle, 0) * _shootTrs.forward;

        // ��ä���� ���� �� �׸���
        Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + leftBoundary * _distance);
        Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + rightBoundary * _distance);
    }
}
