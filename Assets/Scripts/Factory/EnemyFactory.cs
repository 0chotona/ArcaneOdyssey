using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ~ 1분 30초 몬스터0_0 Cryogonal 프리지오
1분 30초 ~ 중간보스_0 죽을때까지 몬스터0_0 + 몬스터0_1 
2분 중간보스_0  Regice

중간보스_0 Kill하고나서 ~ 1분 몬스터1_0 Bergmite 꽁어름
1분뒤 몬스터1_0 원패턴
1분 ~ 2분 몬스터1_0 + 몬스터1_1 Avalugg 크레베이스
2분 ~ 2분 30초 몬스터1_1
2분30초 중간보스_1 --- 5분30초  Glalie

중간보스_1 Kill하고나서 ~ 1분 몬스터2_0
1분뒤 몬스터2_0 원패턴
1분 ~ 2분 30초 몬스터2_0   Vanillite 바닐프티
2분30초 중간보스_2 --- 9분 Froslass

중간보스_2 Kill하고나서 ~ 1분 몬스터3_0  Vanillish 바닐리치
1분뒤 몬스터3_0 원패턴
1분 ~ 2분 몬스터3_0 + 몬스터3_1   Vanilluxe 배바닐라
2분 ~ 2분 30초 몬스터3_1
2분30초 최종보스--- 12분 30초 Kyurem
*/
public class EnemyFactory : MonoBehaviour
{
    
    


    [Header("소환 쿨타임"), SerializeField] float _spawnCoolTime = 1f;

    [Header("일반 몬스터 팩토리"), SerializeField] MobFactory _normalFactory;
    [Header("보스 몬스터 팩토리"), SerializeField] MobFactory _bossFactory;
    private void Awake()
    {
        
    }
    void SpwanPatern_0()
    {
        StartCoroutine(CRT_Patern_0(_spawnCoolTime, 90f));
    }
    IEnumerator CRT_Patern_0(float gap, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            yield return new WaitForSeconds(gap);
            elapsedTime += gap;
        }

    }
}
