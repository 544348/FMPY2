using UnityEngine;

public class door : MonoBehaviour
{
    private Animator doorAnimator;

    public void ToggleDoor()
    {
        doorAnimator.SetTrigger("ToggleDoor");
        Debug.Log("door should toggle");
    }
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }
}
