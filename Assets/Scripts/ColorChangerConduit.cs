using System;
using Meta.Voice.Samples.ShapesConduit;
using Meta.Voice.Samples.WitShapes;
using Meta.WitAi;
using UnityEngine;

public class ColorChangerConduit : MonoBehaviour
{
    [SerializeField] RabbitActionLauncher rabbitActionLauncher;
    
    private void SetColorObject(string objectname, UnityEngine.Color color)
    {
        rabbitActionLauncher.SetColorObject(objectname, color);
    }

    [MatchIntent("set_color")]
    public void SetColor(String[] info)
    {
        Debug.Log("Set color activated");
        //get the gameobject name
        if (info != null)
        {
            if (info.Length > 0)
            {
                string objectname = info[0];
                Debug.Log(objectname);
                if(info.Length > 1 && ColorUtility.TryParseHtmlString(info[1], out UnityEngine.Color color))
                {
                    Debug.Log("set color " + color.ToString() + " on object " + objectname);
                    SetColorObject(objectname, color);
                }

            }
        }
    }
}
