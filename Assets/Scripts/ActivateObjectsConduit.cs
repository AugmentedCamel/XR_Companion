using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using System;

public class ActivateObjectsConduit : MonoBehaviour
{
    private RabbitActionLauncher actionLauncher;
    private bool state;
    // Start is called before the first frame update
    void Start()
    {
        actionLauncher = GetComponent<RabbitActionLauncher>();
    }

    private bool GetState(string state)
    {
        if (state == "on")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpecialAction(SpecialObject specialObject, bool state)
    {
        switch (specialObject)
        {
            case SpecialObject.headlight:
                if (state)
                {
                    actionLauncher.EnableHeadLight();
                }
                else
                {
                    actionLauncher.DisableHeadLight();
                }
                break;
            case SpecialObject.rabbiteye:
                if (state)
                {
                    actionLauncher.EnableRabbitEye();
                }
                else
                {
                    actionLauncher.DisableRabbitEyes();
                }
                break;
            case SpecialObject.laser:
                if (state)
                {
                    actionLauncher.EnableLaser();
                }
                else
                {
                    actionLauncher.DisableLaser();
                }
                break;
            case SpecialObject.hands:
                //actionLauncher.EnableHands();
                break;
            default:
                break;
        }
    }

    [MatchIntent("activate_object")]
    // Update is called once per frame
    public void ActivateObject(string[] info)
    {
        Debug.Log("Activate object activated");
        if (info != null)
        {
            if (info.Length > 0)
            {
                if (info.Length > 1)
                {
                    state = GetState(info[1]);
                }
                else
                {
                    state = true;
                }
                
                if (Enum.TryParse(info[0], out SpecialObject specialObject))
                {
                    Debug.Log($"asked to set {specialObject.ToString()} {state}");
                    SpecialAction(specialObject, state);
                }
                else
                {
                    string objectname = info[0];
                    Debug.Log(objectname);
                    actionLauncher.ActivateObject(objectname, state);
                }

            }
        }
        //get the gameobject name
    }
}
