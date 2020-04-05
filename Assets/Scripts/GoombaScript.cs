using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : MonoBehaviour
{
    public float Speed;
    public bool MoveRight;

    void Update()
    {
        if (MoveRight) 
        {
            transform.Translate(2 * Time.deltaTime * Speed, 0, 0);
            // transform.localScale = new Vector2(1, 1);
        }
        else 
        {
            transform.Translate(-2 * Time.deltaTime * Speed, 0, 0);
            // transform.localScale = new Vector2(-1, 1);
        }
    }
}
