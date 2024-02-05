
using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SetAnchorsToLighting : MonoBehaviour
{
    
    [SerializeField] List<GameObject> HueLamps;
    private List<Transform> AnchorLocation = new List<Transform>();
    public List<GameObject> FloorAnchors = new List<GameObject>();
    protected OVRSceneManager SceneManager { get; private set; }
    
    private void Awake()
    {
        SceneManager = GetComponent<OVRSceneManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SceneModelLoadedSuccessfully += OnSceneModelLoadedSuccesfully;  
    }

    private void OnSceneModelLoadedSuccesfully()
    {
        StartCoroutine(GetThePositionsOfAnchors());
        StartCoroutine(FindFloorAnchors());
    }

    private IEnumerator GetThePositionsOfAnchors()
    {
        yield return new WaitForEndOfFrame();

        LampAnchorComponent[] lampAnchorComponents = FindObjectsOfType<LampAnchorComponent>();
        foreach (LampAnchorComponent lampAnchorComponent in lampAnchorComponents)
        {
            AnchorLocation.Add(lampAnchorComponent.transform);
        }
        
    }

    private IEnumerator FindFloorAnchors()
    {
        yield return new WaitForEndOfFrame();

        SpawnFloorObjects[] SpawnFloorObjects = FindObjectsOfType<SpawnFloorObjects>();
        foreach (SpawnFloorObjects spawnFloorObjects in SpawnFloorObjects)
        {
            FloorAnchors.Add(spawnFloorObjects.gameObject);
        }

    }

    public void SpawnObjectsOnFloor(GameObject prefab)
    {
        foreach (GameObject floorAnchor in FloorAnchors)
        {
            floorAnchor.GetComponent<SpawnFloorObjects>().SpawnThisObjectOnFloor(prefab);
        }
    }


    public void SetLocationHueLamps()
    {
        for (int i = 0; i < HueLamps.Count; i++)
        {
            if(AnchorLocation.Count == HueLamps.Count)
            {
                HueLamps[i].transform.position = AnchorLocation[i].transform.position;
            }

        }


    }


    

}
