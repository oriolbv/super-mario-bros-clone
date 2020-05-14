using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBlock : MonoBehaviour
{
    private bool _isFlowerBlockInitialState;
    public Sprite UsedBlockSprite;

    public GameObject Flower;

    private AudioSource blockAudioSource;

    private Animator animator;

    void Start()
    {
        _isFlowerBlockInitialState = true;
        animator = this.GetComponentInChildren<Animator>();

        blockAudioSource = this.GetComponentInChildren<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            if (_isFlowerBlockInitialState)
            {
                _isFlowerBlockInitialState = false;
                this.GetComponentInChildren<SpriteRenderer>().sprite = UsedBlockSprite;
                animator.SetTrigger("useBlock");
                // Reproduce sound
                blockAudioSource.Play();

                // Mario to Fire state
                collision.collider.GetComponent<Player>().UpdateMarioState(PlayerState.Fire);
                
            }
        }
    }
}
