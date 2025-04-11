using UnityEngine;

public class KeyFloat : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("GameObject to act as the center point for up-down movement.")]
    public Transform centerPoint;

    [Tooltip("Distance to move up and down from the center.")]
    public float verticalRange = 2f;

    [Tooltip("Speed of movement.")]
    public float speed = 2f;

    [Tooltip("Use local space instead of world space.")]
    public bool useLocalPosition = false;

    private float timer;

    void Update()
    {
        if (centerPoint == null)
        {
            Debug.LogWarning("CenterPoint is not assigned.");
            return;
        }

        timer += Time.deltaTime * speed;

        // Vertical offset using sine wave
        float offset = Mathf.Sin(timer) * verticalRange;

        Vector3 basePosition = useLocalPosition ? centerPoint.localPosition : centerPoint.position;
        Vector3 newPosition = basePosition + new Vector3(0, offset, 0);

        if (useLocalPosition)
            transform.localPosition = newPosition;
        else
            transform.position = newPosition;
    }
}