using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {


    public List<Node> parentNodes = new List<Node>();
    public List<Node> childrenNodes = new List<Node>();

    public bool isStartNode;


	// Use this for initialization
	void Start () {
        if(isStartNode){
            scan();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void scan(){
        Node[] allNodes = FindObjectsOfType<Node>();
        List<Node> sightedNodes = new List<Node>();
        foreach(Node cNode in allNodes){
            bool isParentOrSelf = false;
            foreach(Node pNode in parentNodes){
                if(cNode.Equals(pNode) || cNode.Equals(this)){
                    isParentOrSelf = true;
                    break;
                }
            }
            if (isParentOrSelf){
                continue;
            }

            if(!Physics.Linecast(transform.position, cNode.transform.position)){
                sightedNodes.Add(cNode);
                childrenNodes.Add(cNode);
                cNode.parentNodes.Add(this);

            }
        }
        foreach(Node sNode in sightedNodes){
            sNode.scan();
        }
    }


}
