using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float rotation;
    private float changeX;
    private float changeY;

    void Start()
    {
        // Get player's rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Get movement X and Y
        // Inputs either -1, 0, 1
        // Left, still, right
        // Down, still, Up
        changeX = Input.GetAxisRaw("Horizontal");
        changeY = Input.GetAxisRaw("Vertical");

        // Create a 2D vector from movement
        // Normalize movement so the player doesn't go faster
        // at an angle
        movement = new Vector2(changeX, changeY).normalized;

        // Rotation modifier
        rotation = 0f;
        // Moving horiz and/or going up
        if (changeX != 0 && changeY >= 0)
        {
            // Rotate player 15 degrees
            // in direction of movement
            rotation = -changeX * 15f;
        }

        rb.velocity = movement * speed;
        // Rotate player
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }
}