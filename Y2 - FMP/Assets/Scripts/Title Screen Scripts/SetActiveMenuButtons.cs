using UnityEngine;
using System.Collections;

public class SetActiveMenuButtons : MonoBehaviour
{
    // Array to hold multiple GameObjects (e.g., Canvases or other UI objects)
    public GameObject[] objectsToActivate; // Drag your GameObjects here

    // Delay time before activating or deactivating the objects
    public float activationDelay = 0f;  // Set delay time in seconds
    public float deactivationDelay = 0f;

    void Start()
    {
        // Optionally activate the objects on start after delay
        if (objectsToActivate.Length > 0)
        {
            StartCoroutine(ActivateObjectsWithDelay());
        }
        else
        {
            Debug.LogError("No GameObjects assigned to objectsToActivate.");
        }
    }

    // Coroutine to activate objects after delay
    public IEnumerator ActivateObjectsWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);  // Enable the GameObject
            }
        }
    }

    // Coroutine to deactivate objects after delay
    public IEnumerator DeactivateObjectsWithDelay()
    {
        yield return new WaitForSeconds(deactivationDelay);

        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);  // Disable the GameObject
            }
        }
    }

    // Public method to trigger activation manually with delay
    public void ActivateObjects()
    {
        StartCoroutine(ActivateObjectsWithDelay());
    }

    // Public method to trigger deactivation manually with delay
    public void DeactivateObjects()
    {
        StartCoroutine(DeactivateObjectsWithDelay());
    }
}