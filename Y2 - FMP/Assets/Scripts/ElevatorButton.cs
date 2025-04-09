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
        tasksScript.CompleteTask("Press the button");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
