using UnityEngine;
using System.Collections;

public class ObjectScaler : MonoBehaviour
{
    [Header("Target Object")]
    public Transform targetObject;

    [Header("Scaling Settings")]
    public Vector3 startScale = Vector3.one;
    public Vector3 midScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 finalScale = new Vector3(0.33f, 0.33f, 0.33f);

    [Header("Timing Settings")]
    public float firstScaleDuration = 2f;
    public float delayBeforeSecondScale = 1f;
    public float secondScaleDuration = 2f;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target Object is not assigned.");
            return;
        }

        targetObject.localScale = startScale;
        StartCoroutine(ScaleRoutine());
    }

    IEnumerator ScaleRoutine()
    {
        // Scale to midScale
        yield return StartCoroutine(ScaleTo(targetObject, midScale, firstScaleDuration));

        // Wait before next scale
        yield return new WaitForSeconds(delayBeforeSecondScale);

        // Scale to finalScale
        yield return StartCoroutine(ScaleTo(targetObject, finalScale, secondScaleDuration));
    }

    IEnumerator ScaleTo(Transform obj, Vector3 target, float duration)
    {
        Vector3 initialScale = obj.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.localScale = Vector3.Lerp(initialScale, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.localScale = target;
    }
}