using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Horizontal Movement")]
    private float moveSpeed = 10f;
    private Vector2 direction;
    private bool movingRight = true;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;

    [Header("Collision")]
    public bool onGround = false;

    void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);

        // Left direction: -1 | Idle: 0 | Right direction: 1
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate() 
    {
        moveCharacter(direction.x);
        modifyPhysics();
    }

    void moveCharacter(float horizontal) 
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));

        if ((horizontal > 0 && !movingRight) || (horizontal < 0 && movingRight)) 
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed) 
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (Mathf.Abs(direction.x) < 0.4f || changingDirections) 
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    void Flip() 
    {
        movingRight = !movingRight;
        transform.rotation = Quaternion.Euler(0, movingRight ? 0 : 180, 0);
    }
}
