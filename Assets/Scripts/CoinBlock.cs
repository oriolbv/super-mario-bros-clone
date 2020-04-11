using System;
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
        myTile = new Tile();
        _isCoinBlockInitialState = true;
        tilemap = this.GetComponentInChildren<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            if (_isCoinBlockInitialState)
            {
                hitPosition.x = (float)Math.Truncate(collision.contacts[0].point.x);
                hitPosition.y = (float)Math.Truncate(collision.contacts[0].point.y);

                Debug.Log(hitPosition.x);
                Debug.Log(hitPosition.y);
                //TileBase t = tilemap.GetTile(tilemap.WorldToCell(hitPosition));
                myTile.sprite = newSprite;

                tilemap.SetTile(tilemap.WorldToCell(hitPosition), myTile);
 
                //this.transform.Translate(new Vector3(0, 0.2f, 0));
                //Wait(0.1f, () => {
                //    this.transform.Translate(new Vector3(0, -0.2f, 0));
                //});
            }
        }
    }
}
