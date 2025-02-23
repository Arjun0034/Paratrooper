using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    // Rotation settings
    public float rotateSpeed = 2.5f;
    public float minRotationZ = 2f;   
    public float maxRotationZ = 87f;  

    // Firing settings
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.15f;    
    private float nextFireTime = 0f;

    // Rotation state
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

    void Update()
    {
        HandleRotation();
        HandleFiring();
    }

    void HandleRotation()
    {
        float rotationInput = 0f;

        if (isRotatingLeft)
            rotationInput = 1f;
        else if (isRotatingRight)
            rotationInput = -1f;

        if (rotationInput != 0f)
        {
            float rotationAmount = rotationInput * rotateSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);

            // Clamp rotation within specified angles
            float zRotation = transform.eulerAngles.z;
            if (zRotation > 180) zRotation -= 360; // Convert to signed angle
            zRotation = Mathf.Clamp(zRotation, minRotationZ, maxRotationZ);
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }
    }

    void HandleFiring()
    {
        // Firing is handled by the FireBullet method
    }

    public void StartRotatingRight()
    {
        Debug.Log("Rotating Left");
        isRotatingLeft = true;
    }

    public void StopRotatingRight()
    {
        isRotatingLeft = false;
    }

    public void StartRotatingLeft()
    {
        Debug.Log("Rotating Right");
        isRotatingRight = true;
    }

    public void StopRotatingLeft()
    {
        isRotatingRight = false;
    }

    public void FireBullet()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            if (bulletPrefab != null && bulletSpawnPoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float bulletSpeed = 10f;
                    rb.velocity = bulletSpawnPoint.up * bulletSpeed; 
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Troop") && GameManager.Instance.rightSideTroopCount == 4)
        {
            Debug.Log("4th troop landed on turret! Game Over!");
            ScoreManager.Instance.GameOver();
            GameManager.Instance.GameOver();  
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Debug.Log("💥 Bomb hit the turret! Game Over!");
            ScoreManager.Instance.GameOver();
            GameManager.Instance.GameOver();
            Destroy(collision.gameObject);
        }
    }
}
