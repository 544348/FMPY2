using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;
    public float raycastDistance = 0f;

    public LayerMask interactable;

    public RaycastHit interactableHit;

    private GameObject lastInteractableObjectLookedAt;
    private MeshRenderer meshRen;
    public Material[] theMaterials;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX; // Allow full horizontal rotation

        // Apply rotation to camera and player body
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //playerBody.Rotate(Vector3.up * mouseX);
        Physics.Raycast(gameObject.transform.GetChild(0).gameObject.transform.position, -gameObject.transform.GetChild(0).gameObject.transform.right, out interactableHit, raycastDistance, interactable);
        Debug.DrawRay(gameObject.transform.GetChild(0).gameObject.transform.position, -gameObject.transform.GetChild(0).gameObject.transform.right, Color.green);
        if( interactableHit.collider != null )
        {
            Debug.Log("Raycast has hit" + interactableHit.collider.name);
            Debug.Log("Raycast has hit" + interactableHit.collider.gameObject.layer);
            meshRen = interactableHit.collider.gameObject.GetComponent<MeshRenderer>();
            meshRen.materials = interactableHit.collider.gameObject.GetComponent<Outline>().defaultAndOutline;
            lastInteractableObjectLookedAt = interactableHit.collider.gameObject;
        }
        else
        {
            Debug.Log("Looking at different object");
            if(meshRen != null)
            {
                meshRen.materials = lastInteractableObjectLookedAt.GetComponent<Outline>().defaultMaterials;
            }
        }
    }
}
