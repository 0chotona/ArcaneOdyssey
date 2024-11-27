using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ~ 1�� 30�� ����0_0 Cryogonal ��������
1�� 30�� ~ 3�� ����0_0 + ����0_1 
2�� �߰�����_0  Regice

3�� ~ 4�� ����1_0 Bergmite �Ǿ
4�� ����1_0 ������
4�� ~ 5�� ����1_0 + ����1_1 Avalugg ũ�����̽�
5�� ~ 6�� 30�� ����1_1
5��30�� �߰�����_1 --- 5��30��  Glalie

6��30�� ~ 7��30�� ����2_0
7�� 30�� ����2_0 ������
7�� 30�� ~ 10�� ����2_0   Vanillite �ٴ���Ƽ
9�� �߰�����_2 --- 9�� Froslass

10�� ~ 11�� ����3_0  Vanillish �ٴҸ�ġ
11�� ����3_0 ������
11�� ~ 12�� ����3_0 + ����3_1   Vanilluxe ��ٴҶ�
12�� ~ ����3_1
12��30�� ��������--- 12�� 30�� Kyurem
*/
public class PaternMaker
{
    
}
public class EnemyFactory : MonoBehaviour
{   
    EnemyData _enemyData = new EnemyData();

    [Header("�÷��̾� Ʈ������"), SerializeField] Transform _playerTrs;

    [Header("��ȯ ��Ÿ��"), SerializeField] float _spawnCoolTime = 1f;

    [Header("��ȯ �Ÿ�"), SerializeField] float _spawnDist = 20f;
    [Header("��ȯ ���� ����"), SerializeField] float _angle = 10f;

    [Header("�Ϲ� ���� ���丮"), SerializeField] MobFactory _normalFactory;
    [Header("���� ���� ���丮"), SerializeField] MobFactory _bossFactory;


    [Header("�Ϲ� �� ���� ��ġ"), SerializeField] List<Transform> _spawnPoses;
    [Header("�߰� ���� ���� ��ġ"), SerializeField] Transform _normalBossSpawnPos;
    [Header("���� ���� ���� ��ġ"), SerializeField] Transform _finalBossSpawnPos;

