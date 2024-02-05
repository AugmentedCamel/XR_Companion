using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cleanerBehaviour : MonoBehaviour
{
    private SuctionCollider suctionCollider;
    private void Start()
    {
        suctionCollider = GetComponentInChildren<SuctionCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bacteria"))
        {
            
            other.gameObject.SetActive(false);
        }
    }
}
