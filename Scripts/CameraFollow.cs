using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {
    public Transform player;

    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10f);

        if (Input.GetKey("n"))
        {
            GetComponent<Camera>().orthographicSize += 1;
        }
        if (Input.GetKey("m") && GetComponent<Camera>().orthographicSize > 3)
        {
            GetComponent<Camera>().orthographicSize -= 1;
        }
    }
}
