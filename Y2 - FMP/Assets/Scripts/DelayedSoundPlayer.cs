using System.Collections;
using UnityEngine;

public class DelayedSoundPlayer : MonoBehaviour
{
    // Public variable to set the delay time (in seconds) in the Inspector
    public float delayTime = 2.0f; // Default to 2 seconds
    public AudioClip soundClip; // Drag and drop the sound clip in the Inspector
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // If the AudioSource or soundClip isn't assigned, log a warning
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on this GameObject.");
        }
        if (soundClip == null)
        {
            Debug.LogWarning("SoundClip is not assigned in the Inspector.");
        }

        // Start the delay and play the sound after the set delay
        StartCoroutine(PlaySoundAfterDelay());
    }

    IEnumerator PlaySoundAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Play the sound if the AudioSource and soundClip are assigned
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}