    [Header("�׽�Ʈ�� �ð� ����"), SerializeField] float _timeGap = 30f;
    private void Start()
    {
        //StartCoroutine(CRT_Test());
        //StartTestPatern();
        SetData();
    }
    public void SetData()
    {
        _normalFactory.SetData(DataHandler.Instance._EnemyDic);
        _normalFactory.SetTarget(_playerTrs);

        _bossFactory.SetData(DataHandler.Instance._EnemyDic);
        _bossFactory.SetTarget(_playerTrs);
    }/*
    IEnumerator SpawnPatern_0()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Cryogonal, 1f, 120f));
        SpawnBoss(eNORMALMOB_TYPE.Regice);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Cryogonal, 1f, 60f));

        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Bergmite, 1f, 60f));
        yield return StartCoroutine(CRT_Patern_Circle(eNORMALMOB_TYPE.Bergmite));
        yield return StartCoroutine(CRT_Patern_1(eNORMALMOB_TYPE.Bergmite, eNORMALMOB_TYPE.Avalugg, 1f, 60f));
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Avalugg, 1f, 30f));
        SpawnBoss(eNORMALMOB_TYPE.Glalie);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Avalugg, 1f, 60f));


        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillite, 1f, 60f));
        yield return StartCoroutine(CRT_Patern_Circle(eNORMALMOB_TYPE.Vanillite));
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillite, 1f, 90f));
        SpawnBoss(eNORMALMOB_TYPE.Froslass);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillite, 1f, 60f));

        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillish, 1f, 60f));
        yield return StartCoroutine(CRT_Patern_Circle(eNORMALMOB_TYPE.Vanillish));
        yield return StartCoroutine(CRT_Patern_1(eNORMALMOB_TYPE.Vanillish, eNORMALMOB_TYPE.Vanilluxe, 1f, 60f));
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanilluxe, 1f, 30f));
        SpawnBoss(eNORMALMOB_TYPE.Kyurem);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanilluxe, 1f, 1000f));
    }*/
    public void StartTestPatern(float delay)
    {
        StartCoroutine(CRT_TestPatern(delay));
    }
    IEnumerator CRT_TestPatern(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Cryogonal, _spawnCoolTime, _timeGap * 4));
        SpawnBoss(eNORMALMOB_TYPE.Regice);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Cryogonal, _spawnCoolTime, _timeGap * 2));

        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Bergmite, _spawnCoolTime, _timeGap * 2));
        yield return StartCoroutine(CRT_Patern_Circle(eNORMALMOB_TYPE.Bergmite));
        yield return StartCoroutine(CRT_Patern_1(eNORMALMOB_TYPE.Bergmite, eNORMALMOB_TYPE.Avalugg, _spawnCoolTime, _timeGap * 2));
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Avalugg, _spawnCoolTime, _timeGap));
        SpawnBoss(eNORMALMOB_TYPE.Glalie);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Avalugg, _spawnCoolTime, _timeGap * 2));


        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillite, _spawnCoolTime, _timeGap * 2));
        yield return StartCoroutine(CRT_Patern_Circle(eNORMALMOB_TYPE.Vanillite));
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillite, _spawnCoolTime, _timeGap * 3));
        SpawnBoss(eNORMALMOB_TYPE.Froslass);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillite, _spawnCoolTime, _timeGap * 2));

        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanillish, _spawnCoolTime, _timeGap * 2));
        yield return StartCoroutine(CRT_Patern_Circle(eNORMALMOB_TYPE.Vanillish));
        yield return StartCoroutine(CRT_Patern_1(eNORMALMOB_TYPE.Vanillish, eNORMALMOB_TYPE.Vanilluxe, _spawnCoolTime, _timeGap * 2));
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanilluxe, _spawnCoolTime, _timeGap));
        SpawnBoss(eNORMALMOB_TYPE.Kyurem);
        yield return StartCoroutine(CRT_Patern_0(eNORMALMOB_TYPE.Vanilluxe, _spawnCoolTime, 1000f));
    }
    IEnumerator CRT_Patern_0(eNORMALMOB_TYPE type, float gap, float time) //time���� gap�ʸ��� ����
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            Vector3 rndPos = GetRandomSpawnPos();
            _normalFactory.Spawn(type, rndPos);
            yield return new WaitForSeconds(gap);
            elapsedTime += gap;
        }

    }
    IEnumerator CRT_Patern_1(eNORMALMOB_TYPE type0, eNORMALMOB_TYPE type1, float gap, float time) //time���� gap�ʸ��� ����
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            Vector3 rndPos = GetRandomSpawnPos();
            _normalFactory.Spawn(type0, rndPos);

            rndPos = GetRandomSpawnPos();
            _normalFactory.Spawn(type1, rndPos);
            yield return new WaitForSeconds(gap);
            elapsedTime += gap;
        }

    }
    IEnumerator CRT_Patern_Circle(eNORMALMOB_TYPE type) //������
    {

        for (int i = 0; i < 360 / _angle; i++)
        {


            Vector3 direction = new Vector3(Mathf.Cos(_angle * i * Mathf.Deg2Rad), 0, Mathf.Sin(_angle * i * Mathf.Deg2Rad));

            
            Vector3 startPos = _playerTrs.position;
            Ray ray = new Ray(startPos, direction);

            RaycastHit hit;
            Vector3 spawnPos;

            if (Physics.Raycast(ray, out hit, _spawnDist, LayerMask.GetMask("Wall")))
            {
                spawnPos = hit.point;
            }
            else
            {
                spawnPos = startPos + direction * _spawnDist;
            }
            _normalFactory.Spawn(type, spawnPos);
        }

        yield return new WaitForSeconds(1f);
    }
    
    void SpawnBoss(eNORMALMOB_TYPE type)
    {
        _bossFactory.Spawn(type, _normalBossSpawnPos.position);

    }
    Vector3 GetRandomSpawnPos()
    {

        int rnd = Random.Range(0, _spawnPoses.Count);
        return _spawnPoses[rnd].position;
    }
    /*
    IEnumerator CRT_BossPatern_0(eMOBTYPE type, float delay) //delay�� �Ŀ� ��ȯ
    {
        yield return new WaitForSeconds(delay);
        _bossFactory.Spawn(type, _normalBossSpawnPos.position);
    }
    */
}
