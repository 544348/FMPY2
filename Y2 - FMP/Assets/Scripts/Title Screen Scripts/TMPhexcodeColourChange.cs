using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace
using System.Collections;

public class TMPhexcodeColourChange : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // The TextMeshProUGUI component
    public string startColorHex = "#FFFFFF"; // Starting color in hex (white by default)
    public string targetColorHex = "#FF0000"; // Target color in hex (red by default)
    public float delayBeforeFade = 1f; // Time to wait before starting the fade (in seconds)
    public float fadeDuration = 2f; // Duration of the fade (in seconds)

    private void Start()
    {
        // Ensure the TextMeshProUGUI component is assigned
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();

        // Convert hex to Color and start the fade
        if (textComponent != null)
        {
            Color startColor = HexToColor(startColorHex);
            Color targetColor = HexToColor(targetColorHex);

            // Set initial color
            textComponent.color = startColor;

            // Start the fade coroutine
            StartCoroutine(FadeToColor(targetColor, fadeDuration, delayBeforeFade));
        }
    }

    // Coroutine that fades the color over time
    private IEnumerator FadeToColor(Color targetColor, float duration, float delay)
    {
        // Wait for the specified delay before starting the fade
        yield return new WaitForSeconds(delay);

        Color startColor = textComponent.color;
        float timeElapsed = 0f;

        // Gradually fade the color
        while (timeElapsed < duration)
        {
            textComponent.color = Color.Lerp(startColor, targetColor, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is set exactly to the target color
        textComponent.color = targetColor;
    }

    // Convert hex color code to Color object
    private Color HexToColor(string hex)
    {
        // Remove the '#' if present
        hex = hex.Replace("#", "");

        // Parse the RGB components of the hex string
        float r = Mathf.Clamp01(int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f);
        float g = Mathf.Clamp01(int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f);
        float b = Mathf.Clamp01(int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f);

        return new Color(r, g, b);
    }
}
