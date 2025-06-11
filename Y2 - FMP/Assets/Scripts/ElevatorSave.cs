using UnityEngine;

public class ElevatorSave : MonoBehaviour
{
    [Header("Animator Setup")]
    [Tooltip("The Animator component to watch.")]
    public Animator animator;

    [Tooltip("Name of the animation state to watch.")]
    public string targetStateName;

    [Tooltip("Layer index where the animation state resides.")]
    public int layerIndex = 0;

    [Header("Target Object")]
    [Tooltip("The GameObject to set inactive when the animation state is active.")]
    public GameObject targetObject;

    [Header("Timing")]
    [Tooltip("Delay (in seconds) after state becomes active before deactivating the object.")]
    public float delayBeforeDeactivate = 1f;

    private bool isWaiting = false;

    void Update()
    {
        if (animator == null || targetObject == null || string.IsNullOrEmpty(targetStateName))
            return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);

        if (stateInfo.IsName(targetStateName) && !isWaiting)
        {
            StartCoroutine(DeactivateAfterDelay());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitElevator") && targetObject != null)
        {
            Debug.Log("hitbox should be active");
            targetObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ExitElevator"))
        {
            Debug.Log("hitbox should be active");
            targetObject.SetActive(true);
        }
    }
    private System.Collections.IEnumerator DeactivateAfterDelay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delayBeforeDeactivate);

        // Double-check state is still active before deactivating
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        if (stateInfo.IsName(targetStateName))
        {
            targetObject.SetActive(false);
        }

        isWaiting = false; // Reset for future reactivation, if needed
    }
}