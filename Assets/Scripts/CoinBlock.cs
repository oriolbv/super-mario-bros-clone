﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinBlock : ExtendedBehaviour
{
    private bool _isCoinBlockInitialState;
    public Sprite UsedBlockSprite;

    public AudioClip CoinAudioClip;
    private AudioSource coinAudioSource;

    private Animator animator;



    void Start()
    {
        _isCoinBlockInitialState = true;
        animator = this.GetComponentInChildren<Animator>();

        coinAudioSource = this.GetComponentInChildren<AudioSource>();
       
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
                // Reproduce sound
                coinAudioSource.clip = CoinAudioClip;
                coinAudioSource.Play();
                // Coins counter & puntuation
                GameScore.Instance.Score += 100;
                GameScore.Instance.Coins += 1;
            }
        }
    }
}
