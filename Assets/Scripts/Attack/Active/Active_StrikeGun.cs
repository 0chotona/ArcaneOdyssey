using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_StrikeGun : MonoBehaviour, IActiveAttackable
{
    [Header("총알 발사 위치"), SerializeField] Transform _shootTrs;
    [Header("각도"), SerializeField] float _angle = 30f;
    [Header("거리"), SerializeField] float _distance = 20f;

    [Header("공격 횟수"), SerializeField] int _attCount = 8;
    [Header("공격 간격"), SerializeField] float _timeGap = 0.2f;

    [Header("라인 출력 횟수"), SerializeField] int _printCount = 16;

    [Header("발사 이펙트"), SerializeField] ParticleSystem _shootEffect;

    //[Header("발사 간격"), SerializeField] float _shootGap = 0.15f;
    [Header("유지 시간"), SerializeField] float _shootDur = 0.05f;

    [Header("데미지"), SerializeField] float _damage = 10f;

    LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
        _shootEffect.Stop();
    }
    public void ActiveInteract()
    {
        StartCoroutine(CRT_Active());
        StartCoroutine(CRT_PrintLine());
    }
    IEnumerator CRT_Active()
    {
        for(int i = 0; i < _attCount; i++)
        {
            yield return new WaitForSeconds(_timeGap);
            GiveDamage();
        }
        
    }
    void GiveDamage()
    {
        List<GameObject> targetList = GetEnemiesInFanShape();
        foreach(GameObject obj in targetList)
        {
            EnemyHealth enemyHealth = obj.GetComponent<EnemyHealth>();
            enemyHealth.LoseDamage(_damage);
        }
    }
    public List<GameObject> GetEnemiesInFanShape()
    {
        List<GameObject> enemiesInRange = new List<GameObject>();

        Collider[] hits = Physics.OverlapSphere(_shootTrs.position, _distance);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Vector3 directionToEnemy = (hit.transform.position - _shootTrs.position).normalized;

                float angleToEnemy = Vector3.Angle(_shootTrs.forward, directionToEnemy);

                if (angleToEnemy <= _angle)
                {
                    enemiesInRange.Add(hit.gameObject);
                }
            }
        }

        return enemiesInRange; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 leftBoundary = Quaternion.Euler(0, -_angle, 0) * _shootTrs.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, _angle, 0) * _shootTrs.forward;

        Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + leftBoundary * _distance);
        Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + rightBoundary * _distance);
    }
    void PrintLineRenderer(Vector3 hitPos)
    {
        _lineRenderer.SetPosition(0, _shootTrs.position);
        _lineRenderer.SetPosition(1, hitPos);
        _shootEffect.Play();
        _lineRenderer.enabled = true;
    }
    Vector3 GetRandomPos()
    {
        float randomAngle = Random.Range(-_angle, _angle);

        Quaternion randomRotation = Quaternion.Euler(0, randomAngle, 0);
        Vector3 randomDirection = randomRotation * _shootTrs.forward;

        Vector3 rndPos = _shootTrs.position + randomDirection.normalized * _distance;

        return rndPos;
    }
    IEnumerator CRT_PrintLine()
    {
        float timeGap = _attCount * _timeGap / _printCount;
        
        for (int i = 0; i < _printCount; i++)
        {
            Vector3 rndPos = GetRandomPos();
            PrintLineRenderer(rndPos);
            
            yield return new WaitForSeconds(_shootDur);
            _lineRenderer.enabled = false;
            yield return new WaitForSeconds(timeGap - _shootDur);
        }
        
    }
}
