using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Device;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public Transform playerCamera; // Reference to the camera to determine forward direction
    public float gravity = -9.81f;

    public GameObject playerCameraObject;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private int taskNumber = 0;
    public Dictionary<string , bool> taskList = new Dictionary<string , bool>();

    Vector3 velocity;
    bool isGrounded;

    public Animator elevatorAnimator;

    public GameObject deleteScriptRunnerObj;

    private void Start()
    {
        taskList.Add("interactWithComputer", false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ExitElevator")
        {
            elevatorAnimator.SetTrigger("ExitElevator");
        }
        if(other.gameObject.tag == "DoorTrigger")
        {
            other.transform.parent.gameObject.GetComponent<door>().ToggleDoor();
            deleteScriptRunnerObj.SetActive(true);
        }
    }

    public void CompleteTask(string taskName, string newTask)
    {
        if (taskList.ContainsKey(taskName))
        {
            taskList[taskName] = true;
            taskList.Add(newTask, false);
            taskNumber++;
        }
        else
        {
            Debug.LogError(taskName + "Task doesnt exist");
        }
    }
    void Update()
    {
        if(playerCameraObject.GetComponent<MouseLook>().canMove)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxisRaw("Horizontal"); // Use GetAxisRaw for instant stop
            float z = Input.GetAxisRaw("Vertical");

            // Get camera's forward and right directions, ignoring the Y-axis (to prevent unintended vertical movement)
            Vector3 forward = playerCamera.forward;
            Vector3 right = playerCamera.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            // Calculate movement direction
            Vector3 move = forward * z + right * x;

            // Prevents drifting by ensuring move is precisely zero when no input is given
            if (move.magnitude < 0.1f)
            {
                move = Vector3.zero;
            }

            // Move the character
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
       

    }
}