using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Node[] all = FindObjectsOfType<Node>();
        int counter = 0;
        foreach(Node n in all){
            n.nodeId = counter;
            counter++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
