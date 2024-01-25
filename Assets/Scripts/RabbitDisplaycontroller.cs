using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitDisplaycontroller : MonoBehaviour
{
    
    [SerializeField] private GameObject displayText;
    private TextFade textComponent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        textComponent = displayText.GetComponent<TextFade>();
        OnIdle();

    }

    public void OnListening()
    {
        if (displayText != null)
        {
            string text = "Listening..";
            textComponent.SetText(text, true);
        }
        
    }

    public void OnActionFound()
    {
        if (displayText != null)
        {
            string text = "Action found";
            textComponent.SetText(text, false);
        }
    }

    public void OnIdle()
    {
        if (displayText != null)
        {
            string text = string.Empty;
            textComponent.SetText(text, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
