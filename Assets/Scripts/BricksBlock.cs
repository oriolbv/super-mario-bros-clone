using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BricksBlock : ExtendedBehaviour
{
    private bool _isSuperMario;

    private Animator animator;

    private void Start()
    {
        // TODO: Super Mario behaviour
        _isSuperMario = false;
        animator = this.GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            animator.SetTrigger("useBlock");
            if (_isSuperMario)
            {
                Wait(0.2f, () => {
                    Destroy(this.gameObject);
                });
            }
        }
    }
}
