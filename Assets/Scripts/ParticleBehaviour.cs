using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    void Start()
    {
        // Check if object spawned in desk collider
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f); // Use a small radius for the object's position
        bool foundDesk = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Desk")) // Checking if any of the hit colliders is tagged as Desk
            {
                foundDesk = true;
                break; // Exit the loop if we've found our desk
            }
        }

        // If not inside a desk collider, set active to false
        if (!foundDesk)
        {
            gameObject.SetActive(false);
        }
        else
        {
            // Properly initialize the animator reference
            m_Animator = GetComponentInChildren<Animator>(); // Assuming the Animator is in a child object
            SetAnimatorToRandomPosition();
        }
    }

    public void SetAnimatorToRandomPosition()
    {
        if (m_Animator == null)
        {
            Debug.Log("Animator not found on the particle object.");
            return;
        }

        // Assume "Particle" is the name of your animation state
        // Generate a random normalized time
        float randomNormalizedTime = Random.Range(0.0f, 1.0f);

        // Play the animation from a random position
        m_Animator.Play("Coin", 0, randomNormalizedTime);
    }

    // Check if the particle has left desk collider
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "DeskArea" || other.CompareTag("Desk"))
        {
            gameObject.SetActive(false);
        }
    }
}
