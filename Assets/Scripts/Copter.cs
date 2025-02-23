using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copter : MonoBehaviour
{
    public GameObject bombPrefab; // Assign the bomb prefab in Inspector
    public float moveSpeed = 5f;

    private bool movingLeft;
    private bool bombDropped = false; // Ensure only one bomb is dropped
    private HelicopterSpawner spawner;
    private bool willDropBomb; // Randomly decide if this copter will drop a bomb

    private Vector3 turretPosition = new Vector3(0f, -2.6f, 0f); // Fixed turret position

    public void Initialize(bool fromLeft, HelicopterSpawner spawner)
    {
        movingLeft = !fromLeft;
        this.spawner = spawner;
        willDropBomb = Random.value > 0.5f; // 50% chance to drop a bomb
    }

    void Update()
    {
        // Move the helicopter
        transform.position += (movingLeft ? Vector3.left : Vector3.right) * moveSpeed * Time.deltaTime;

        // Drop bomb if this copter is chosen to drop and is above the turret
        if (!bombDropped && willDropBomb && IsAboveTurret())
        {
            Debug.Log("💣 Dropping Bomb from Copter!");
            DropBomb();
            bombDropped = true;
        }

        // Destroy the helicopter when off-screen
        if ((movingLeft && transform.position.x < -18) || (!movingLeft && transform.position.x > 18))
        {
            spawner.HelicopterDestroyed(); // Notify spawner BEFORE destruction
            Destroy(gameObject);
        }
    }

    bool IsAboveTurret()
    {
        // Drop bomb only when directly above turret's X position
        float distanceToTurret = Mathf.Abs(transform.position.x - turretPosition.x);
        return distanceToTurret < 0.5f; // Adjust this value if needed
    }

    void DropBomb()
    {
        if (bombPrefab != null)
        {
            Vector3 bombPosition = new Vector3(
                transform.position.x,  // Drop bomb from the helicopter's current X position
                transform.position.y,  // Drop bomb from the helicopter's current Y position
                transform.position.z
            );

            Instantiate(bombPrefab, bombPosition, Quaternion.identity);
        }
    }
}
