using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour
{
    public List<GameObject> spawnList = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnAtIndex(int index)
    {
 
        Instantiate(spawnList[index], transform.position, transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
