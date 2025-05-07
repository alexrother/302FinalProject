using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private Rigidbody2D player;
    private List<Bounds> walkableBounds = new List<Bounds>();

    // Run once at start of script
    void Start()
    {
        // Get player's rigidbody from player
        player = GetComponent<Rigidbody2D>();
        // Get array of gameobjects with tag "Walkable"
        GameObject[] walkableObjects = GameObject.FindGameObjectsWithTag("Walkable");

        // For each walkable rectangle
        // Get bounds and add to a List
        foreach (GameObject rect in walkableObjects) {
            walkableBounds.Add(rect.GetComponent<SpriteRenderer>().bounds);
        }
    }

    void FixedUpdate()
    {
        // Next position where the player will be
        // Previous position + (velocity*time)
        Vector2 newPosition = player.position + player.velocity * Time.fixedDeltaTime;

        // If the new position is inside the walkable bounds
        // isWalkable = true
        bool isWalkable = false;
        foreach (Bounds bounds in walkableBounds)
        {
            if (bounds.Contains(newPosition))
            {
                isWalkable = true;
                break;
            }
        }

        // If walkable update position
        // else set velocity to zero
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
