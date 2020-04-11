﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinBlock : ExtendedBehaviour
{
    private bool _isCoinBlockInitialState;
    public Sprite newSprite;
    private Tile myTile;
    private Tilemap tilemap;

    void Start()
    {
        
        _isCoinBlockInitialState = true;
        tilemap = this.GetComponentInChildren<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            myTile = new Tile();
            if (_isCoinBlockInitialState)
            {
                hitPosition.x = (float)Math.Truncate(collision.contacts[0].point.x);
                hitPosition.y = (float)Math.Truncate(collision.contacts[0].point.y);

                Debug.Log(myTile.sprite);
                Debug.Log(newSprite);
                if (myTile.sprite != newSprite)
                {
                    myTile.sprite = newSprite;
                    tilemap.SetTile(tilemap.WorldToCell(hitPosition), myTile);
                }
            }
        }
    }
}
