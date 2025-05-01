using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("IsWalking");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.ResetTrigger("IsWalking");
        }
        // -----------
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("IsWalking");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.ResetTrigger("IsWalking");
        }
        // -----------
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("IsWalking");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.ResetTrigger("IsWalking");
        }
        // -----------
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("IsWalking");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.ResetTrigger("IsWalking");
        }
    }
}
