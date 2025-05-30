using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;                // Drag the player GameObject here
    public Vector3 teleportPosition;        // Set teleport destination in Inspector

    [Header("Objects to Toggle")]
    public GameObject objectToActivate;     // Drag the object to activate
    public GameObject objectToDeactivate;   // Drag the object to deactivate
    public GameObject nextLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.position = teleportPosition;
                player.gameObject.GetComponent<Collider>().enabled = false;
                player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                player.gameObject.GetComponent<Rigidbody>().useGravity = false;
                player.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            }

            if (objectToActivate != null )
            {
                objectToActivate.SetActive(true);
                player.gameObject.GetComponent<Movement2D>().StartCoroutine(player.gameObject.GetComponent<Movement2D>().CoinCountUp(gameObject));
                player.gameObject.GetComponent<Movement2D>().velocity = Vector3.zero;
            }

            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
            }
        }
    }
}
