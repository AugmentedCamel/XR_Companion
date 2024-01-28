using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLighningBehaviour : MonoBehaviour
{
    public bool PowerLights = false;
    private PointerManager pointerManager;
    private HueLamp hueLamp;
    
    // Start is called before the first frame update
    void Start()
    {
        pointerManager = GetComponent<PointerManager>();
    }

    private void PowerOnTouch()
    {
        if (pointerManager.LastSelectedObjects.Count > 0)
        {
            foreach (GameObject obj in pointerManager.LastSelectedObjects)
            {
                hueLamp = obj.GetComponent<HueLamp>();
                hueLamp.LightingOn();

            }
        }
    }

    

    private IEnumerator lightingOff(HueLamp hueLamp)
    {
        hueLamp.LightingOff();
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

        if (PowerLights) { PowerOnTouch(); }
    }
}
