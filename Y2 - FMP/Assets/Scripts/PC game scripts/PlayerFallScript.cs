using UnityEngine;

public class PlayerFallScript : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;                // Drag the player GameObject here
    public Vector3 teleportPosition;        // Set teleport destination in Inspector
    public Vector3 killwallLeftTeleport;
    public Vector3 killwallRightTeleport;

    public Transform killwallLeft;
    public Transform killwallRight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.position = teleportPosition;
            }
            if (killwallLeft && killwallRight != null)
            {
                player.position = teleportPosition;
                killwallRight.position = killwallRightTeleport;
                killwallLeft.position = killwallLeftTeleport;
            }
        }
    }
}
