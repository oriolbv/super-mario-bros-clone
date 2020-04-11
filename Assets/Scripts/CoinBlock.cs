using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinBlock : ExtendedBehaviour
{
    private bool _isCoinBlockInitialState;
    public Sprite UsedBlockSprite;

    void Start()
    {
        _isCoinBlockInitialState = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            this.GetComponentInChildren<SpriteRenderer>().sprite = UsedBlockSprite;
            this.transform.Translate(new Vector3(0, 0.2f, 0));
            Wait(0.1f, () => {
                this.transform.Translate(new Vector3(0, -0.2f, 0));
            });
        }
    }
}
