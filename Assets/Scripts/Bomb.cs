using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Turret"))
        {
            Debug.Log("Bomb hit the turret! Game Over!");
            ScoreManager.Instance.GameOver();
            GameManager.Instance.GameOver();
            Destroy(gameObject); // ✅ Destroy bomb on impact
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Bomb hit the ground!");
            Destroy(gameObject); // ✅ Bomb disappears when hitting ground
        }
    }
}
