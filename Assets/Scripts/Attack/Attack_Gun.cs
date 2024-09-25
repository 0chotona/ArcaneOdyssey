using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Gun : Attack
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Gun _damageBox;

    LineRenderer _lineRenderer;
    [Header("총알 발사 위치"), SerializeField] Transform _shootTrs;
    [Header("발사 간격"), SerializeField] float _shootGap = 0.15f;
    [Header("유지 시간"), SerializeField] float _shootDur = 0.05f;
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
    }
    public override void AttackInteract()
    {
        Vector3 hitPos = _shootTrs.position + _shootTrs.forward * _distance * _attRange;
        if (!_isMaxLevel)
        {
            RaycastHit hit;
            if (Physics.Raycast(_shootTrs.position, _shootTrs.forward, out hit, _distance * _attRange))
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
            RaycastHit[] hits = Physics.RaycastAll(_shootTrs.position, _shootTrs.forward, _distance * _attRange);

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
        _lineRenderer.SetPosition(0, _shootTrs.position);
        _lineRenderer.SetPosition(1, hitPos);
        _lineRenderer.enabled = true;
    }
    public override void StartAttack() { return; }
}
