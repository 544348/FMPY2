using UnityEngine;

public class TemporaryMovingObject : MonoBehaviour
{
    public GameObject objectToMove; // Drag and drop your GameObject here in the Inspector
    public Vector3 targetPosition; // The point to move to
    public float moveSpeed = 5f;   // Speed at which the object moves

    void Update()
    {
        if (objectToMove != null)
        {
            // Move the object towards the target position
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
