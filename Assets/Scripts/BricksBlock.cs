using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BricksBlock : MonoBehaviour
{
    private bool _isSuperMario;
    private Tilemap tilemap;

    private void Start()
    {
        // TODO: Super Mario behaviour
        _isSuperMario = true;
        tilemap = this.GetComponentInChildren<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            if (_isSuperMario)
            {
                hitPosition.x = (float)Math.Truncate(collision.contacts[0].point.x);
                hitPosition.y = (float)Math.Truncate(collision.contacts[0].point.y);
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
    }
}
