using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {
    public Transform player;

    // N times per frame
    void FixedUpdate()
    {
        // Change camera position to player position
        transform.position = new Vector3(player.position.x, player.position.y, -10f);

        // If N pressed zoom out
        if (Input.GetKey("n"))
        {
            GetComponent<Camera>().orthographicSize += 1;
        }
        // If m pressed zoom in, maxzoom 3
        if (Input.GetKey("m") && GetComponent<Camera>().orthographicSize > 3)
        {
            GetComponent<Camera>().orthographicSize -= 1;
        }
    }
}
