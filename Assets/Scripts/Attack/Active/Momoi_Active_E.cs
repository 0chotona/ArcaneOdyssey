using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momoi_Active_E : MonoBehaviour, IActiveAttackable
{
    
    [Header("총알 발사 위치"), SerializeField] Transform _shootTrs;

    [Header("각도"), SerializeField] float _angle = 15f;
    [Header("거리"), SerializeField] float _distance = 20f;

    [Header("공격 횟수"), SerializeField] int _attCount = 4;
    [Header("공격 간격"), SerializeField] float _timeGap = 0.2f;

    [Header("데미지"), SerializeField] float _damage = 10f;

    [Header("로켓 프리펩"), SerializeField] GameObject _roketPref;
    [Header("총알 속도"), SerializeField] float _speed = 10f;

    [Header("쿨타임"), SerializeField] float _coolTime = 10f;

    bool _canActive = true;
    CBuffStat _buffStat = new CBuffStat();
    public void ActiveInteract()
    {
        if(_canActive)
        {
            StartCoroutine(CRT_ShootRocket());
            StartCoroutine(CRT_CoolTime());
            UIManager.Instance.StartECooltime(_coolTime - _coolTime * BuffStat.Instance._CoolTimeBuff);
        }
        
    }

    public void SetPlayerTrs(Transform playerTrs) { }
    

    IEnumerator CRT_ShootRocket()
    {
        for(int i = 0; i < _attCount; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // 각 구역의 시작 각도와 끝 각도를 계산
                float startAngle = -1.5f * _angle + j * _angle;
                float endAngle = startAngle + _angle;

                // 구역 내에서 랜덤 각도를 선택
                float randomAngle = Random.Range(startAngle, endAngle);

                // 랜덤 각도 방향을 Vector3로 변환 (플레이어 앞방향을 기준으로 회전)
                Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * _shootTrs.forward;


                Vector3 targetPos = _shootTrs.position + randomDirection * _distance;
                SpawnRocket(targetPos);

                SoundManager.Instance.PlaySound(eCHARSOUNDTYPE.Momoi_E);
                // 원하는 행동을 수행 (예: Gizmos로 그리기, 총알 발사 등)
                //Gizmos.DrawLine(_shootTrs.position, _shootTrs.position + randomDirection * _distance);
                yield return new WaitForSeconds(_timeGap);
            }

        }
        
    }
    void SpawnRocket(Vector3 targetPos)
    {
        

        GameObject rocket = Instantiate(_roketPref, _shootTrs.position, _shootTrs.rotation);
        Momoi_DamageBox_E damageBox = rocket.GetComponent<Momoi_DamageBox_E>();

        float finalDamage = _damage + (_damage * BuffStat.Instance._AttBuff);
        damageBox.UpdateDamage(finalDamage);
        damageBox.UpdateSpeed(_speed);
        damageBox.Shot(targetPos);
    }
    IEnumerator CRT_CoolTime()
    {
        _canActive = false;
        yield return new WaitForSeconds(_coolTime - _coolTime * _buffStat._CoolTime);
        _canActive = true;
    }

    public void SetBuffStat(CBuffStat buffStat)
    {
        _buffStat = buffStat;
    }
}
