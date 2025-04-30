using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;
    public GameObject player;
    public Transform camDefaultTransform;

    float xRotation = 0f;
    float yRotation = 0f;
    public float raycastDistance = 0f;

    private GameObject interactableObject;
    public GameObject camLockObject;

    public LayerMask interactable;

    public RaycastHit interactableHit;
    public RaycastHit interactableObjectHit;

    private Interacable interactableScript;

    public GameObject camera;
    public bool canMove = true;

    private GameObject lastInteractableObjectLookedAt;
    private MeshRenderer meshRen;
    public Material[] theMaterials;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public IEnumerator ResetCameraToDefault()
    {
        gameObject.transform.parent = player.transform;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CamMove(camDefaultTransform.localPosition, camDefaultTransform.localEulerAngles));
        Debug.LogWarning(camDefaultTransform.localPosition + camDefaultTransform.localEulerAngles);
    //    Debug.LogWarning("warningtest 1");
        canMove = true;
    }
    private IEnumerator CamMove(Vector3 position, Vector3 rotation)
    {
        yield return new WaitForSeconds(0.1f);
        camera.transform.localPosition = position;
        yield return new WaitForSeconds(0.1f);
        camera.transform.eulerAngles = rotation;
        Debug.Log("rotation should be " + rotation);
     //   Debug.LogError("test2");
    }
    private IEnumerator PlayerMove(Vector3 position, Vector3 rotation)
    {
        player.transform.localPosition = position;
      //  Debug.LogError("test3");
        yield return new WaitForSeconds(0.1f);
      //  Debug.LogError("test4");
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.transform.eulerAngles = rotation;
        Debug.Log("rotation should be " + rotation);
    }
    private void CamLock()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(ResetCameraToDefault());
        }
        if (canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += mouseX; // Allow full horizontal rotation

            if (Input.GetKeyDown(KeyCode.E) && interactableObject != null)
            {
                Physics.Raycast(gameObject.transform.GetChild(0).gameObject.transform.position, -gameObject.transform.GetChild(0).gameObject.transform.right, out interactableObjectHit, raycastDistance, interactable);
                Debug.DrawRay(gameObject.transform.GetChild(0).gameObject.transform.position, -gameObject.transform.GetChild(0).gameObject.transform.right, Color.red);
                Debug.Log("Has interacted");
                if (interactableObject.GetComponent<ElevatorButton>() != null)
                {
                    interactableObject.GetComponent<ElevatorButton>().ButtonPress();
                    interactableObject.GetComponent<Animator>().SetTrigger("ButtonPress");
                    if (interactableObjectHit.collider.gameObject.tag == "Button")
                    {
                        interactableObject = interactableObjectHit.collider.gameObject;
                    }
                }
                else if (interactableObject.GetComponent<Interacable>() != null)
                {
                    interactableScript = interactableObject.GetComponent<Interacable>();
                    if (interactableScript.interactableType == Interacable.theType.computer)
                    {
                        CamLock();
                        gameObject.transform.parent = null;
                        StartCoroutine(CamMove(interactableObject.transform.GetChild(8).transform.position, interactableObject.transform.GetChild(8).transform.eulerAngles));
                        StartCoroutine(PlayerMove(interactableObject.transform.GetChild(9).transform.position, interactableObject.transform.GetChild(9).transform.eulerAngles));
                      //  Debug.LogError("test");
                        StartCoroutine(interactableScript.ComputerFunction());


                    }
                    if (interactableScript.interactableType == Interacable.theType.NPC)
                    {
                        CamLock();
                        gameObject.transform.parent = null;
                        StartCoroutine(CamMove(interactableObject.transform.GetChild(0).transform.position, interactableObject.transform.GetChild(0).transform.eulerAngles));
                        StartCoroutine(PlayerMove(interactableObject.transform.GetChild(1).transform.position, interactableObject.transform.GetChild(1).transform.eulerAngles));
                        //  Debug.LogError("test");
                        StartCoroutine(interactableScript.NPCFunction());


                    }
                }
            }
            // Apply rotation to camera and player body
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            //playerBody.Rotate(Vector3.up * mouseX);
            Physics.Raycast(gameObject.transform.GetChild(0).gameObject.transform.position, -gameObject.transform.GetChild(0).gameObject.transform.right, out interactableHit, raycastDistance, interactable);
            Debug.DrawRay(gameObject.transform.GetChild(0).gameObject.transform.position, -gameObject.transform.GetChild(0).gameObject.transform.right, Color.green);
            if (interactableHit.collider != null)
            {
                interactableObject = interactableHit.collider.gameObject;

                Debug.Log("Raycast has hit" + interactableHit.collider.name);
                Debug.Log("Raycast has hit" + interactableHit.collider.gameObject.layer);
                meshRen = interactableHit.collider.gameObject.GetComponent<Outline>().meshRenderer;
                meshRen.materials = interactableHit.collider.gameObject.GetComponent<Outline>().defaultAndOutline;
                lastInteractableObjectLookedAt = interactableHit.collider.gameObject;
            }
            else
            {
                Debug.Log("Looking at different object");
                interactableObject = null;
                if (meshRen != null)
                {
                    meshRen.materials = lastInteractableObjectLookedAt.GetComponent<Outline>().defaultMaterials;
                }
            }
        }
        
    }
}
