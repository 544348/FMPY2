using UnityEngine;

public class DestroyLoopedObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Debug message to ensure the trigger is being detected
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        // Check if the other object has the tag 'DestroyTitleScreenLoopedObjects'
        if (other.CompareTag("DestroyTitleScreenLoopedObjects"))
        {
            // Debug message to confirm the tag match
            Debug.Log("Object with correct tag found. Destroying: " + other.gameObject.name);

            // Destroy the GameObject that entered the trigger
            Destroy(other.gameObject);
        }
    }
}