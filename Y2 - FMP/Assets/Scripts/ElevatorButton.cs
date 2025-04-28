using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public Animator elevatorAnimator;
    private tasks tasksScript;
    void Start()
    {
        tasksScript = GameObject.Find("TaskCanvas").GetComponent<tasks>();
    }
    public void ButtonPress()
    {
        elevatorAnimator.SetTrigger("ButtonPress");
        tasksScript.CompleteTask("Call the elevator");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
