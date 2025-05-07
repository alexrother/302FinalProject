using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public GameObject player;
    public AudioClip doorUnlockSound;
    public AudioClip doorLockSound;
    public bool hasKey = false;

    private AudioSource audioSource;

    // Script startup
    void Start()
    {
        // Get audiosource
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If player touches door
        if (other.gameObject == player)
        {
            if (hasKey)
            {
                // Change audioclip to unlocked sound
                // and play
                audioSource.clip = doorUnlockSound;
                audioSource.Play();
                // Reload the scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                // Change audioclip to locked sound
                // and play
                audioSource.clip = doorLockSound;
                audioSource.Play();
            }
        }
    }
}
