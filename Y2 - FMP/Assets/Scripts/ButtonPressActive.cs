using UnityEngine;
using UnityEngine.UI; // Required for UI elements like buttons

public class ButtonPressActive : MonoBehaviour
{
    // Adjustable Variables
    public GameObject objectToActivate;  // The GameObject that will be set active/inactive
    public Button toggleButton;          // The Button that will trigger the action
    public bool defaultState = false;    // Default state of the object (true = active, false = inactive)

    void Start()
    {
        // Set the object initial state (active or inactive)
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(defaultState);
        }
        else
        {
            Debug.LogWarning("Object to activate is not assigned.");
        }

        // Check if the button is assigned
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleObjectState);
        }
        else
        {
            Debug.LogWarning("Toggle button is not assigned.");
        }
    }

    // Method to toggle the object's active state
    void ToggleObjectState()
    {
        if (objectToActivate != null)
        {
            // Toggle the active state of the object
            objectToActivate.SetActive(!objectToActivate.activeSelf);
        }
        else
        {
            Debug.LogWarning("Object to activate is not assigned.");
        }
    }
}