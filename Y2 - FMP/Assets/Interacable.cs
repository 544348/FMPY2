using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Interacable : MonoBehaviour
{
    public enum theType{button, computer, interactable, NPC}
    public theType interactableType = theType.interactable;
    private GameObject screen;
    private tasks tasksScript;
    private MouseLook mouseLookScript;

    private float timer;
    public float timerInterval;

    private bool timerIsActive = false;
    public bool isComputer2;
    public bool isInteractable = true;

    private GameObject camera;
    public GameObject theDoor;
    public GameObject DeleteOutlineObject;
    public GameObject arrow;

    private MouseLook cameraLookScript;
    public AudioSource audio;
    public Animator NPCanimator;

    void Start()
    {
        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
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
        if (isInteractable)
        {
            timerIsActive = true;
            Debug.Log("computer is active");
            yield return new WaitForSeconds(1.4f);
            screen.SetActive(true);
            tasksScript.CompleteTask("Interact with the computer");
            if (isComputer2)
            {
                arrow.SetActive(false);
                tasksScript.CompleteTask("Interact with your computer");
                yield return new WaitForSeconds(1.4f);
                SceneManager.LoadScene("ComputerDesktop");
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void DisableCurrentOutline()
    {
        Debug.Log("outline should be disabled");
        // interactableObject = null;
        if (mouseLookScript.meshRen != null)
        {
            Debug.Log("Outline should be default material");
            mouseLookScript.meshRen.materials = gameObject.GetComponent<Outline>().defaultMaterials;
        }
        if (mouseLookScript.skinnedMeshRen != null)
        {
            Debug.Log("Outline should be default material");
            mouseLookScript.skinnedMeshRen.materials = gameObject.GetComponent<Outline>().defaultMaterials;
        }
    }
    public IEnumerator NPCFunction()
    {
        if (isInteractable)
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
            arrow.SetActive(true);
            Debug.Log("audio is finished");
            if (NPCanimator != null)
            {
                NPCanimator.ResetTrigger("CutscenePlaying");
            }
            StartCoroutine(cameraLookScript.ResetCameraToDefault());
            tasksScript.CompleteTask("Interact with the receptionist");
            DisableCurrentOutline();
            tasksScript.taskList.Add("Interact with your computer", false);
            isInteractable = false;
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        if (isInteractable)
        {
            if (timerIsActive)
            {
                timer += Time.deltaTime;
                if (timer >= timerInterval)
                {
                    cameraLookScript.StartCoroutine(cameraLookScript.ResetCameraToDefault());
                    if (theDoor != null)
                    {
                        theDoor.GetComponent<door>().ToggleDoor();
                    }
                    timerIsActive = false;
                    timer = 0;
                }
            }
        }
    }
}
