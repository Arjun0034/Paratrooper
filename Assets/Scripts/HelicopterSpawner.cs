using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    public GameObject helicopterPrefab;
    private bool spawnFromLeft;
    private bool isHelicopterActive = false;

    void Start()
    {
        StartCoroutine(SpawnHelicopter());
    }

    IEnumerator SpawnHelicopter()
    {
        while (true)
        {
            if (!isHelicopterActive) 
            {
                isHelicopterActive = true; 

                spawnFromLeft = Random.value > 0.5f;
                Vector3 spawnPosition = spawnFromLeft ? new Vector3(-17, 3, 0) : new Vector3(17, 3, 0);

                GameObject helicopter = Instantiate(helicopterPrefab, spawnPosition, Quaternion.identity);

                Copter helicopterScript = helicopter.GetComponent<Copter>();
                if (helicopterScript != null)
                {
                    helicopterScript.Initialize(spawnFromLeft, this);
                }

                
                yield return new WaitUntil(() => !isHelicopterActive);
            }

            yield return null; 
        }
    }

    public void HelicopterDestroyed()
    {
        isHelicopterActive = false; 
    }
}
