using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    public string LastHit;
    private GameObject LaserTarget;

    private GameObject CurrentTarget;
    
    // Start is called before the first frame update
    
    void Start()
    {
        LastHit = "none";
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
        LastHit = other.gameObject.name;
        CurrentTarget = other.gameObject;

        if (other.gameObject.tag == "HueLamp")
        {
            other.gameObject.GetComponent<HueLamp>().LightingOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "HueLamp")
        {
            other.gameObject.GetComponent<HueLamp>().LightingOff();
        }
    }

    public GameObject GetTarget()
    {
        return CurrentTarget;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
