using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ~ 1분 30초 몬스터0_0 Cryogonal 프리지오
1분 30초 ~ 3분 몬스터0_0 + 몬스터0_1 
2분 중간보스_0  Regice

3분 ~ 4분 몬스터1_0 Bergmite 꽁어름
4분 몬스터1_0 원패턴
4분 ~ 5분 몬스터1_0 + 몬스터1_1 Avalugg 크레베이스
5분 ~ 6분 30초 몬스터1_1
5분30초 중간보스_1 --- 5분30초  Glalie

6분30초 ~ 7분30초 몬스터2_0
7분 30초 몬스터2_0 원패턴
7분 30초 ~ 10분 몬스터2_0   Vanillite 바닐프티
9분 중간보스_2 --- 9분 Froslass

10분 ~ 11분 몬스터3_0  Vanillish 바닐리치
11분 몬스터3_0 원패턴
11분 ~ 12분 몬스터3_0 + 몬스터3_1   Vanilluxe 배바닐라
12분 ~ 몬스터3_1
12분30초 최종보스--- 12분 30초 Kyurem
*/
public class PaternMaker
{
    
}
public class EnemyFactory : MonoBehaviour
{   
    EnemyData _enemyData = new EnemyData();

    [Header("플레이어 트랜스폼"), SerializeField] Transform _playerTrs;

    [Header("소환 쿨타임"), SerializeField] float _spawnCoolTime = 1f;

    [Header("소환 거리"), SerializeField] float _spawnDist = 20f;
    [Header("소환 각도 간격"), SerializeField] float _angle = 10f;

    [Header("일반 몬스터 팩토리"), SerializeField] MobFactory _normalFactory;
    [Header("보스 몬스터 팩토리"), SerializeField] MobFactory _bossFactory;


    [Header("일반 몹 스폰 위치"), SerializeField] List<Transform> _spawnPoses;
    [Header("중간 보스 스폰 위치"), SerializeField] Transform _normalBossSpawnPos;
    [Header("최종 보스 스폰 위치"), SerializeField] Transform _finalBossSpawnPos;

    [Header("테스트용 시간 간격"), SerializeField] float _timeGap = 30f;
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
    IEnumerator CRT_Patern_0(eNORMALMOB_TYPE type, float gap, float time) //time동안 gap초마다 생성
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
    IEnumerator CRT_Patern_1(eNORMALMOB_TYPE type0, eNORMALMOB_TYPE type1, float gap, float time) //time동안 gap초마다 생성
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
    IEnumerator CRT_Patern_Circle(eNORMALMOB_TYPE type) //원패턴
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
    IEnumerator CRT_BossPatern_0(eMOBTYPE type, float delay) //delay초 후에 소환
    {
        yield return new WaitForSeconds(delay);
        _bossFactory.Spawn(type, _normalBossSpawnPos.position);
    }
    */
}
