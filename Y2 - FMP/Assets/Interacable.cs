using UnityEngine;

public class Interacable : MonoBehaviour
{
    public enum theType{button, computer, interactable}
    public theType interactableType = theType.interactable;
    private GameObject screen;
    private tasks tasksScript;

    void Start()
    {
        tasksScript = GameObject.Find("TaskCanvas").GetComponent<tasks>();
        if(interactableType == theType.computer)
        {
            screen = gameObject.transform.GetChild(0).gameObject;
            screen.SetActive(false);
        }
    }

    public void ComputerFunction()
    {
        Debug.Log("computer is active");
        screen.SetActive(true);
        tasksScript.CompleteTask("Interact with the computer");

    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
