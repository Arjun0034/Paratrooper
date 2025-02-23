using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // Move using physics
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Troop"))
        {
            ScoreManager.Instance.AddScore(1); //+1 for hitting a troop
            Destroy(collision.gameObject);  //Destroy the troop
            Destroy(gameObject); //Destroy bullet
        }
        else if (collision.CompareTag("Bomb"))
        {
            ScoreManager.Instance.AddScore(2); //+2 for hitting a bomb
            Destroy(collision.gameObject);  //Destroy the bomb
            Destroy(gameObject); //Destroy bullet
        }
        else if (collision.CompareTag("Copter"))
        {
            ScoreManager.Instance.AddScore(4); //+4 for hitting a copter
            Destroy(collision.gameObject);  //Destroy the copter
            Destroy(gameObject); //Destroy bullet
        }
    }
}
