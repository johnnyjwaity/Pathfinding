using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node2 : MonoBehaviour {
    public float heuristic;
    public bool end;
    public Node2 parent;
	// Use this for initialization
	void Start () {
        calculateHeuristic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    


    private void calculateHeuristic()
    {
        Node2[] allNodes = FindObjectsOfType<Node2>();
        Node2 finishedNode = null;
        foreach(Node2 n in allNodes)
        {
            if (n.end)
            {
                finishedNode = n;
                break;
            }
        }
        heuristic = Mathf.Abs(transform.position.x - finishedNode.transform.position.x) + Mathf.Abs(transform.position.z - finishedNode.transform.position.z);
    }

}
