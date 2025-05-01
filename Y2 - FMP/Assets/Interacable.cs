using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Interacable : MonoBehaviour
{
    public enum theType{button, computer, interactable, NPC}
    public theType interactableType = theType.interactable;
    private GameObject screen;
    private tasks tasksScript;

    private float timer;
    public float timerInterval;

    private bool timerIsActive = false;
    public bool isComputer2;

    private GameObject camera;
    public GameObject theDoor;
    public GameObject DeleteOutlineObject;

    private MouseLook cameraLookScript;
    public AudioSource audio;
    public Animator NPCanimator;

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
   

    public IEnumerator ComputerFunction()
    {
        timerIsActive = true;
        Debug.Log("computer is active");
        yield return new WaitForSeconds(1.4f);
        screen.SetActive(true);
        tasksScript.CompleteTask("Interact with the computer");
        if(isComputer2)
        {
            yield return new WaitForSeconds(1.4f);
            SceneManager.LoadScene("ComputerDesktop");
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public IEnumerator NPCFunction()
    {
        audio.Play();
        if (DeleteOutlineObject != null)
        {
            DeleteOutlineObject.SetActive(false);
        }
        if (NPCanimator != null)
        {
            NPCanimator.SetTrigger("CutscenePlaying");
        }
        yield return new WaitUntil(() => !audio.isPlaying);
        Debug.Log("audio is finished");
        if (NPCanimator != null)
        {
            NPCanimator.ResetTrigger("CutscenePlaying");
        }
        StartCoroutine(cameraLookScript.ResetCameraToDefault());
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
