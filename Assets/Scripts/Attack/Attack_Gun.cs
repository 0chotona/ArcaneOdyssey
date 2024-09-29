using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Gun : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Gun _damageBox;

    LineRenderer _lineRenderer;
    [Header("총알 발사 위치"), SerializeField] Transform _shootTrs;
    [Header("총알 이펙트 발사 위치"), SerializeField] Transform _shootLineTrs;
    [Header("발사 간격"), SerializeField] float _shootGap = 0.15f;
    [Header("유지 시간"), SerializeField] float _shootDur = 0.05f;
    [Header("발사 이펙트"), SerializeField] ParticleSystem _shootEffect;
    [Header("총알 두께"), SerializeField] float _thickness = 0.1f;
    Vector3 _dir;
    float _distance = 15f;

    private void OnEnable()
    {
        _name = eSKILL.Gun;
    }
    private void Start()
    {
        StartCoroutine(CRT_Attack());
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;

        _isMaxLevel = false;
        _shootEffect.Stop();
    }
    public override void AttackInteract()
    {
        Vector3 hitPos = _shootTrs.position + _shootTrs.forward * _distance * _attRange;
        float radius = _thickness * 0.5f; // 레이저의 반지름 (두께의 절반)

        /*
        if (!_isMaxLevel)
        {
            RaycastHit hit;
            if (Physics.SphereCast(_shootTrs.position, radius, _shootTrs.forward, out hit, _distance * _attRange))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    GameObject enemyObject = hit.collider.gameObject;
                    EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
                    enemyHealth.LoseDamage(_damage);
                    hitPos = hit.point;
                }
            }
        }
        else
        {
            RaycastHit[] hits = Physics.SphereCastAll(_shootTrs.position, radius, _shootTrs.forward, _distance * _attRange);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    GameObject enemyObject = hit.collider.gameObject;
                    EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
                    enemyHealth.LoseDamage(_damage);
                }
            }
        }
        */
        RaycastHit[] hits = Physics.SphereCastAll(_shootTrs.position, radius, _shootTrs.forward, _distance * _attRange);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                GameObject enemyObject = hit.collider.gameObject;
                EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
                enemyHealth.LoseDamage(_damage);
            }
        }
        PrintLineRenderer(hitPos);
    }
    public void SetMaxLevel() { _isMaxLevel = true; }
    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override IEnumerator CRT_Attack()
    {
        _canAttack = true;
        while (true)
        {

            yield return new WaitForSeconds(_coolTime);
            _dir = transform.forward;
            for (int i = 0; i < _attCount; i++)
            {
                _shootEffect.Play();
                AttackInteract();
                yield return new WaitForSeconds(_shootDur);
                _lineRenderer.enabled = false;
                yield return new WaitForSeconds(_shootGap);
            }
                

        }

    }

    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attCount = stat._attCount;
        _attRange = stat._attRange;
        _coolTime = stat._coolTime;
        if(_level >= 6)
            _isMaxLevel = true;
    }
    void PrintLineRenderer(Vector3 hitPos)
    {
        _lineRenderer.SetPosition(0, _shootLineTrs.position);
        _lineRenderer.SetPosition(1, hitPos);
        _lineRenderer.enabled = true;
    }
    public override void StartAttack() { return; }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Gizmos의 색상을 설정

        Vector3 start = _shootTrs.position; // 시작 위치
        Vector3 direction = _shootTrs.forward; // 레이저 방향
        float radius = _thickness * 0.5f; // 레이저의 반지름
        float maxDistance = _distance * _attRange; // 최대 거리


        if (!_isMaxLevel)
        {
            // Single SphereCast를 시각화
            RaycastHit hit;
            if (Physics.SphereCast(start, radius, direction, out hit, maxDistance))
            {
                Gizmos.DrawWireSphere(hit.point, radius); // 적과 충돌한 지점에 구를 그리기
                Gizmos.DrawLine(start, hit.point); // 레이저 경로 그리기
            }
            else
            {
                // 충돌이 없을 때 최대 거리까지의 궤적을 그리기
                Gizmos.DrawLine(start, start + direction * maxDistance);
            }
        }
        else
        {
            // 여러 개의 SphereCast를 시각화
            RaycastHit[] hits = Physics.SphereCastAll(start, radius, direction, maxDistance);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    Gizmos.DrawWireSphere(hit.point, radius); // 충돌한 지점마다 구를 그리기
                }
            }

            // 충돌이 없더라도 최대 거리까지의 궤적을 그리기
            Gizmos.DrawLine(start, start + direction * maxDistance);
        }
    }
}
