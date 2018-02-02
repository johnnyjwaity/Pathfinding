using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {


    public List<Node> parentNodes = new List<Node>();
    public List<Node> childrenNodes = new List<Node>();
    public int nodeId;
    public bool isStartNode;
    public bool isFinishNode;
    public bool search;

	// Use this for initialization
	void Start () {
        if(isStartNode){
            scan();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (search)
        {
            foreach(List<Node> path in foundFinish())
            {
                Debug.Log("START NEW PATH");
                foreach(Node n in path)
                {
                    Debug.Log("Path", n);
                }
            }
            search = false;
        }
	}


    public void scan(){
        Node[] allNodes = FindObjectsOfType<Node>();
        //Debug.Log("All Nodes Length" + allNodes.Length);
        List<Node> sightedNodes = new List<Node>();
        foreach(Node cNode in allNodes){
            bool isParent = false;
            foreach(Node pNode in parentNodes){
                if(cNode.nodeId == pNode.nodeId){
                    isParent = true;
                    break;
                }
            }
            if (isParent || cNode.nodeId == nodeId){
                continue;
            }
            Vector3 rayDirection = cNode.transform.position - transform.position;
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(transform.position, rayDirection.normalized, out hit)){
                if(hit.transform.gameObject == cNode.gameObject)
                {
                    //Debug.Log("Found Node In Sight", cNode);
                    sightedNodes.Add(cNode);
                    childrenNodes.Add(cNode);
                    List<Node> newParents = new List<Node>();
                    foreach(Node pNode in parentNodes)
                    {
                        newParents.Add(pNode);
                    }
                    newParents.Add(this);
                    cNode.parentNodes = newParents;
                }
            }
            
        }
        foreach(Node sNode in sightedNodes){
            sNode.scan();
        }
    }


    public List<List<Node>> foundFinish()
    {
        List<List<Node>> paths = new List<List<Node>>();
        foreach (Node cNode in childrenNodes)
        {
            List<Node> path = new List<Node>();
            Debug.Log("Scanning Node: ", this);
            if (cNode.isFinishNode)
            {
                path.Add(cNode);
                path.Insert(0, this);

                paths.Add(path);
            }
            else
            {
                List<List<Node>> tempPaths = cNode.foundFinish();
                foreach(List<Node> tempPath in tempPaths)
                {
                    
                    if (tempPath.Count > 0)
                    {
                        tempPath.Insert(0, this);
                        paths.Add(tempPath);
                    }
                }
                
            }
        }




        return paths;
    }




    //public List<Node> foundFinish()
    //{
    //    List<Node> path = new List<Node>();
    //    foreach(Node cNode in childrenNodes)
    //    {
    //        Debug.Log("Scanning Node: ", this);
    //        if (cNode.isFinishNode)
    //        {
    //            path.Add(cNode);
    //            path.Insert(0, this);
                
    //            return path;
    //        }
    //        List<Node> tempPath = cNode.foundFinish();
    //        if(tempPath.Count > 0)
    //        {
    //            tempPath.Insert(0, this);
    //            return tempPath;
    //        }


    //    }




    //    return path;
    //}


}
