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
        rb.velocity = transform.right * speed; 
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Troop"))
        {
            ScoreManager.Instance.AddScore(1); 
            Destroy(collision.gameObject);  
            Destroy(gameObject); 
        }
        else if (collision.CompareTag("Bomb"))
        {
            ScoreManager.Instance.AddScore(2); 
            Destroy(collision.gameObject);  
            Destroy(gameObject); 
        }
        else if (collision.CompareTag("Copter"))
        {
            ScoreManager.Instance.AddScore(4); 
            Destroy(collision.gameObject);  
            Destroy(gameObject);
        }
    }
}
