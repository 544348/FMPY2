using UnityEngine;
using UnityEngine.UI; // Needed for UI Image

public class KeyGlow : MonoBehaviour
{
    [Header("Target Elements (assign one only)")]
    [Tooltip("SpriteRenderer for world-space objects.")]
    public SpriteRenderer spriteRenderer;

    [Tooltip("UI Image component for Canvas elements.")]
    public Image uiImage;

    [Header("Alpha Settings")]
    [Range(0f, 1f)] public float minAlpha = 0f;
    [Range(0f, 1f)] public float maxAlpha = 1f;
    public float fadeSpeed = 1f;

    private bool fadingToMax = true;

    void Update()
    {
        // Determine the current color based on the component used
        Color currentColor;

        if (spriteRenderer != null)
        {
            currentColor = spriteRenderer.color;
        }
        else if (uiImage != null)
        {
            currentColor = uiImage.color;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer or UI Image assigned.");
            return;
        }

        float targetAlpha = fadingToMax ? maxAlpha : minAlpha;
        float newAlpha = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);

        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

        // Apply the new color
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
        else if (uiImage != null)
        {
            uiImage.color = newColor;
        }

        // Switch direction when target alpha is reached
        if (Mathf.Approximately(newAlpha, targetAlpha))
        {
            fadingToMax = !fadingToMax;
        }
    }
}