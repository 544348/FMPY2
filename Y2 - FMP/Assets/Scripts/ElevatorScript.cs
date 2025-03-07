using System.Runtime.CompilerServices;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    private Animator theElevatorAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = gameObject.transform;
            theElevatorAnimator.SetTrigger("PlayerHasSteppedOnElevator");
            other.gameObject.GetComponent<PlayerMovement>().elevatorAnimator = theElevatorAnimator;
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

}
