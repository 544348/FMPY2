using System.Collections.Generic;
using UnityEngine;
public enum KeyType
{
    Red,
    Blue,
    Green
}
[RequireComponent(typeof(Rigidbody))]

public class Movement2D : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("Jumping")]
    public float jumpForce = 8f;
    public float gravity = 20f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Key Collection")]
    public List<KeyType> collectedKeys = new List<KeyType>();
    private Transform collectedKeyVisual;
    [SerializeField] private Vector3 keyOffset = new Vector3(0.5f, 1.2f, 0f);
    [SerializeField] private float keyDistance = 1.0f;

    private Rigidbody rb;
    private Vector3 velocity;
    private bool isGrounded;
    [SerializeField]private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal > 0 || horizontal < 0)
        {
            animator.SetBool("moving" , true);
        } 
        else if (horizontal == 0)
        {
            animator.SetBool("moving", false);
        }
        isGrounded = Physics.CheckSphere(groundCheck1.position, groundCheckRadius, groundLayer) ||
                     Physics.CheckSphere(groundCheck2.position, groundCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        if (velocity.y < 0)
            velocity.y -= gravity * fallMultiplier * Time.deltaTime;
        else if (velocity.y > 0 && !Input.GetButton("Jump"))
            velocity.y -= gravity * lowJumpMultiplier * Time.deltaTime;
        else
            velocity.y -= gravity * Time.deltaTime;

        if (collectedKeyVisual != null)
        {
            Vector3 keyPosition = transform.position + new Vector3(keyOffset.x, keyOffset.y, 0f);
            keyPosition.x += (isFacingRight ? keyDistance : -keyDistance);
            keyPosition.z = collectedKeyVisual.position.z;
            collectedKeyVisual.position = keyPosition;
        }

        Flip();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(horizontal * speed, velocity.y, 0f);
        rb.linearVelocity = move;
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LevelKey"))
        {
            KeyItem keyItem = other.GetComponent<KeyItem>();
            if (keyItem != null && !collectedKeys.Contains(keyItem.keyType))
            {
                collectedKeys.Add(keyItem.keyType);
                collectedKeyVisual = other.transform;

                Rigidbody keyRb = collectedKeyVisual.GetComponent<Rigidbody>();
                if (keyRb != null) keyRb.isKinematic = true;

                Collider keyCollider = collectedKeyVisual.GetComponent<Collider>();
                if (keyCollider != null) keyCollider.enabled = false;
            }
        }

        if (other.CompareTag("DoorTrigger"))
        {
            DoorTrigger trigger = other.GetComponent<DoorTrigger>();
            if (trigger != null && collectedKeys.Contains(trigger.requiredKey))
            {
                trigger.ActivateDoor();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (groundCheck1 != null)
            Gizmos.DrawWireSphere(groundCheck1.position, groundCheckRadius);
        if (groundCheck2 != null)
            Gizmos.DrawWireSphere(groundCheck2.position, groundCheckRadius);
    }
}