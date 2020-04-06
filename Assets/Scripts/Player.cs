using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ExtendedBehaviour
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
    public float jumpSpeed = 3f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    public float gravity = 1;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;

    void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);

        animator.SetBool("on_ground", onGround);
        if (Input.GetButtonDown("Jump")) 
        {
            jumpTimer = Time.time + jumpDelay;
        }

        // Left direction: -1 | Idle: 0 | Right direction: 1
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate() 
    {
        moveCharacter(direction.x);
        if (jumpTimer > Time.time  && onGround) 
        {
            Jump();
        }

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

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (onGround) 
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections) 
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else 
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0) 
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) 
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
        
    }

    void Flip() 
    {
        movingRight = !movingRight;
        transform.rotation = Quaternion.Euler(0, movingRight ? 0 : 180, 0);
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Goomba goomba = collision.collider.GetComponent<Goomba>();
        if (goomba != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                Debug.Log(point.normal);
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                if (point.normal.y >= 0.6f)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpSpeed;
                    rb.velocity = velocity;
                    goomba.Hurt();
                }
                else
                {
                    Hurt();
                }
            }     
        }
    }

    void Hurt()
    {
        animator.SetBool("is_alive", false);
        Vector2 velocity = rb.velocity;
        velocity.y = jumpSpeed;
        rb.velocity = velocity;
        Destroy(this.GetComponent<CapsuleCollider2D>());
        // Destroy object after some time
        // Wait(1f, () => {
        //     Destroy(this.gameObject);
        // });
    }
}
