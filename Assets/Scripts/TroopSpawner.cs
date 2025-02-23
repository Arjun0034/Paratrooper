using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSpawner : MonoBehaviour
{
    public GameObject troopPrefab; // Assign the troop prefab in Inspector
    public float spawnInterval = 3f; // Time between spawns

    private float minX, maxX; // Screen boundaries
    private Vector3 turretPosition = new Vector3(0f, -2.6f, 0f); // Fixed turret position

    void Start()
    {
        // Get screen width dynamically
        Camera mainCamera = Camera.main;
        float screenHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        minX = -screenHalfWidth + 1f; // Prevent spawning off-screen
        maxX = screenHalfWidth - 1f;

        StartCoroutine(SpawnTroops());
    }

    IEnumerator SpawnTroops()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameManager.Instance.CanSpawnTroops()) // Check if troops can still spawn
            {
                SpawnTroop();
            }
            else
            {
                Debug.Log("🚫 Troop spawning stopped! One side reached the limit.");
                break; // Stop coroutine
            }
        }
    }

    void SpawnTroop()
    {
        if (troopPrefab != null)
        {
            float spawnY = 2f; // Fixed height for troops to fall

            // Choose a random X position between 2 units away from the turret and the screen edges
            float spawnX;
            if (Random.value > 0.5f) // Spawn on the left side
            {
                spawnX = Random.Range(minX, turretPosition.x - 2f);
            }
            else // Spawn on the right side
            {
                spawnX = Random.Range(turretPosition.x + 2f, maxX);
            }

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
            Instantiate(troopPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
