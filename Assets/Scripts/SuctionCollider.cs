using UnityEngine;
using System.Collections.Generic;

public class SuctionCollider : MonoBehaviour
{
    public float strengthOfAttraction = 5.0f; // The strength of the attraction
    public List<Rigidbody> affectedBodies = new List<Rigidbody>();

    void FixedUpdate()
    {
        foreach (var rb in affectedBodies)
        {
            Vector3 forceDirection = (transform.position - rb.position).normalized;
            float distance = Vector3.Distance(transform.position, rb.position);
            float forceMagnitude = Mathf.Clamp(strengthOfAttraction / Mathf.Pow(distance, 2), 0, strengthOfAttraction);

            rb.AddForce(forceDirection * forceMagnitude, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.gameObject.CompareTag("Bacteria"))
        {
            affectedBodies.Add(other.GetComponent<Rigidbody>());

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            affectedBodies.Remove(other.GetComponent<Rigidbody>());
        }
    }
}
