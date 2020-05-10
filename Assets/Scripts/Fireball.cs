using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float directionX; // > 0 for right, < 0 for left
    private float explosionDuration = .75f;
    private Vector2 absVelocity = new Vector2(11, 11);

    private Rigidbody2D rigidbody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // initial velocity
        rigidbody.velocity = new Vector2(directionX * absVelocity.x, -absVelocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(directionX * absVelocity.x, rigidbody.velocity.y);
    }

    void Explode()
    {
        Destroy(gameObject, explosionDuration);
    }

    void ExplodeImmediately()
    {
        Destroy(gameObject, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Goomba goomba = collision.collider.GetComponent<Goomba>();
        if (goomba != null)
        {
            // Fireball has kicked a goomba
            goomba.Hurt();
            ExplodeImmediately();
        }
        else
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 leftSide = new Vector2(-1f, 0f);
            Vector2 rightSide = new Vector2(1f, 0f);
            Vector2 bottomSide = new Vector2(0f, 1f);

            if (normal == leftSide || normal == rightSide)
            { // explode if side hit
                Explode();
            }
            else if (normal == bottomSide)
            { // bounce off
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, absVelocity.y);
            }
            else
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -absVelocity.y);
            }
        }
    }

}
