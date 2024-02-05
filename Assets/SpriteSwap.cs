using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;
    [SerializeField] private float speedThreshold = 1.0f; // Speed threshold to activate/deactivate sprites
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        float speed = rb.velocity.magnitude; // Calculate the speed
        // Check the speed against the threshold and activate/deactivate sprites accordingly
        if (speed > speedThreshold)
        {
            sprites[0].SetActive(false);
            sprites[1].SetActive(true);
        }
        else
        {
            sprites[0].SetActive(true);
            sprites[1].SetActive(false);
        }
    }
}
