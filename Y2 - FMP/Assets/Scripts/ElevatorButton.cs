using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public Animator elevatorAnimator;
    public Animator buttonAnimator;
    private tasks tasksScript;
    void Start()
    {
        tasksScript = GameObject.Find("TaskCanvas").GetComponent<tasks>();
    }
    public void ButtonPress()
    {
        Debug.Log("button has been pressed");
        elevatorAnimator.SetTrigger("ButtonPress");
        buttonAnimator.SetTrigger("ButtonPress");
        tasksScript.CompleteTask("Press the button");
        tasksScript.taskList.Add(tasksScript.theTasks[tasksScript.currentTask], false);
        gameObject.GetComponent<Outline>().isInteractable = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
