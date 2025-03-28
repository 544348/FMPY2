using UnityEngine;
using System.Collections;

public class DeleteCanvasObjects : MonoBehaviour
{
    public GameObject[] canvasObjects;   // Array to store multiple canvas objects to delete
    public float delayBeforeDelete = 2f;  // Delay before starting the deletion (in seconds)

    private void Start()
    {
        // Start the deletion process after the specified delay
        StartCoroutine(DeleteObjectsAfterDelay());
    }

    // Coroutine that deletes the objects after a specified delay
    private IEnumerator DeleteObjectsAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeDelete);

        // Loop through each object in the array and destroy it
        foreach (GameObject obj in canvasObjects)
        {
            if (obj != null)
            {
                Destroy(obj);  // Destroy the GameObject
                Debug.Log("Deleted: " + obj.name);  // Log the name of the object that was deleted
            }
            else
            {
                Debug.LogWarning("A canvas object is null and could not be deleted.");
            }
        }
    }
}
