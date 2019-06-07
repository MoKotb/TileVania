using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 5f;
    [SerializeField]
    float playerJump = 5f;
    [SerializeField]
    float playerClimb = 3f;
    [SerializeField]
    Vector2 deathKick = new Vector2(0f,25f);
  
    private Rigidbody2D rigidbody;
    private Animator animator;
    private BoxCollider2D myFeetCollider;
    private float gravityAtStart;
    private bool isAlive = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = rigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        ClimbLadder();
        FlipSprite();
        Jump();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxisRaw("Horizontal");
        float speed = playerSpeed * controlThrow;
        Vector2 playerVelocity = new Vector2(speed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;
        bool playerHasHorizontalVelocity = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Epsilon;
        animator.SetBool("Run", playerHasHorizontalVelocity);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Epsilon;
        if(playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(Input.GetAxisRaw("Horizontal")),1f);
        }
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f,playerJump);
            rigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animator.SetBool("Climb", false);
            rigidbody.gravityScale = gravityAtStart;
            return;
        }
        rigidbody.gravityScale = 0.0f;
        float controlThrow = Input.GetAxisRaw("Vertical");
        float speed = controlThrow * playerClimb;
        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, speed);
        rigidbody.velocity = climbVelocity;
        bool playerHasVerticalVelocity = Mathf.Abs(Input.GetAxisRaw("Vertical")) > Mathf.Epsilon;
        animator.SetBool("Climb", playerHasVerticalVelocity);
    }

    private void Die()
    {
        isAlive = false;
        animator.SetTrigger("Die");
        rigidbody.velocity = deathKick;
        FindObjectOfType<GameSession>().PlayerDeath();
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        if (otherCollision.gameObject.tag == "Enemy" && isAlive)
        {
            Die();
        }
    }
}
