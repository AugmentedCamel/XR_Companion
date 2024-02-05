using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloorObjects : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab for objects to spawn
    public float objectSize = 1.0f; // Serializable size of each object
    public int objectsPerRow = 10; // Target number of objects per row

    private Vector3 floorSize;

    void Start()
    {
        //SpawnObjects();
    }

    public void SpawnThisObjectOnFloor(GameObject prefab)
    {
        SpawnObjects(prefab);
    }   

    void SpawnObjects(GameObject prefab)
    {
        floorSize = GetComponent<Renderer>().bounds.size;
        Vector3 startPosition = transform.position - floorSize / 2 + new Vector3(objectSize / 2, 0, objectSize / 2);

        float xSpacing = floorSize.x / objectsPerRow;
        float zSpacing = floorSize.z / objectsPerRow;

        for (int i = 0; i < objectsPerRow; i++)
        {
            for (int j = 0; j < objectsPerRow; j++)
            {
                Vector3 spawnPosition = startPosition + new Vector3(xSpacing * i, 0, zSpacing * j);
                SpawnObjectAtPosition(spawnPosition, prefab);
            }
        }
    }

    void SpawnObjectAtPosition(Vector3 position, GameObject obj)
    {
        obj = Instantiate(objectPrefab, position, Quaternion.identity);
        obj.transform.localScale = new Vector3(objectSize, objectSize, objectSize); // Keep object size consistent
    }
}
