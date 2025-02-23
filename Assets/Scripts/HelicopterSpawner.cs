using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    public GameObject helicopterPrefab;
    private bool spawnFromLeft; // Side selection
    private bool isHelicopterActive = false; // Ensures only one helicopter exists

    void Start()
    {
        StartCoroutine(SpawnHelicopter());
    }

    IEnumerator SpawnHelicopter()
    {
        while (true)
        {
            if (!isHelicopterActive) // Only spawn if no helicopter exists
            {
                isHelicopterActive = true; // Mark helicopter as active

                spawnFromLeft = Random.value > 0.5f; // Random side selection
                Vector3 spawnPosition = spawnFromLeft ? new Vector3(-17, 3, 0) : new Vector3(17, 3, 0);

                GameObject helicopter = Instantiate(helicopterPrefab, spawnPosition, Quaternion.identity);

                Copter helicopterScript = helicopter.GetComponent<Copter>();
                if (helicopterScript != null)
                {
                    helicopterScript.Initialize(spawnFromLeft, this);
                }

                // Wait until this helicopter is destroyed before spawning another
                yield return new WaitUntil(() => !isHelicopterActive);
            }

            yield return null; // Small delay to prevent excessive coroutine calls
        }
    }

    public void HelicopterDestroyed()
    {
        isHelicopterActive = false; // Allow the next helicopter to spawn
    }
}
