using Q42.HueApi.Interfaces;
using Q42.HueApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HueSettings : MonoBehaviour
{
    [SerializeField]
    string appKey;

    [SerializeField]
    string appName;

    [SerializeField]
    string deviceName;

    public string AppKey { get => appKey; set => appKey = value; }

    public string AppName { get => appName; set => appName = value; }

    public string DeviceName { get => deviceName; set => deviceName = value; }

    
}


