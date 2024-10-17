using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    List<Transform> _nearEnemyList = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _nearEnemyList.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _nearEnemyList.Remove(other.transform);
        }
    }
    public Transform GetNearestEnemy()
    {
        if (_nearEnemyList == null || _nearEnemyList.Count == 0)
            return null;
        RemoveDestroyEnemy();
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity; 
        Vector3 currentPosition = transform.position; 

        foreach (Transform enemy in _nearEnemyList)
        {
            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.position); // 적과의 거리 계산

            if (distanceToEnemy < minDistance) // 더 가까운 적을 찾으면 갱신
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy; // 가장 가까운 적 반환
    }
    public void RemoveDestroyEnemy()
    {
        _nearEnemyList.RemoveAll(target => target == null);
    }
}
