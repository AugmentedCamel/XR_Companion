using UnityEngine;

public class RotateZAxis : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate around the Z-axis at the specified speed, while maintaining upright position
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, transform.rotation.eulerAngles.z);
    }
}
