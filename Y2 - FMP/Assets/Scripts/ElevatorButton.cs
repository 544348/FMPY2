using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public Animator elevatorAnimator;
    void Start()
    {
        
    }
    public void ButtonPress()
    {
        elevatorAnimator.SetTrigger("ButtonPress");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
