using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro PointedObjectText;
    [SerializeField] private LaserCollider LaserCollider;
    public int MaxLastSelectedObjects = 2;

    public List<GameObject> LastSelectedObjects = new List<GameObject>();
    public GameObject PointedObject;
    private GameObject lastPointedObject;

    // Start is called before the first frame update
    void Start()
    {
        PointedObjectText.text = "laseractivated";
        lastPointedObject = null;
    }

    public void SelectCurrentTarget()
    {
        PointedObject = LaserCollider.GetTarget();
        
        if (PointedObject)
        {
            PointedObjectText.text = $"Selected target {PointedObject.name}";
            if (!lastPointedObject)
            {
                AddToLastSelectedList(PointedObject);
                lastPointedObject = PointedObject;
            }

            if (lastPointedObject && !(lastPointedObject == PointedObject))
            {
                lastPointedObject = PointedObject;
                //new object is selected, should add to list
                AddToLastSelectedList(PointedObject);
                
            }
        }
    }

    private void AddToLastSelectedList(GameObject selectedObj)
    {
        //check if object is not already in list
        if (!LastSelectedObjects.Contains(selectedObj))
        {
            LastSelectedObjects.Add(selectedObj);
            PointedObjectText.text = "added selected object to last selected";
        }

        //delete the objects from list if they exceed maximum
        if(LastSelectedObjects.Count > MaxLastSelectedObjects)
        {
            
            LastSelectedObjects.RemoveAt(0);
        }


    }



    // Update is called once per frame
    void Update()
    {
        if (PointedObject != null)
        {
            PointedObjectText.text = PointedObject.name;
        }
        
    }
}
