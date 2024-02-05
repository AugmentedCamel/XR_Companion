using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIButton : MonoBehaviour
{
    //this script starts unity events when button is pressed and when it is released
    public UnityEvent OnButtonUIPressed;
    public UnityEvent OnButtonUIReleased;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thumb")
        {
            Debug.Log("Button Pressed");
            //start unity event
            //testText.text = "Button Pressed";
            OnButtonUIPressed.Invoke();
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Thumb")
        {
            //Debug.Log("Button Released");
            //start unity event
            
            OnButtonUIReleased.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
