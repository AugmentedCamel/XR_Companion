using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


    [RequireComponent(typeof(Grid), typeof(BoxCollider))]
public class RoomManager : MonoBehaviour
    {
    [Header("Dependencies")]
    [SerializeField] OVRSceneManager sceneManager;

    [Header("Settings")]
    [SerializeField] float paddingFromWall = .2f;
    [SerializeField] float paddingFromDeskEdges = .1f;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask furnitureLayer;
    [SerializeField] bool debugSpawnPoints = false;

    private OVRSceneRoom room;
    
    private BoxCollider roomBounds;
    private GameObject deskObject;
    public List <GameObject> deskObjects = new List<GameObject>();

    private Grid grid;
    private TransformFollower follower;

    private void Awake()
    {
        if (sceneManager == null)
            sceneManager = GameObject.FindObjectOfType<OVRSceneManager>();

        sceneManager.SceneModelLoadedSuccessfully += OnSceneLoaded;
        grid = GetComponent<Grid>();
        follower = GetComponent<TransformFollower>();
        roomBounds = GetComponent<BoxCollider>();
        roomBounds.isTrigger = true;

        
        
    }
    private void OnSceneLoaded()
    {
        room = GameObject.FindAnyObjectByType<OVRSceneRoom>();
        //room.gameObject.SetLayerRecursive("Room");
        

        //Setup collider for room to fit the bounds of the space (using collider as a Bounds type doesn't work with rotation)
        float height = room.Walls[0].Height;
        float width = room.Floor.Width;
        float depth = room.Floor.Height;
        roomBounds.size = new Vector3(width, depth, height);
        roomBounds.center = new Vector3(0, 0, height / 2f);
        follower.target = room.Floor.transform;

        FindDesksByComponent();
    }
    public bool IsInsideGuardian(Vector3 position)
    {
        position.y = 0;
        var result = OVRManager.boundary.TestPoint(position, OVRBoundary.BoundaryType.PlayArea);
        float dist = result.ClosestDistance;
        return dist <= .1f;
    }

    //Checks if the point is colliding with furniture using a Physics check box
    public bool IsInsideFurniture(Vector3 position)
    {
        bool isInFurniture = Physics.CheckBox(position, grid.cellSize, transform.rotation, furnitureLayer);
        bool isBelowFurniture = Physics.Raycast(position, Vector3.up, Mathf.Infinity, furnitureLayer);
        return isInFurniture || isBelowFurniture;
    }
    //Returns true if a point is facing in the forward direction (i.e. the normal) of the closest wall to that given point
    public bool IsPointInRoom(Vector3 point, float padding = 0)
    {
        float distance = DistanceToClosestWall(point, out Vector3 closestPoint, out OVRScenePlane closestWall);
        if (distance < padding || closestWall == null)
        {
            return false;
        }
        bool isInBoxCollider = roomBounds.ClosestPoint(point) == point;
        Vector3 dirToWall = (closestPoint - point).normalized;
        float dotProduct = Vector3.Dot(closestWall.transform.forward, dirToWall);
        return isInBoxCollider && dotProduct < -.5f;
    }

    //Calculate the closest point to any given wall in the OVRSceneRoom
    public float DistanceToClosestWall(Vector3 point, out Vector3 closestPoint, out OVRScenePlane closestWall)
    {

        float minDist = Mathf.Infinity;
        closestPoint = Vector3.zero;
        closestWall = null;
        if (room == null)
        {
            return minDist;
        }
        foreach (OVRScenePlane wall in room.Walls)
        {
            var col = wall.GetComponentInChildren<Collider>();
            Vector3 cp = col.ClosestPoint(point);
            float dist = Vector3.Distance(cp, point);
            if (dist < minDist)
            {
                minDist = dist;
                closestPoint = cp;
                closestWall = wall;
            }
        }
        return minDist;
    }
    //Returns the center position of the room
    public Vector3 CenterOfRoom()
    {
        Vector3 centerOfRoom = roomBounds.transform.TransformPoint(roomBounds.center);
        return centerOfRoom;
    }
    //Returns a list of positions on a grid that are within the room and not intersecting furniture
    public List<Vector3> GetAvailableSpawnLocations()
    {
        if (grid == null)
        {
            grid = GetComponent<Grid>();
        }
        List<Vector3> result = new List<Vector3>();

        int end = 25;
        int start = -25;

        for (int i = start; i < end; i++)
        {
            for (int j = start; j < end; j++)
            {
                Vector3Int cell = new Vector3Int(i, 0, j);
                Vector3 worldPos = grid.CellToWorld(cell);

                //Add a small offset to account for room movement
                Vector3 floorOffset = worldPos;
                floorOffset.y += .5f;

                bool isInRoom = IsPointInRoom(worldPos, paddingFromWall);
                bool isInFurniture = IsInsideFurniture(floorOffset);
                bool isInsideGuardian = IsInsideGuardian(worldPos);

                bool allowSpawn = isInRoom && !isInFurniture && isInsideGuardian;
                if (allowSpawn)
                {
                    result.Add(worldPos);
                }
            }
        }
        return result;
    }

    private void FindDesksByComponent()
    {
        DeskTracker[] desks = FindObjectsOfType<DeskTracker>();
        deskObjects.Clear(); // Clear the list to ensure it's fresh

        foreach (DeskTracker desk in desks)
        {
            deskObjects.Add(desk.gameObject);
        }

        if (deskObjects.Count == 0)
        {
            Debug.LogError("No desks with DeskTracker component found in the scene.");
        }
    }

    // This method calculates and returns a list of positions on the desk where objects can be spawned.
    public List<Vector3> GetDeskPositions()
    {
        if (grid == null || deskObjects.Count == 0)
        {
            Debug.LogError("Grid is not set or no desk objects found in the scene.");
            return new List<Vector3>();
        }

        List<Vector3> allDeskPositions = new List<Vector3>();

        foreach (var deskObject in deskObjects)
        {
            BoxCollider deskBounds = deskObject.GetComponent<BoxCollider>();
            if (deskBounds == null)
            {
                Debug.LogError("Desk object does not have a BoxCollider.");
                continue; // Proceed to the next deskObject in the loop
            }

            Vector3 deskSize = deskBounds.size;
            Vector3 deskCenter = deskBounds.center;
            Vector3 deskWorldPosition = deskObject.transform.position + deskCenter;

            float startX = (-deskSize.x / 2f) + paddingFromDeskEdges;
            float endX = (deskSize.x / 2f) - paddingFromDeskEdges;
            float startY = (-deskSize.z / 2f) + paddingFromDeskEdges;
            float endY = (deskSize.z / 2f) - paddingFromDeskEdges;
            float startZ = deskWorldPosition.y + deskSize.y / 2; //+ 0.05f;

            int gridSizeX = Mathf.FloorToInt(deskSize.x / grid.cellSize.x);
            int gridSizeY = Mathf.FloorToInt(deskSize.z / grid.cellSize.y);

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    // Apply the hexagonal offset by shifting every other row
                    float offsetX = (y % 2 == 0) ? 0 : grid.cellSize.x / 2;
                    // Ensure that the offset rows do not exceed the desk bounds
                    if (x == gridSizeX - 1 && offsetX != 0) continue;

                    Vector3 gridPosition = new Vector3(startX + offsetX + (x * grid.cellSize.x) + grid.cellSize.x / 2, startY + (y * grid.cellSize.y) + grid.cellSize.y / 2, startZ);
                    Vector3 worldPos = deskObject.transform.TransformPoint(gridPosition);

                    bool isAboveDesk = Physics.CheckBox(worldPos, grid.cellSize * 0.5f, Quaternion.identity, furnitureLayer);

                    if (!isAboveDesk)
                    {
                        allDeskPositions.Add(worldPos);
                    }
                }
            }
        }

        return allDeskPositions;
    }



    private void OnDrawGizmos()
    {
        if (!debugSpawnPoints) return;

        List<Vector3> availableLocations = GetAvailableSpawnLocations();
        Gizmos.color = Color.green;
        foreach (Vector3 location in availableLocations)
        {
            Gizmos.DrawCube(location, grid.cellSize * .9f);
        }
    }


}

