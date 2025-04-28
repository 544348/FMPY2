using UnityEngine;

public class DeleteWorldSpaceScreenCanvas : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Drag the root Canvas or parent object you want to monitor/delete.")]
    public GameObject rootObject;

    [Header("Timing Settings")]
    [Tooltip("Delay in seconds before deletion starts after activation.")]
    public float delayBeforeDelete = 3f;

    [Tooltip("Also destroy all child objects (TMP, images, etc)?")]
    public bool deleteChildren = true;

    private bool hasStarted = false;

    void Update()
    {
        if (!hasStarted && rootObject != null && rootObject.activeInHierarchy)
        {
            hasStarted = true;
            StartCoroutine(DeleteAfterDelay());
        }
    }

    private System.Collections.IEnumerator DeleteAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDelete);

        if (rootObject != null)
        {
            if (deleteChildren)
            {
                foreach (Transform child in rootObject.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            Destroy(rootObject);
        }
    }
}