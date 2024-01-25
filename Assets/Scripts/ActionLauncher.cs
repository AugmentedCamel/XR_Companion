using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionLauncher : MonoBehaviour
{
    public bool TestButton;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LaunchActionSpawn()
    {
        SpawnAction spawnAction = GetComponentInChildren<SpawnAction>();
        spawnAction.SpawnAtIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (TestButton)
        {
            TestButton = false;
            LaunchActionSpawn();
        }
    }
}
