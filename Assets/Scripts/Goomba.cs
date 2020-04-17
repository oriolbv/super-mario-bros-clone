using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : ExtendedBehaviour
{

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    public float Speed;
    public bool MoveRight;

    private bool stopMovement = false;

    [Header("Sound Effects")]
    private AudioSource goombaDieAudioSource;

    void Update()
    {
        if (!stopMovement)
        {
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        // Destroy object after some time
        Wait(0.6f, () => {
            Destroy(this.gameObject);
        });
    }

}
