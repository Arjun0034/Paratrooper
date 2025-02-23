﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troops : MonoBehaviour
{
    private bool hasLanded = false;
    private Rigidbody2D rb;
    private Collider2D coll;
    private bool isMoving = false;

    [SerializeField] private float movementSpeed = 0.5f;  // ⬅️ Reduced speed for smooth movement
    [SerializeField] private float jumpHeight = 5f; // Height of the jump

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevent rotation issues
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLanded)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("LeftGround"))
            {
                GameManager.Instance.AddTroop(this, true);
                hasLanded = true;
                PlayIdleAnimation();
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("RightGround"))
            {
                GameManager.Instance.AddTroop(this, false);
                hasLanded = true;
                PlayIdleAnimation();
            }
        }
    }

    public IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        if (isMoving) yield break;
        isMoving = true;

        PlayRunAnimation();

        yield return StartCoroutine(SlideToPosition(targetPosition));

        PlayIdleAnimation();
        StopMovement();
    }

    public IEnumerator JumpOnTroop(Troops belowTroop)
    {
        if (isMoving) yield break;
        isMoving = true;

        PlayJumpAnimation();

        // Jump up
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f); // Wait for the jump to complete

        // Move to the position above the troop below
        Vector3 targetPosition = new Vector3(belowTroop.transform.position.x, belowTroop.transform.position.y + 0.5f, 0f);
        yield return StartCoroutine(SlideToPosition(targetPosition));

        PlayIdleAnimation();
        StopMovement();
    }

    private IEnumerator SlideToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void StopMovement()
    {
        isMoving = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Prevent unwanted physics movement
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // Stop all movement
        Debug.Log("🚫 Troop has completely stopped!");
    }

    // Troop constraints and colliders
    public void FreezeTroop()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        coll.enabled = false; // Disable collider
    }

    public void UnfreezeTroop()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        coll.enabled = true; // Enable collider
    }

    public void FixPosition()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    private void PlayRunAnimation() { Debug.Log("🏃 Running!"); }
    private void PlayJumpAnimation() { Debug.Log("🦘 Jumping!"); }
    private void PlayIdleAnimation() { Debug.Log("😐 Idle!"); }
}