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
    private bool playedOnce = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!playedOnce && other.gameObject == player)
        {
            playedOnce = true;
            if (hasKey)
            {
                audioSource.clip = doorUnlockSound;
                audioSource.Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                audioSource.clip = doorLockSound;
                audioSource.Play();
            }
        }
    }
}
