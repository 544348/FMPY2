using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmissionFlicker : MonoBehaviour
{
    [Header("Settings")]
    // Drag and drop the Renderer of the GameObject whose material you want to change
    public Renderer objectRenderer;

    // List of emission intensities (can be expanded in the inspector)
    public List<float> emissionIntensities = new List<float> { 1f, 5f, 3f, 0.5f };

    // Duration between each flicker transition (in seconds)
    public float transitionDuration = 1f;

    // Time to wait before restarting the flicker cycle
    public float waitTime = 1f;

    // Hex code for the base color (e.g., "#FF5733" or "#00FF00")
    public string baseHexColor = "#FF5733";  // Default base color is #FF5733 (a red-like color)

    private int currentIntensityIndex = 0;
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
    private Color baseColor;

    private void Start()
    {
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer is not assigned! Please assign the renderer of the object.");
            return;
        }

        // Convert the hex color string to a Color object for the base color
        if (ColorUtility.TryParseHtmlString(baseHexColor, out baseColor))
        {
            objectRenderer.material.color = baseColor;  // Set the base color of the material
        }
        else
        {
            Debug.LogError("Invalid hex color string provided. Falling back to default color.");
            baseColor = Color.white;  // Default to white if the hex is invalid
            objectRenderer.material.color = baseColor; // Set to white if invalid
        }

        // Ensure the material uses emission
        objectRenderer.material.EnableKeyword("_EMISSION");

        // Start the emission flicker cycle
        StartCoroutine(FlickerEmission());
    }

    private IEnumerator FlickerEmission()
    {
        while (true)
        {
            // Get the current emission intensity from the list
            float targetIntensity = emissionIntensities[currentIntensityIndex];

            // Modify only the emission intensity by multiplying the base color
            Color startEmissionColor = objectRenderer.material.GetColor(EmissionColorID);
            Color targetEmissionColor = baseColor * targetIntensity;

            float timeElapsed = 0f;

            // Gradually change the emission intensity to the target value
            while (timeElapsed < transitionDuration)
            {
                // Lerp the emission color based on elapsed time
                objectRenderer.material.SetColor(EmissionColorID, Color.Lerp(startEmissionColor, targetEmissionColor, timeElapsed / transitionDuration));
                timeElapsed += Time.deltaTime;

                // Wait a frame before updating the emission color
                yield return null;
            }

            // Ensure the final emission color is set after transition completes
            objectRenderer.material.SetColor(EmissionColorID, targetEmissionColor);

            // Wait for the specified wait time before starting the next flicker
            yield return new WaitForSeconds(waitTime);

            // Move to the next intensity in the list
            currentIntensityIndex = (currentIntensityIndex + 1) % emissionIntensities.Count;
        }
    }

    // Optionally, you can call this to turn off emission
    public void TurnOffEmission()
    {
        objectRenderer.material.DisableKeyword("_EMISSION");
    }
}
