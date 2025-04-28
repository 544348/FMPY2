using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public KeyType requiredKey;
    public GameObject doorToActivate;

    public void ActivateDoor()
    {
        if (doorToActivate != null)
        {
            doorToActivate.SetActive(true);
        }
    }
}