using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace
using System.Collections;

public class FadeOutText : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // The TextMeshProUGUI component
    public float delayBeforeFadeOut = 1f; // Time to wait before starting the fade out (in seconds)
    public float fadeOutDuration = 2f;    // Duration of the fade out (in seconds)

    private void Start()
    {
        // Ensure the TextMeshProUGUI component is assigned
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();

        // Start the fade-out coroutine
        if (textComponent != null)
        {
            StartCoroutine(FadeOut());
        }
    }

    // Coroutine that fades out the text color over time
    private IEnumerator FadeOut()
    {
        // Wait for the specified delay before starting the fade out
        yield return new WaitForSeconds(delayBeforeFadeOut);

        Color startColor = textComponent.color;
        float timeElapsed = 0f;

        // Gradually fade out the alpha channel (transparency) of the text
        while (timeElapsed < fadeOutDuration)
        {
            // Calculate the new alpha value (0 is fully transparent)
            float alpha = Mathf.Lerp(startColor.a, 0f, timeElapsed / fadeOutDuration);
            textComponent.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is fully transparent at the end
        textComponent.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}