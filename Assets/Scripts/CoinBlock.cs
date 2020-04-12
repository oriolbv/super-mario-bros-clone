using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinBlock : ExtendedBehaviour
{
    private bool _isCoinBlockInitialState;
    public Sprite UsedBlockSprite;


    private Animator animator;

    void Start()
    {
        _isCoinBlockInitialState = true;
        animator = this.GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            if (_isCoinBlockInitialState)
            {
                _isCoinBlockInitialState = false;
                this.GetComponentInChildren<SpriteRenderer>().sprite = UsedBlockSprite;
                animator.SetTrigger("useBlock");
            }
        }
    }
}
