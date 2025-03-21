using UnityEngine;

public class RotationScript : MonoBehaviour
{
    // The object to rotate (drag and drop in the inspector)
    [SerializeField]
    private GameObject objectToRotate;

    // Rotation speed (can be adjusted in the inspector)
    [SerializeField]
    private float rotationSpeed = 50f;

    // Rotation axis (can be adjusted in the inspector)
    [SerializeField]
    private Vector3 rotationAxis = new Vector3(0, 1, 0); // Default is rotating around the Y-axis

    void Update()
    {
        // Check if the objectToRotate is assigned in the inspector
        if (objectToRotate != null)
        {
            // Rotate the object around its center at the specified rotation speed and axis
            objectToRotate.transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}