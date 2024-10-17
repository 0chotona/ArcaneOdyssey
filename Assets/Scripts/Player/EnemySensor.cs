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
            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.position); // ������ �Ÿ� ���

            if (distanceToEnemy < minDistance) // �� ����� ���� ã���� ����
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy; // ���� ����� �� ��ȯ
    }
    public void RemoveDestroyEnemy()
    {
        _nearEnemyList.RemoveAll(target => target == null);
    }
}
