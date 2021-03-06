﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Small = 0,
    Fire = 1
}


public class Player : ExtendedBehaviour
{
    [Header("Horizontal Movement")]
    private float moveSpeed = 10f;
    private Vector2 direction;
    private bool movingRight = true;

    [Header("Special Movements")]
    public GameObject Fireball;
    public Transform FireballPosition;
    private bool shootingFireball = false;
    private float initialShootTime, actualShootTime;
    private float totalShootWait = .2f;

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

    [Header("Sound Effects")]
    private AudioSource marioAudioSource;
    public AudioClip JumpAudioClip;
    public AudioClip PowerupAudioClip;
    public AudioClip ShootAudioClip;

    [Header("Particle Systems")]
    public ParticleSystem CheckpointParticleSystem;
    public ParticleSystem Finish1ParticleSystem;
    public ParticleSystem Finish2ParticleSystem;

    [Header("Player information")]
    private PlayerState actualPlayerState;

    void Start() 
    {
        marioAudioSource = this.GetComponentInChildren<AudioSource>();
        if (GameScore.Instance.IsCheckpointActive == true) 
        {
            transform.position = new Vector3(transform.position.x + 81f, transform.position.y, transform.position.z);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 80f, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }

        actualShootTime = 0;
        initialShootTime = 0;

        actualPlayerState = PlayerState.Small;
    }

    void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);
        animator.SetBool("on_ground", onGround);

        if (Input.GetButtonDown("Jump")) 
        {
            jumpTimer = Time.time + jumpDelay;
        }
        if (Input.GetButtonDown("Fire1") && (actualPlayerState == PlayerState.Fire) && !shootingFireball)
        {
            shootingFireball = false;
            actualShootTime = Time.time;

            if (actualShootTime - initialShootTime >= totalShootWait)
            {
                animator.SetTrigger("is_shooting");
                GameObject fireball = Instantiate(Fireball, FireballPosition.position, Quaternion.identity);
                fireball.GetComponent<Fireball>().directionX = movingRight ? 1 : -1;
                // Play shoot sound
                marioAudioSource.clip = ShootAudioClip;
                marioAudioSource.Play();
                initialShootTime = Time.time;
            }
        }

        // Left direction: -1 | Idle: 0 | Right direction: 1
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        moveCharacter(direction.x);

        if (jumpTimer > Time.time && onGround)
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
        marioAudioSource.clip = JumpAudioClip;
        marioAudioSource.Play();
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
                    break;
                }
            }     
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checking trigger using different tags
        if (other.gameObject.CompareTag("Finish"))
        {
            // End level
            GameScore.Instance.IsWinner = true;
            Finish1ParticleSystem.Play();
            Finish2ParticleSystem.Play();
        }
        else if (other.gameObject.CompareTag("Checkpoint")) 
        {
            // Checkpoint reached
            GameScore.Instance.IsCheckpointActive = true;
            CheckpointParticleSystem.Play();
        }
    }


    void Hurt()
    {
        if (actualPlayerState == PlayerState.Small)
        {
            animator.SetBool("is_alive", false);
            Vector2 velocity = rb.velocity;
            velocity.y = jumpSpeed;
            rb.velocity = velocity;
            Destroy(this.GetComponent<CapsuleCollider2D>());
            GameScore.Instance.IsPlaying = false;
        }
        else
        {
            // TODO: Add sound
            UpdateMarioState(PlayerState.Small);
        }
        
    }

    public void UpdateMarioState(PlayerState marioState) {
        // Updating actualPlayerState variable
        actualPlayerState = marioState;
        // Set bool variable to change state
        animator.SetBool("is_changing_state", true);
        // Set state identifier to change animations
        animator.SetInteger("mario_state", (int) marioState);
        if (marioState == PlayerState.Fire)
        {
            // Play powerup sound
            marioAudioSource.clip = PowerupAudioClip;
            marioAudioSource.Play();
        }
        Wait(0.3f, () => {
            // After waiting a little, unset is_changing_state flag
            animator.SetBool("is_changing_state", false);
        });
	}

    public bool MovingRight
    {
        get
        {
            return movingRight;
        }
    }
}
