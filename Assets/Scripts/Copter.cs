using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copter : MonoBehaviour
{
    public GameObject bombPrefab; 
    public float moveSpeed = 5f;

    private bool movingLeft;
    private bool bombDropped = false; 
    private HelicopterSpawner spawner;
    private bool willDropBomb; 

    private Vector3 turretPosition = new Vector3(0f, -2.6f, 0f); 

    public void Initialize(bool fromLeft, HelicopterSpawner spawner)
    {
        movingLeft = !fromLeft;
        this.spawner = spawner;
        willDropBomb = Random.value > 0.5f; 
    }

    void Update()
    {
        
        transform.position += (movingLeft ? Vector3.left : Vector3.right) * moveSpeed * Time.deltaTime;

        
        if (!bombDropped && willDropBomb && IsAboveTurret())
        {
            Debug.Log("💣 Dropping Bomb from Copter!");
            DropBomb();
            bombDropped = true;
        }

        
        if ((movingLeft && transform.position.x < -18) || (!movingLeft && transform.position.x > 18))
        {
            spawner.HelicopterDestroyed(); 
            Destroy(gameObject);
        }
    }

    bool IsAboveTurret()
    {
        
        float distanceToTurret = Mathf.Abs(transform.position.x - turretPosition.x);
        return distanceToTurret < 0.5f; 
    }

    void DropBomb()
    {
        if (bombPrefab != null)
        {
            Vector3 bombPosition = new Vector3(
                transform.position.x,  
                transform.position.y,  
                transform.position.z
            );

            Instantiate(bombPrefab, bombPosition, Quaternion.identity);
        }
    }
}
