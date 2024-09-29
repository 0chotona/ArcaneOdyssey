using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Gun : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Gun _damageBox;

    LineRenderer _lineRenderer;
    [Header("�Ѿ� �߻� ��ġ"), SerializeField] Transform _shootTrs;
    [Header("�Ѿ� ����Ʈ �߻� ��ġ"), SerializeField] Transform _shootLineTrs;
    [Header("�߻� ����"), SerializeField] float _shootGap = 0.15f;
    [Header("���� �ð�"), SerializeField] float _shootDur = 0.05f;
    [Header("�߻� ����Ʈ"), SerializeField] ParticleSystem _shootEffect;
    [Header("�Ѿ� �β�"), SerializeField] float _thickness = 0.1f;
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
        float radius = _thickness * 0.5f; // �������� ������ (�β��� ����)

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
        Gizmos.color = Color.red; // Gizmos�� ������ ����

        Vector3 start = _shootTrs.position; // ���� ��ġ
        Vector3 direction = _shootTrs.forward; // ������ ����
        float radius = _thickness * 0.5f; // �������� ������
        float maxDistance = _distance * _attRange; // �ִ� �Ÿ�


        if (!_isMaxLevel)
        {
            // Single SphereCast�� �ð�ȭ
            RaycastHit hit;
            if (Physics.SphereCast(start, radius, direction, out hit, maxDistance))
            {
                Gizmos.DrawWireSphere(hit.point, radius); // ���� �浹�� ������ ���� �׸���
                Gizmos.DrawLine(start, hit.point); // ������ ��� �׸���
            }
            else
            {
                // �浹�� ���� �� �ִ� �Ÿ������� ������ �׸���
                Gizmos.DrawLine(start, start + direction * maxDistance);
            }
        }
        else
        {
            // ���� ���� SphereCast�� �ð�ȭ
            RaycastHit[] hits = Physics.SphereCastAll(start, radius, direction, maxDistance);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    Gizmos.DrawWireSphere(hit.point, radius); // �浹�� �������� ���� �׸���
                }
            }

            // �浹�� ������ �ִ� �Ÿ������� ������ �׸���
            Gizmos.DrawLine(start, start + direction * maxDistance);
        }
    }
}
