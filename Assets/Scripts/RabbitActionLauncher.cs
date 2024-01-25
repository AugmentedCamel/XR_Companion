using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RabbitActionLauncher : MonoBehaviour
{
    //this script is for the rabbit to launch the action
    [SerializeField] private TextMeshPro DebugText;
    
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
