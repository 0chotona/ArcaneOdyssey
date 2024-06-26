using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/*
 * 1. ����Ƚ�� (0. �ѹ��� 1. �ι��� 2. ������)
2. ���ݹ��� (0. 1�� 1. 2�� 2. 3�� + ����)
3. ��Ÿ�� (0. 5�� 1. 4�� 2. 3��)
*/
public class Attack_Meteor : AttackController
{
    [SerializeField] GameObject _damageBoxObj;
    DamageBox_Meteor _damageBox;

    Skill _skill;
    private void Start()
    {
        
        StartCoroutine(CRT_Attack());

        _name = "� �浹";
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _skillManager.UpgradeLevel("� �浹");
            UpdateStat();
        }
        UpdateStat();
        */
    }
    public override void Attack()
    {
        for (int i = 0; i < _attCount; i++)
        {
            
            GameObject damageBox = Instantiate(_damageBoxObj, transform.position + GetRandomPos(), Quaternion.identity);

            _damageBox = damageBox.GetComponent<DamageBox_Meteor>();

            _damageBox.UpdateScale(_attRange);
            _damageBox.UpdateDamage(_damage);
        }



    }
    Vector3 GetRandomPos()
    {
        int rndDist = Random.Range(0, 10);
        int rndAngle = Random.Range(0, 360);

        Vector3 rndPos = new Vector3(Mathf.Cos(rndAngle * Mathf.Deg2Rad) * rndDist, 15, Mathf.Sin(rndAngle * Mathf.Deg2Rad) * rndDist);
        return rndPos;
    }
    public override void UpdateStat(Skill skill)
    {
        _level = skill._level;
        switch (_level)
        {
            case 1:
                _damage = 9;
                _attCount = 1;
                _attRange = 0.8f;
                _coolTime = 7f;
                break;
            case 2:
                _damage = 10;
                _attCount = 1;
                _attRange = 0.8f;
                _coolTime = 7f;
                break;
            case 3:
                _damage = 11;
                _attCount = 1;
                _attRange = 1f;
                _coolTime = 7f;
                break;
            case 4:
                _damage = 11;
                _attCount = 1;
                _attRange = 1f;
                _coolTime = 6f;
                break;
            case 5:
                _damage = 13;
                _attCount = 1;
                _attRange = 1.2f;
                _coolTime = 6f;
                break;
            case 6:
                _damage = 15;
                _attCount = 1;
                _attRange = 1.2f;
                _coolTime = 4f;
                break;
        }
        skill.UpdateStat(_level, _damage, _attCount, _attRange, _coolTime, _durTime, _shotSpeed);
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolTime);
            Attack();
        }
    }

    public override void SetSkill(Skill skill)
    {
        _skill = skill;
    }
}
