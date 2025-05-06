using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private Rigidbody2D player;
    private List<Bounds> walkableBounds = new List<Bounds>();

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        GameObject[] walkableObjects = GameObject.FindGameObjectsWithTag("Walkable");

        foreach (GameObject rect in walkableObjects) {
            walkableBounds.Add(rect.GetComponent<SpriteRenderer>().bounds);
        }
    }

    void FixedUpdate()
    {
        Vector2 newPosition = player.position + player.velocity * Time.fixedDeltaTime;
        bool isWalkable = false;

        foreach (Bounds bounds in walkableBounds)
        {
            if (bounds.Contains(newPosition))
            {
                isWalkable = true;
                break;
            }
        }

        if (isWalkable)
        {
            player.position = newPosition;
        }
        else
        {
            player.velocity = Vector2.zero;
        }
    }
}
