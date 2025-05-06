using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject player;
    public DoorTrigger door;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            door.hasKey = true;
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
        }
    }
}
