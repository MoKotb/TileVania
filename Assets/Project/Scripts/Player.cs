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
    float playerClimb = 5f;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private Collider2D collider;
    private float gravityAtStart;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        gravityAtStart = rigidbody.gravityScale;
    }

    void Update()
    {
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
            transform.localScale = new Vector2(Mathf.Sign(Input.GetAxisRaw("Horizontal")),1f
);
        }
    }

    private void Jump()
    {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
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
}
