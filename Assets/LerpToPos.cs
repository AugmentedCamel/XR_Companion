using UnityEngine;

public class PositionLerperWithDynamicStart : MonoBehaviour
{
    public float lerpSpeed = 1.0f;
    private Material objectMaterial;
    [SerializeField] private Transform transformFollowerStart;
    [SerializeField] private Transform transformFollowerEnd;
    private bool isLerping = false;
    private bool lerpingToEnd = true; // True when lerping to end, false when lerping to start

    void Start()
    {
        InitializeMaterial();
    }

    void Update()
    {
        if (isLerping)
        {
            PerformLerping();
        }
    }

    public void StartLerpingToEnd()
    {
        isLerping = true;
        lerpingToEnd = false;
    }

    public void StartLerpingToStart()
    {
        isLerping = true;
        lerpingToEnd = true;
    }

    private void PerformLerping()
    {
        Vector3 startPosition = transformFollowerStart.position;
        Vector3 endPosition = transformFollowerEnd.position;
        Vector3 targetPosition = lerpingToEnd ? endPosition : startPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

        // Adjust opacity based on distance
        AdjustOpacity(startPosition, endPosition, targetPosition);

        // Stop lerping when close to the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isLerping = false;
        }
    }

    private void InitializeMaterial()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            objectMaterial = renderer.material;
        }
        else
        {
            Debug.LogWarning("Renderer not found on the GameObject. Opacity changes will not be applied.");
        }
    }

    private void AdjustOpacity(Vector3 startPosition, Vector3 endPosition, Vector3 targetPosition)
    {
        if (objectMaterial != null)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            float totalDistance = Vector3.Distance(startPosition, endPosition);
            float opacity = Mathf.Clamp01(distance / totalDistance) * 0.9f;
            opacity = lerpingToEnd ? 0.9f - opacity : opacity; // Invert opacity calculation based on direction
            Color color = objectMaterial.color;
            color.a = opacity;
            objectMaterial.color = color;
        }
    }
}
