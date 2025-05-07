using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip footstepClip;
    private AudioSource audioSource;
    private Rigidbody2D rb;

    void Start()
    {
        // Get player's rigidbody
        rb = GetComponent<Rigidbody2D>();
        // Get player's audiosource
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // If player's velocity isn't zero
        if (rb.velocity != Vector2.zero)
        {
            // If audio is not already playing
            if (!audioSource.isPlaying)
            {
                // Play footstep sound
                audioSource.Play();
            }
        }
        // If player isn't moving (velocity==0)
        else
        {
            // Turn off footstep sound
            audioSource.Stop();
        }
    }
}
