
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
