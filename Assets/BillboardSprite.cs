using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        // Find the main camera's transform
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Rotate your sprite to face the camera.
        // This assumes the camera is never null. You might want to add checks if your game can disable the main camera.
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
                         cameraTransform.rotation * Vector3.up);
    }
}
