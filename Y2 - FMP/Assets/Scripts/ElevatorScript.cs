using System.Runtime.CompilerServices;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    private Animator theElevatorAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has stepped on elevator");
            other.transform.parent = gameObject.transform;
            other.gameObject.GetComponent<PlayerMovement>().elevatorAnimator = theElevatorAnimator;
            theElevatorAnimator.SetTrigger("PlayerHasSteppedOnElevator");
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
            //  elevatorAnimator.SetTrigger("PlayerHasSteppedOnElevator");
            Debug.Log("player has exited elevator");
        }
    }
    private void Awake()
    {
        theElevatorAnimator = GetComponent<Animator>();
    }
    public void OpenBackDoor()
    {
        theElevatorAnimator.SetTrigger("BackdoorOpen");
        Debug.Log("Elevator door should open");
    }
}
