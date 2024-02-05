using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointCollectorManager : MonoBehaviour
{
    [SerializeField] private SuctionCollider SuctionCollider;
    [SerializeField] private TextMeshPro coinScoreText;
    private int coinScore;

    [SerializeField] TransformFollower handFollower;
    
    // Start is called before the first frame update


    void Start()
    {
        coinScoreText = GetComponentInChildren<TextMeshPro>();
        SuctionCollider = GetComponentInChildren<SuctionCollider>();

        coinScore = 0;
        coinScoreText.text = coinScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = handFollower.transform.position;
        coinScore = SuctionCollider.affectedBodies.Count;
        coinScoreText.text = coinScore.ToString();
    }

    public void DeleteList()
    {
        foreach (Rigidbody obj in SuctionCollider.affectedBodies) { Destroy(obj); }
        
        SuctionCollider.affectedBodies.Clear();
    }
}
