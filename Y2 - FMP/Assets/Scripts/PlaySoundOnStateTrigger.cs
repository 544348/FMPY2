using UnityEngine;

public class PlaySoundOnStateTrigger : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator component
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip soundToPlay;  // The sound clip to play
    public string stateNameToTrigger = "YourAnimatorState";  // The name of the animator state
    public int layerToCheck = 0;  // The layer to check (0 for the first layer, 1 for the second, etc.)

    private bool soundPlayed = false;  // Flag to check if the sound has been played

    private void Start()
    {
        // If animator is not assigned, try to get it from the GameObject
        if (animator == null)
            animator = GetComponent<Animator>();

        // If audioSource is not assigned, try to get it from the GameObject
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Debugging output to check current state
        Debug.Log("Current State on Layer " + layerToCheck + ": " + animator.GetCurrentAnimatorStateInfo(layerToCheck).IsName(stateNameToTrigger));

        // If the desired state is active on the selected layer and the sound has not been played yet
        if (IsAnimatorStateActive(stateNameToTrigger) && !soundPlayed)
        {
            PlaySound();
            soundPlayed = true;  // Set the flag to true so the sound is not played again
        }
        else if (!IsAnimatorStateActive(stateNameToTrigger) && soundPlayed)
        {
            soundPlayed = false;  // Reset the flag once the state is no longer active
        }
    }

    // Function to check if the current animation state is active on the selected layer
    private bool IsAnimatorStateActive(string stateName)
    {
        // Check if the current state name matches the one we want to trigger the sound on, on the selected layer
        return animator.GetCurrentAnimatorStateInfo(layerToCheck).IsName(stateName);
    }

    // Function to play the sound
    private void PlaySound()
    {
        if (audioSource != null && soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);  // Play the sound clip once
        }
    }
}