using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravitateTowards : MonoBehaviour
{
    public float strengthOfAttraction = 5.0f; // The strength of the attraction
    public float effectiveRange = 50f; // Effective range of attraction

    private Rigidbody rb; // The Rigidbody of the current object
    private GameObject targetObject; // The object to gravitate towards, assumed to be the one with the SuctionCollider

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetObject = FindObjectOfType<SuctionCollider>()?.gameObject; // Automatically find the SuctionCollider in the scene
    }

    void FixedUpdate()
    {
        if (targetObject != null)
        {
            Vector3 directionToTarget = targetObject.transform.position - transform.position;
            float distance = directionToTarget.magnitude;

            // Only apply force if within effective range
            if (distance <= effectiveRange)
            {
                // Normalize the direction and calculate force magnitude
                Vector3 forceDirection = directionToTarget.normalized;
                float forceMagnitude = Mathf.Clamp(strengthOfAttraction / Mathf.Pow(distance, 2), 0, strengthOfAttraction);

                // Apply the force towards the target
                rb.AddForce(-forceDirection * forceMagnitude, ForceMode.Acceleration);
            }
        }
    }

    // Example of finding any object by type, refactored to a more specific use case here
    T FindAnyObjectByType<T>() where T : Component
    {
        return FindObjectOfType<T>();
    }
}
