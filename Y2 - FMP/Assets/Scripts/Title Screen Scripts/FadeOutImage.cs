using UnityEngine;
using UnityEngine.UI;  // Import the UnityEngine.UI namespace for UI components
using System.Collections;

public class FadeOutImage : MonoBehaviour
{
    public Image imageComponent; // The Image UI component
    public float delayBeforeFadeOut = 1f; // Time to wait before starting the fade out (in seconds)
    public float fadeOutDuration = 2f;    // Duration of the fade out (in seconds)

    private void Start()
    {
        // Ensure the Image component is assigned
        if (imageComponent == null)
            imageComponent = GetComponent<Image>();

        // Start the fade-out coroutine
        if (imageComponent != null)
        {
            StartCoroutine(FadeOut());
        }
    }

    // Coroutine that fades out the image's alpha over time
    private IEnumerator FadeOut()
    {
        // Wait for the specified delay before starting the fade out
        yield return new WaitForSeconds(delayBeforeFadeOut);

        Color startColor = imageComponent.color;
        float timeElapsed = 0f;

        // Gradually fade out the alpha channel (transparency) of the image
        while (timeElapsed < fadeOutDuration)
        {
            // Calculate the new alpha value (0 is fully transparent)
            float alpha = Mathf.Lerp(startColor.a, 0f, timeElapsed / fadeOutDuration);
            imageComponent.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the image is fully transparent at the end
        imageComponent.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}
