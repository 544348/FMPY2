using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;                // Drag the player GameObject here
    public Vector3 teleportPosition;        // Set teleport destination in Inspector

    [Header("Objects to Toggle")]
    public GameObject objectToActivate;     // Drag the object to activate
    public GameObject objectToDeactivate;   // Drag the object to deactivate

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.position = teleportPosition;
            }

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
            }
        }
    }
}
