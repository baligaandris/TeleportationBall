using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {

    public int ballNumber;
    public GameObject[] nodes;
    private int nextNodeIndex;
    public int startingNode;
    private int layerOffSet = 1;

	// Use this for initialization
	void Start () {
        nextNodeIndex = startingNode;
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<SpriteRenderer>().sortingOrder = transform.root.GetComponent<SpriteRenderer>().sortingOrder + layerOffSet;

        transform.position = Vector3.MoveTowards(transform.position, nodes[nextNodeIndex].transform.position, 1*Time.deltaTime);
        if (transform.position == nodes[nextNodeIndex].transform.position)
        {
            SetLayerOffset(nodes[nextNodeIndex].GetComponent<EnergyBallLayerManager>().layerOffSet);
            if (nextNodeIndex<nodes.Length-1)
            {
                nextNodeIndex++;
            }
            else
            {
                nextNodeIndex = 0;
            }
        }

        if (transform.root.GetComponent<PlayerMovement>().GetTeleportsLeft() >= ballNumber)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetLayerOffset(int os) {
        layerOffSet = os;
    }
}
