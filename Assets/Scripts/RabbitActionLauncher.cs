using Org.BouncyCastle.Security;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RabbitActionLauncher : MonoBehaviour
{
    //this script is for the rabbit to launch the action
    [SerializeField] private TextMeshPro DebugText;
    [SerializeField] private GameObject rabbitCasing;

    public UnityEvent TestEvent;
    public bool TestBool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnObject(GameObject ob)
    {
        if (ob != null)
        {
            Instantiate(ob, transform.position, transform.rotation);
        }
        else { Debug.Log("No object to spawn"); }

    }
    
    public void HueLightingOn()
    {
        DebugText.text = "Hue Lighting ON";
    }

    public void HueLightingOff()
    {
        DebugText.text = "hue Lighting OFF";
    }

    //------------------------------------------------//
    //SET COLOR OF THE RABBIT CASING
    public void SetColorCasing(Color color)
    {
        if (rabbitCasing)
        {
            rabbitCasing.GetComponent<Renderer>().material.color = color;
            Debug.Log("Color changed");
        }
    }

    public void SetColorObject(string objectname, Color color)
    {
        /*if (objectname == "rabbit")
        {
            SetColorCasing(color);
            return;
        }*/
        try { GameObject.Find(objectname).GetComponent<Renderer>().material.color = color; }
        catch { Debug.Log("Could not find object " + objectname); }
        

    }   

    public void TestColorCasing()
    {
        SetColorCasing(Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if(TestBool) { TestEvent.Invoke(); TestBool = false; }
    }
}
