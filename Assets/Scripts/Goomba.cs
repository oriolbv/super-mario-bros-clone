using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goomba : ExtendedBehaviour
{

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Movement")]
    public float Speed;
    public bool MoveRight;
    public GameObject Player;
    private bool stopMovement = false;
    private float minDistance = 1;
    private float maxDistance = 5;

    [Header("Sound Effects")]
    private AudioSource goombaDieAudioSource;


    void Update()
    {
        // Check player distance
        float position = this.transform.position.x;
        float playerPosition = Player.transform.position.x;
        float distance = Math.Abs(position) - Math.Abs(playerPosition);

        if (!stopMovement)
        {
            if ((distance >= minDistance) && (distance <= maxDistance))
            {
                // Mario is in front of the goomba
                transform.Translate(-2 * Time.deltaTime * Speed, 0, 0);
                MoveRight = true;
            }
            else if ((distance <= -minDistance) && (distance >= -maxDistance))
            {
                // Mario is behind the goomba
                transform.Translate(2 * Time.deltaTime * Speed, 0, 0);
                MoveRight = false;
            }
            else
            {
                // Normal movement
                if (MoveRight)
                {
                    transform.Translate(2 * Time.deltaTime * Speed, 0, 0);
                }
                else
                {
                    transform.Translate(-2 * Time.deltaTime * Speed, 0, 0);
                }
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If goomba collides with a wall or another goomba, change movement direction
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            MoveRight = !MoveRight;
        }
    }

    public void Hurt()
    {
        // Object should stop moving
        stopMovement = true;
        // Animation transition to 'goomba_dead'
        animator.SetBool("is_hurt", true);
        // Reproduce sound
        goombaDieAudioSource = this.GetComponentInChildren<AudioSource>();
        goombaDieAudioSource.Play();
        // Increase player points
        GameScore.Instance.Score += 100;
        // Destroy object after some time
        Wait(0.6f, () => {
            Destroy(this.gameObject);
        });
    }

}
