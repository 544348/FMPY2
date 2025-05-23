using System.Collections.Generic;
using System.Collections;
using TMPro;
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

    [Header("Coin Collection")]
    public int totalCoinsCollected = 0;
    private int totalCoins = 0;
    public int coinsCollectedCounter;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI coinTextCounter;
    public GameObject[] levels;
    private int coinsForLevel1 = 0;
    private int coinsForLevel2 = 0;
    private int coinsForLevel3 = 0;
    private bool countedLevelOneCoins = false;
    private bool countedLevelTwoCoins = false;
    private bool countedLevelThreeCoins = false;

    private Rigidbody rb;
    public Vector3 velocity;
    private bool isGrounded;
    [SerializeField]private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        coinText.text = "Coins:" + totalCoinsCollected.ToString();
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

    public IEnumerator CoinCountUp(GameObject levelEndTrigger)
    {
        int totalCoinsOverAllLevels = 0;
        for (int coins = 0; coins <= totalCoinsCollected; coins++)
        {
            coinTextCounter.text = "Level coins collected:" + coins + "/" + totalCoins;
            yield return new WaitForSeconds(1f);
        }
        if (!levels[2].activeSelf && !countedLevelThreeCoins && countedLevelOneCoins && countedLevelTwoCoins)
        {
            Debug.Log("third counter");
            coinsForLevel3 = totalCoinsCollected;
            totalCoinsCollected = 0;
            countedLevelThreeCoins = true;
        }
        if (!levels[1].activeSelf && !countedLevelTwoCoins && countedLevelOneCoins)
        {
            Debug.Log("second counter");
            coinsForLevel2 = totalCoinsCollected;
            totalCoinsCollected = 0;
            countedLevelTwoCoins = true;
        }
        if (!levels[0].activeSelf && !countedLevelOneCoins)
        {
            Debug.Log("first counter");
            coinsForLevel1 = totalCoinsCollected;
            totalCoinsCollected = 0;
            countedLevelOneCoins = true;
        }
        if (coinsForLevel1 <0 && coinsForLevel2 <0 && coinsForLevel3 <0)
        {
            totalCoinsOverAllLevels = coinsForLevel1 + coinsForLevel2 + coinsForLevel3;
        }
        yield return new WaitForSeconds(1f);
        levelEndTrigger.GetComponent<LevelEndTrigger>().nextLevel.SetActive(true);
        levelEndTrigger.GetComponent<LevelEndTrigger>().objectToActivate.SetActive(false);
        velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
        velocity = Vector3.zero;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        
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
        if (other.CompareTag("Coin"))
        {
            totalCoinsCollected++;
            Destroy(other.gameObject);
            coinText.text = "Coins:" + totalCoinsCollected.ToString();
        }

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