using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject player;
    public DoorTrigger door;
    public AudioClip pickupSound;

    // On collision trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // If player touches key
        if (other.gameObject == player)
        {
            // door's hasKey becomes true
            door.hasKey = true;
            // Play key pickup sound
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            // Destroy the key
            Destroy(gameObject);
        }
    }
}
