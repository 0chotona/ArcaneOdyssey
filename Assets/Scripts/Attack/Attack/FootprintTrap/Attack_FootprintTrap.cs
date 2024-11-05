using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Attack_FootprintTrap : Attack
{
    DamageBox_FootprintTrap _damageBox;
    IObjectPool<GameObject> _damageBoxPool;
    Queue<GameObject> _activeObjectsQueue = new Queue<GameObject>();

    [SerializeField] PlayerHealth _playerHealth;
    [Header("������ �ڽ� ������Ʈ"), SerializeField] GameObject _damageBoxObj;

    [Header("������ ����"), SerializeField] float _damageGap = 0.5f;
    [Header("��ȯ ����"), SerializeField] float _spawnGap = 0.5f;

    [Header("��ȯ y ��ǥ"), SerializeField] float _spawnYPos = 0.5f;
    [Header("�߻� ��ġ"), SerializeField] Transform _shootTrs;

    [Header("�⺻ ũ��"), SerializeField] Vector3 _baseScale;

    public List<Transform> _damagedEnemies = new List<Transform>();

    [Header("Ǯ�� �⺻ ��ȯ ����"), SerializeField] int _defaultCapacity = 1;
    [Header("Ǯ�� �ִ� ��ȯ ����"), SerializeField] int _maxPoolSize = 20;

    private void OnEnable()
    {
        _name = eSKILL.FootprintTrap;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("ss");
        }
    }
    public override void UpdateStat(CStat stat)
    {
        _damage = stat._damage;
        _attRange = stat._attRange;
        _durTime = stat._durTime;
        if (_level >= 6)
        {
            _isMaxLevel = true;
        }
    }

    public override IEnumerator CRT_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_damageGap);
            AttackInteract();
        }
    }

    IEnumerator CRT_SpawnDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnGap);

            // Ǯ���� ������Ʈ�� �����ͼ� ���
            GameObject bullet = _damageBoxPool.Get();
            bullet.transform.position = _shootTrs.position;
            /*
            // ť�� ������Ʈ �߰�
            _activeObjectsQueue.Enqueue(bullet);

            // Ȱ��ȭ�� ������Ʈ�� _maxPoolSize�� �ʰ��ϸ� ���� ������ ������Ʈ�� ��Ȱ��ȭ�Ͽ� Ǯ�� ��ȯ
            if (_activeObjectsQueue.Count > _maxPoolSize)
            {
                GameObject oldestBullet = _activeObjectsQueue.Dequeue();
                _damageBoxPool.Release(oldestBullet);
            }
            */
        }
    }

    public override void AttackInteract()
    {
        _damagedEnemies.RemoveAll(target => target == null);
        if (_damagedEnemies.Count > 0)
        {
            foreach (Transform t in _damagedEnemies)
            {
                float finalDamage = _damage + _damage * _buffStat._Att;
                EnemyHealth health = t.GetComponent<EnemyHealth>();
                health.LoseDamage(finalDamage);
            }
        }
    }

    public override void SetSkill(CSkill skill)
    {
        _skill = skill;
        _level = _skill._level;
        UpdateStat(_skill._stat);
    }

    public override void StartAttack()
    {
        _damageBoxPool = new ObjectPool<GameObject>(CreatePooledDamageBox, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, _defaultCapacity, _maxPoolSize);

        for (int i = 0; i < _defaultCapacity; i++)
        {
            DamageBox_FootprintTrap damageBox = CreatePooledDamageBox().GetComponent<DamageBox_FootprintTrap>();
            damageBox._pool.Release(damageBox.gameObject);
        }

        StartCoroutine(CRT_Attack());
        StartCoroutine(CRT_SpawnDamage());
    }

    GameObject CreatePooledDamageBox()
    {
        GameObject poolObj = Instantiate(_damageBoxObj);
        DamageBox_FootprintTrap damageBox = poolObj.GetComponent<DamageBox_FootprintTrap>();
        damageBox._pool = _damageBoxPool;
        //damageBox.UpdateScale(_baseScale * _attRange);
        //damageBox.SetDamageBox(_durTime);
        //damageBox.SetAttack(this);
        return poolObj;
    }

    private void OnTakeFromPool(GameObject poolGo) 
    {
        poolGo.SetActive(true);
        DamageBox_FootprintTrap damageBox = poolGo.GetComponent<DamageBox_FootprintTrap>();
        //damageBox._pool = _damageBoxPool;
        damageBox.SetDamageBox(_durTime);
        damageBox.UpdateScale(_baseScale * _attRange);
        
        damageBox.SetAttack(this);
    }
    private void OnReturnedToPool(GameObject poolGo) { poolGo.SetActive(false); }
    private void OnDestroyPoolObject(GameObject poolGo) { Destroy(poolGo); }

    public override void UpdateBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }

    public void SetDamageEnemyList(Transform enemy, bool isAdd)
    {
        if (isAdd)
        {
            if (!_damagedEnemies.Contains(enemy))
            {
                _damagedEnemies.Add(enemy);
            }
        }
        else
        {
            if (_damagedEnemies.Contains(enemy))
            {
                _damagedEnemies.Remove(enemy);
            }
        }
    }

    public override void SetPlayerTrs(Transform playerTrs) { }
}
