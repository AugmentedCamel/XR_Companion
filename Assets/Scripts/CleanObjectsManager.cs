using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanObjectsManager : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private PointCollectorManager pointCollectorManager;
    [SerializeField] private GameObject SpawnObjectPrefab;

    [SerializeField] private List<GameObject> spawnObjects = new List<GameObject>();

    public List<GameObject> SpawnedObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnObjectsOnFloor()
    {
        //get list of spawnable positions
        List<Vector3> spawnablePos = roomManager.GetAvailableSpawnLocations();

        foreach (Vector3 pos in spawnablePos) { SpawnObjectAt(pos, SpawnObjectPrefab); }
        Debug.Log("spawned Objects on floor");
        //gets the Anchor lighting script and spawns an object on all floor objects
        //sceneAnchors.SpawnObjectsOnFloor(SpawnObjectPrefab);
        //Debug.Log("spawned Objects on floor");
    }

    private Vector3 RandomOffsetPos(Vector3 pos, Vector3 cellSize, float offsetPercentage = 5f)
    {
        // Calculate the maximum offset based on the percentage of the cell size
        float offsetX = Random.Range(-cellSize.x * offsetPercentage, cellSize.x * offsetPercentage);
        float offsetY = Random.Range(-cellSize.y * offsetPercentage, cellSize.y * offsetPercentage); // Assuming vertical variation is desired
        float offsetZ = Random.Range(-cellSize.z * offsetPercentage, cellSize.z * offsetPercentage);

        // Apply the offset to the original position
        Vector3 randomizedPos = pos + new Vector3(offsetX, offsetY, offsetZ);

        return randomizedPos;
    }

    public void SpawnObjectsOnDesk()
    {
        pointCollectorManager.DeleteList();

        List<Vector3> spawnablePos = roomManager.GetDeskPositions();
        Grid grid = roomManager.GetComponent<Grid>();
        Vector3 cellSize = grid.cellSize;

        foreach (Vector3 pos in spawnablePos)
        {
            Vector3 randomizedPos = RandomOffsetPos(pos, cellSize, 0.5f); // Use 10% of cell size as offset
            SpawnObjectAt(randomizedPos, RandomizedPrefab()); // Assuming SpawnObjectAt is a method you have defined
        }
        Debug.Log("Spawned objects on desk");
    }

    private GameObject RandomizedPrefab()
    {
        //return SpawnObjectPrefab;
        return spawnObjects[Random.Range(0, spawnObjects.Count)];
    }

    private void SpawnObjectAt(Vector3 pos, GameObject ob)
    {
        if (ob != null)
        {
            Instantiate(ob, pos, Quaternion.identity);
            SpawnedObjects.Add(ob);
            //StartAnimation(SpawnedObjects.Count); //on every added object, start the animation
        }
        else { Debug.Log("No object to spawn"); }
    }

    private void StartAnimation(int index)
    {
        if (SpawnedObjects[index].GetComponent<ParticleBehaviour>())
        {
            SpawnedObjects[index].GetComponent<ParticleBehaviour>().SetAnimatorToRandomPosition();
        }
    }
    
}
