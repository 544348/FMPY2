using UnityEngine;
using System.Collections;

public class Interacable : MonoBehaviour
{
    public enum theType{button, computer, interactable}
    public theType interactableType = theType.interactable;
    private GameObject screen;
    private tasks tasksScript;
    private float timer;
    public float timerInterval;
    private bool timerIsActive = false;
    private GameObject camera;
    public GameObject theDoor;
    private MouseLook cameraLookScript;

    void Start()
    {
        camera = Camera.main.gameObject;
        cameraLookScript = camera.GetComponent<MouseLook>();
        tasksScript = GameObject.Find("TaskCanvas").GetComponent<tasks>();
        if(interactableType == theType.computer)
        {
            screen = gameObject.transform.GetChild(0).gameObject;
            screen.SetActive(false);
        }
    }
   

    public void ComputerFunction()
    {
        timerIsActive = true;
        Debug.Log("computer is active");
        screen.SetActive(true);
        tasksScript.CompleteTask("Interact with the computer");

    }

   

    // Update is called once per frame
    void Update()
    {
        if(timerIsActive)
        {
            timer += Time.deltaTime;
            if(timer >= timerInterval)
            {
                cameraLookScript.StartCoroutine(cameraLookScript.ResetCameraToDefault());
                if(theDoor != null)
                {
                    theDoor.GetComponent<door>().ToggleDoor();
                }
                timerIsActive = false;
                timer = 0;
            }
        }
    }
}
