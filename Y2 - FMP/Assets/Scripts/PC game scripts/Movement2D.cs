using UnityEngine;

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
    public bool hasKey = false;
    private Transform collectedKey;
    [SerializeField] private Vector3 keyOffset = new Vector3(0.5f, 1.2f, 0f);  // Distance from the player
    [SerializeField] private float keyDistance = 1.0f;  // Adjustable distance for key to follow player

    private Rigidbody rb;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Ground check using two positions
        isGrounded = Physics.CheckSphere(groundCheck1.position, groundCheckRadius, groundLayer) ||
                     Physics.CheckSphere(groundCheck2.position, groundCheckRadius, groundLayer);

        // Reset vertical velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        // Apply gravity
        if (velocity.y < 0)
        {
            velocity.y -= gravity * fallMultiplier * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump"))
        {
            velocity.y -= gravity * lowJumpMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        // Update key position if collected
        if (hasKey && collectedKey != null)
        {
            // Key follows the player but only updates the X and Y positions
            Vector3 keyPosition = transform.position + new Vector3(keyOffset.x, keyOffset.y, 0f);
            keyPosition.x += (isFacingRight ? keyDistance : -keyDistance);  // Adjust distance based on direction
            keyPosition.z = collectedKey.position.z;  // Maintain Z position

            collectedKey.position = keyPosition;
        }

        Flip();
    }

    void FixedUpdate()
    {
        // Apply horizontal movement + gravity
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
        if (!hasKey && other.CompareTag("LevelKey"))
        {
            hasKey = true;
            collectedKey = other.transform;

            // Optional: disable physics on the key
            Rigidbody keyRb = collectedKey.GetComponent<Rigidbody>();
            if (keyRb != null) keyRb.isKinematic = true;

            // Optional: disable collider so it doesn’t interfere
            Collider keyCollider = collectedKey.GetComponent<Collider>();
            if (keyCollider != null) keyCollider.enabled = false;
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
