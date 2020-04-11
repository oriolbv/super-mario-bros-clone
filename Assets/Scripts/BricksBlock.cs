using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BricksBlock : MonoBehaviour
{
    private bool _isSuperMario;

    private void Start()
    {
        // TODO: Super Mario behaviour
        _isSuperMario = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() && collision.contacts[0].normal.y > 0.5f)
        {
            if (_isSuperMario)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
