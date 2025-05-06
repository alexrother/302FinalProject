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
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        changeX = Input.GetAxisRaw("Horizontal");
        changeY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(changeX, changeY).normalized;

        rotation = 0f;
        if (changeY >= 0 && changeX != 0)
        {
            rotation = -changeX * 15f;
        }

        rb.velocity = movement * speed;
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }
}