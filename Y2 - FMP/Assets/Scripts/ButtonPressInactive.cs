using UnityEngine;
using UnityEngine.UI; // Required for UI elements like buttons

public class ButtonPressInactive : MonoBehaviour
{
    // Adjustable Variables
    public GameObject objectToDeactivate;  // The GameObject that will be set inactive
    public Button toggleButton;            // The Button that will trigger the action
    public bool defaultState = true;       // Default state of the object (true = active, false = inactive)

    void Start()
    {
        // Set the object initial state (active or inactive)
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(defaultState);
        }
        else
        {
            Debug.LogWarning("Object to deactivate is not assigned.");
        }

        // Check if the button is assigned
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(DeactivateObject);
        }
        else
        {
            Debug.LogWarning("Toggle button is not assigned.");
        }
    }

    // Method to deactivate the object
    void DeactivateObject()
    {
        if (objectToDeactivate != null)
        {
            // Set the object to inactive
            objectToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Object to deactivate is not assigned.");
        }
    }
}

