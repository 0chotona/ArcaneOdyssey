using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * ~ 1�� 30�� ����0_0 Cryogonal ��������
1�� 30�� ~ �߰�����_0 ���������� ����0_0 + ����0_1 
2�� �߰�����_0  Regice

�߰�����_0 Kill�ϰ��� ~ 1�� ����1_0 Bergmite �Ǿ
1�е� ����1_0 ������
1�� ~ 2�� ����1_0 + ����1_1 Avalugg ũ�����̽�
2�� ~ 2�� 30�� ����1_1
2��30�� �߰�����_1 --- 5��30��  Glalie

�߰�����_1 Kill�ϰ��� ~ 1�� ����2_0
1�е� ����2_0 ������
1�� ~ 2�� 30�� ����2_0   Vanillite �ٴ���Ƽ
2��30�� �߰�����_2 --- 9�� Froslass

�߰�����_2 Kill�ϰ��� ~ 1�� ����3_0  Vanillish �ٴҸ�ġ
1�е� ����3_0 ������
1�� ~ 2�� ����3_0 + ����3_1   Vanilluxe ��ٴҶ�
2�� ~ 2�� 30�� ����3_1
2��30�� ��������--- 12�� 30�� Kyurem
*/
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData _enemyData;

    [SerializeField] int _stage;

    [SerializeField] GameObject[] _enemyObj;
    [SerializeField] GameObject[] _bossObj;


    Vector3 _spawnAreaSize = new Vector3(40, 0, 40);
    public Transform _playerTrs;

    int _spawnNum;

    [Header("��ȯ ��Ÿ��"),SerializeField]float _spawnCoolTime = 0.5f;
    [Header("������ ������"), SerializeField] ItemSpawner _itemSpawner;
    float _spawnDist = 20;



    [Header("���̺� ��"), SerializeField] float _waveTerm = 60f;
    [Header("������ ��"), SerializeField] float _levelTerm = 6f;
    [Header("������ ���̺� ��"), SerializeField] float _finalWaveTerm = 20f;

    public int _curWave = 0;
    int _curLevel = 0;
    int _curEnemy = 0;

    private void Start()
    {
        StartCoroutine(CRT_Spawn());

        StartCoroutine(CRT_UpgradeEnemyStat());
        StartCoroutine(CRT_UpgradeWave());
    }
    public void SetPlayerTrs(Transform playerTrs)
    {
        _playerTrs = playerTrs;
    }

    IEnumerator CRT_Spawn()
    {
        while (true)
        {
            int rndAngle = Random.Range(0, 360);
            Vector3 rndPos = new Vector3(Mathf.Cos(rndAngle * Mathf.Deg2Rad) * _spawnDist, 0, Mathf.Sin(rndAngle * Mathf.Deg2Rad) * _spawnDist);
            GameObject enemyObj = Instantiate(_enemyObj[_curEnemy], _playerTrs.transform.position + rndPos, Quaternion.identity);

            SetEnemyStat(enemyObj);

            yield return new WaitForSeconds(_spawnCoolTime);
        }

    }
    IEnumerator CRT_UpgradeEnemyStat()
    {
        while(true)
        {
            yield return new WaitForSeconds(_levelTerm);
            _curLevel++;
        }
        
    }
    IEnumerator CRT_UpgradeWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(_waveTerm);
            SpawnCircleWave();
            if (_curEnemy >= _enemyObj.Length - 1)
            {
                _curEnemy = 0;
                StartCoroutine(CRT_FinalWave());
                break;
            }
            
            _curEnemy++;
            _curLevel = 0;
        }
    }
    IEnumerator CRT_FinalWave()
    {
        _curLevel = (int)(_waveTerm / _levelTerm);
        while (true)
        {
            
            yield return new WaitForSeconds(_finalWaveTerm);
            if (_curEnemy >= _enemyObj.Length - 1)
            {
                UIManager.Instance.StartCoroutine(UIManager.Instance.CRT_BossCountDown());
                yield return new WaitForSeconds(10f);
                SpawnBoss();
                break;
            }
            _curEnemy++;
        }
        
    }
    IEnumerator CRT_BossWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnCoolTime);
        }
        
    }
    void SpawnBoss()
    {
        CRT_BossWave();
        Debug.Log("���� ����");
    }
    void SpawnCircleWave()
    {
        int offset = 10;

        for(int i = 0; i < 360 / offset; i++)
        {
            Vector3 spawnPos = new Vector3(Mathf.Cos(offset * i * Mathf.Deg2Rad) * _spawnDist, 0, Mathf.Sin(offset * i * Mathf.Deg2Rad) * _spawnDist);
            GameObject enemyObj = Instantiate(_enemyObj[_curEnemy], _playerTrs.transform.position + spawnPos, Quaternion.identity);
            SetEnemyStat(enemyObj);
        }
    }
    void SetEnemyStat(GameObject obj)
    {
        EnemyInfo enemyInfo = obj.GetComponent<EnemyInfo>();
        CEnemy cEnemy = _enemyData._EnemyDatas[enemyInfo._EnemyType];
        //enemyInfo.SetStat(cEnemy._Hp, cEnemy._Att + _curLevel, cEnemy._Def + _curLevel, cEnemy._MoveSpeed);

        //obj.GetComponent<EnemyHealth>().SetSpawner(transform, _itemSpawner);
        obj.GetComponent<EnemyMove>().SetPlayerTrs(_playerTrs);

    }
}
