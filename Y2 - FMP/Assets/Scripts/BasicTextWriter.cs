using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace
using System.Collections;

public class BasicTextWriter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // The TextMeshProUGUI component
    public string fullText;               // The full text to be typed out
    public float typingSpeed = 0.1f;      // Speed of typing (seconds per character)
    public float startDelay = 1f;         // Delay before typing starts (in seconds)

    public bool isWorldSpace = false;

    private void Start()
    {
        // Ensure the TextMeshProUGUI component is assigned
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();

        // Start the typing coroutine
        if (textComponent != null && !string.IsNullOrEmpty(fullText))
        {
            StartCoroutine(TypeText());
        }
    }
    private void OnEnable()
    {
        // Ensure the TextMeshProUGUI component is assigned
        if (textComponent == null)
        textComponent = GetComponent<TextMeshProUGUI>();

        // Start the typing coroutine
        if (textComponent != null && !string.IsNullOrEmpty(fullText) && isWorldSpace)
        {
            StartCoroutine(TypeText());
        }
    }

    // Coroutine that types the text one character at a time with a delay before starting
    private IEnumerator TypeText()
    {
        Debug.Log("text should be typing");
        // Wait for the delay before starting the typing effect
        yield return new WaitForSeconds(startDelay);

        textComponent.text = ""; // Clear the text initially
        Debug.Log("text should be clear");
        foreach (char letter in fullText)
        {
            Debug.Log(letter + "should be typed");
            textComponent.text += letter; // Add one character at a time
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified time
        }
    }
}