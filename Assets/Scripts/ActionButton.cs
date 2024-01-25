using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour
{

    //[SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private TextMeshPro debugText;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particlesListening;
    [SerializeField] public UnityEvent OnButtonActive;
    [SerializeField] public UnityEvent OnButtonRelease;
    
    [SerializeField] private GameObject RabbitDisplay;
    private RabbitDisplaycontroller rabbitDisplayController;
    

    private bool IsListening = false;

    public bool buttonTestTriggerEnter = false;
    public bool buttonTestTriggerExit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        debugText.text = "Button Not Pressed";
        //animator = GetComponent<Animator>();
        if (animator != null ) { Debug.Log("Animator found"); }
        else { Debug.Log("Animator not found"); }

        rabbitDisplayController = RabbitDisplay.GetComponent<RabbitDisplaycontroller>();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thumb")
        {
            IsListening = true;
            Debug.Log("Button Pressed");
            RabbitListening();

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Thumb")
        {
            IsListening = false;
            Debug.Log("Button Not Pressed");
            RabbitIdling();
        }
    }

    private void RabbitListening()
    {
        OnButtonActive.Invoke();
        Debug.Log("Rabbit Listening");
        animator.SetBool("ListeningBool", true);
        particlesListening.Play();
        rabbitDisplayController.OnListening();


    }

    private void RabbitIdling()
    {
        OnButtonRelease.Invoke();
        Debug.Log("Rabbit Idling");
        animator.SetBool("ListeningBool", false);
        particlesListening.Stop();
        rabbitDisplayController.OnIdle();
    }
    
    
   

    // Update is called once per frame
    void Update()
    {
        // this is for testing purposes
        if (buttonTestTriggerEnter) 
        {
            
            IsListening = true;
            buttonTestTriggerEnter = false;
            RabbitListening();
        }
        if (buttonTestTriggerExit)
        {
            IsListening=false;
            buttonTestTriggerExit = false;
            RabbitIdling();
        }
        
        /*if(IsListening)
        {
            particlesListening.Play();
            debugText.text = "IsListening is true";
        }
        else
        {
            particlesListening.Stop();
            debugText.text = "IsListening is false";
        }*/

    }
}
