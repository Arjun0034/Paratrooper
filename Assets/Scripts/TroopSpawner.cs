using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSpawner : MonoBehaviour
{
    public GameObject troopPrefab; 
    public float spawnInterval = 3f; 

    private float minX, maxX; 
    private Vector3 turretPosition = new Vector3(0f, -2.6f, 0f); 

    void Start()
    {
        
        Camera mainCamera = Camera.main;
        float screenHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        minX = -screenHalfWidth + 1f; 
        maxX = screenHalfWidth - 1f;

        StartCoroutine(SpawnTroops());
    }

    IEnumerator SpawnTroops()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameManager.Instance.CanSpawnTroops()) 
            {
                SpawnTroop();
            }
            else
            {
                Debug.Log("🚫 Troop spawning stopped! One side reached the limit.");
                break; 
            }
        }
    }

    void SpawnTroop()
    {
        if (troopPrefab != null)
        {
            float spawnY = 2f; 

            
            float spawnX;
            if (Random.value > 0.5f) 
            {
                spawnX = Random.Range(minX, turretPosition.x - 2f);
            }
            else 
            {
                spawnX = Random.Range(turretPosition.x + 2f, maxX);
            }

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            Instantiate(troopPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
