using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_GatlingRabbitGun : MonoBehaviour
{
    public List<Transform> _nearEnemies = new List<Transform>();

    [Header("발사 위치"), SerializeField] Transform _shootTrs;
    [Header("적 Layer"), SerializeField] LayerMask _enemyLayer;
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

        // 지정된 거리만큼 구체 범위 내의 적 탐지
        Collider[] hits = Physics.OverlapSphere(_shootTrs.position, _distance, _enemyLayer);

        foreach (Collider hit in hits)
        {
            // 적 오브젝트가 Enemy 태그를 가지고 있는지 확인
            if (hit.CompareTag("Enemy"))
            {
                Vector3 directionToEnemy = (hit.transform.position - _shootTrs.position).normalized;

                // 플레이어가 바라보는 방향과 적의 위치가 부채꼴 안에 있는지 확인
                float angleToEnemy = Vector3.Angle(_shootTrs.forward, directionToEnemy);

                // 적이 지정된 부채꼴 각도 안에 있을 경우 리스트에 추가
                if (angleToEnemy <= _angle)
                {
                    enemiesInRange.Add(hit.gameObject);
                    _nearEnemies.Add(hit.transform);
                }
            }
        }

    }

    // 디버그용 부채꼴 시각화
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // 부채꼴의 시작과 끝 각도 계산
        Vector3 leftBoundary = Quaternion.Euler(0, -_angle, 0) * _shootTrs.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, _angle, 0) * _shootTrs.forward;

        // 부채꼴의 양쪽 선 그리기
        Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + leftBoundary * _distance);
        Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + rightBoundary * _distance);
    }
}
