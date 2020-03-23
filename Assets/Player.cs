using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Horizontal Movement")]
    private float moveSpeed = 10f;
    private Vector2 direction;

    [Header("Components")]
    public Rigidbody2D rb;

    void Update()
    {
        // Left direction: -1 | Idle: 0 | Right direction: 1
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate() 
    {
        moveCharacter(direction.x);
    }

    void moveCharacter(float horizontal) 
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);
    }
}
