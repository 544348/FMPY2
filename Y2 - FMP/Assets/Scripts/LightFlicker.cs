using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightFlicker : MonoBehaviour
{
    [Header("Settings")]
    // Drag and drop the Light source in the Inspector
    public Light spotlight;

    // List of intensity values (can be expanded in the inspector)
    public List<float> lightIntensities = new List<float> { 1f, 5f, 3f, 0.5f };

    // Duration between each flicker transition (in seconds)
    public float transitionDuration = 1f;

    // Time to wait before restarting the flicker cycle
    public float waitTime = 1f;

    // If you want to control the flicker speed in an easier manner, you can add a drag here
    public float drag = 0.05f;

    private int currentIntensityIndex = 0;

    private void Start()
    {
        if (spotlight == null)
        {
            Debug.LogError("Spotlight is not assigned! Please assign the light source.");
            return;
        }

        // Start the light flicker cycle
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // Get the next target intensity
            float targetIntensity = lightIntensities[currentIntensityIndex];

            // Gradually change intensity to the target
            float startIntensity = spotlight.intensity;
            float timeElapsed = 0f;

            while (timeElapsed < transitionDuration)
            {
                // Lerp the intensity based on elapsed time
                spotlight.intensity = Mathf.Lerp(startIntensity, targetIntensity, timeElapsed / transitionDuration);
                timeElapsed += Time.deltaTime;

                // Apply drag to smoothen the transition if needed
                spotlight.intensity -= drag * Time.deltaTime;

                // Wait a frame before updating the intensity
                yield return null;
            }

            // Set the final intensity after transition completes
            spotlight.intensity = targetIntensity;

            // Wait for the specified wait time before starting the next flicker
            yield return new WaitForSeconds(waitTime);

            // Move to the next intensity in the list
            currentIntensityIndex = (currentIntensityIndex + 1) % lightIntensities.Count;
        }
    }
}
