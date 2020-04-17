using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBlock : MonoBehaviour
{
    private bool _isPowerupBlockInitialState;
    public Sprite UsedBlockSprite;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _isPowerupBlockInitialState = true;
        animator = this.GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            if (_isPowerupBlockInitialState)
            {
                _isPowerupBlockInitialState = false;
                this.GetComponentInChildren<SpriteRenderer>().sprite = UsedBlockSprite;
                animator.SetTrigger("useBlock");
            }
        }
    }
}
