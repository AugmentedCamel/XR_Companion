using System;
using Meta.Voice.Samples.ShapesConduit;
using Meta.Voice.Samples.WitShapes;
using Meta.WitAi;
using UnityEngine;

public class LightningChangerConduit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private HueLamp huelamp;

    private void SetLightningState(bool state, UnityEngine.Color color)
    {
        
        huelamp.LightingOn();
        huelamp.SetLightColor(color);

    }

    private void SetLightningOff()
    {
        huelamp.LightingOff();
    }

    [MatchIntent("change_lightning")]
    public void ChangeLightning(String[] info)
    {
        Debug.Log(info.ToString());
        if(info.Length > 0 && Enum.TryParse(info[0], out LightStates state))
        {
            Debug.Log("Parsed lightstate");
            
            if(state == LightStates.off) { SetLightningOff(); return; }
            
            if(info.Length > 1 && ColorUtility.TryParseHtmlString(info[1], out UnityEngine.Color color))
            {
                Debug.Log("Reached the end man");
                SetLightningState(true, color);
                return;
            }
            
            //this point only happens if the color is not specified
           
            
            SetLightningState(true, UnityEngine.Color.white);
        }
        else
        {
            Debug.Log("Could not parse lightstate");
        }
        
        
        
    }

    public void TestChangeColor()
    {
        SetLightningState(true, UnityEngine.Color.red);
        Debug.Log("Tested lightstate");
    }


}



