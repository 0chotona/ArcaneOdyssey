using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class MapSensor : MonoBehaviour
{
    [SerializeField] MapSpawner _mapSpawner;

    [Header("현재 위치"), SerializeField] Vector3 _curPos;
    float _distance = 5f;
    float _curTime = 0f;
    float _maxTime = 3f;
    public bool isCounting = false;
    Coroutine checkCoroutine = null;

    Collider _curEnteredCollider = null;
    List<Collider> collidersInRange = new List<Collider>();  // To track all colliders in range

    [SerializeField] NavMeshSurface _navMeshSurface;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapSensor"))
        {
            collidersInRange.Add(other);  // Add collider to the list

            // Start the coroutine only if it isn't already running
            if (!isCounting)
            {
                checkCoroutine = StartCoroutine(CRT_CheckMapPart());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MapSensor"))
        {
            collidersInRange.Remove(other);  // Remove collider from the list

            // If no more colliders are in range, stop the coroutine
            if (collidersInRange.Count == 0)
            {
                if (checkCoroutine != null)
                {
                    StopCoroutine(checkCoroutine);
                    checkCoroutine = null;
                    isCounting = false;
                }
            }
        }
    }

    IEnumerator CRT_CheckMapPart()
    {
        isCounting = true;

        // Wait for 3 seconds of sustained contact
        yield return new WaitForSeconds(_maxTime);

        // After 3 seconds, check the closest MapSensor in range
        if (collidersInRange.Count > 0)
        {
            Collider closestCollider = GetClosestCollider();
            MapPartsInfo mapParts = closestCollider.GetComponent<MapPartsInfo>();
            _curPos = mapParts._PartPos;
            _mapSpawner.ShiftMap(_curPos);
        }
        _navMeshSurface.RemoveData();
        _navMeshSurface.BuildNavMesh();
        isCounting = false;
    }

    // Function to find the closest collider
    private Collider GetClosestCollider()
    {
        Collider closestCollider = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider col in collidersInRange)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCollider = col;
            }
        }

        return closestCollider;
    }
}