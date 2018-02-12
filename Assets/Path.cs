using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    private Node2[] allNodes;
    public bool startSearch;
    public Node2 startN;

    List<Node2> currentpath = new List<Node2>();

	// Use this for initialization
	void Start () {
        allNodes = FindObjectsOfType<Node2>();
	}
	
	// Update is called once per frame
	void Update () {
        if (startSearch)
        {
            currentpath = search(startN);
            startSearch = false;
        }

        int counter = 0;
        foreach(Node2 p in currentpath)
        {
            if (counter == 0)
            {
                counter++;
                continue;
            }


            Debug.DrawLine(currentpath[counter].transform.position, currentpath[counter - 1].transform.position, Color.green, 1);

            counter++;
        }

	}


    public List<Node2> search(Node2 start)
    {
        List<Node2> openList = new List<Node2>();
        List<Node2> closedList = new List<Node2>();

        closedList.Add(start);
        while (true)
        {
            foreach (Node2 cNode in closedList)
            {
                foreach (Node2 n in allNodes)
                {
                    //Debug.Log("Searching");
                    RaycastHit hit = new RaycastHit();
                    Vector3 rayDirection = n.transform.position - cNode.transform.position;
                    Debug.DrawRay(cNode.transform.position, rayDirection.normalized, Color.red, 1);
                    if (Physics.Raycast(cNode.transform.position, rayDirection.normalized, out hit))
                    {
                        //Debug.Log("Hit REturned");
                        if (hit.transform.gameObject == n.gameObject)
                        {
                            if (!closedList.Contains(n))
                            {
                                if (!openList.Contains(n))
                                {
                                    openList.Add(hit.transform.gameObject.GetComponent<Node2>());
                                    n.parent = cNode;
                                }
                                else
                                {
                                    float currentDist = Vector3.Distance(n.transform.position, n.parent.transform.position);
                                    float checkDist = Vector3.Distance(n.transform.position, cNode.transform.position);
                                    if (checkDist < currentDist)
                                    {
                                        n.parent = cNode;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            float smallestFNum = -1;
            Node2 smallestFNode = null;
            foreach (Node2 oNode in openList)
            {
                if (oNode.end)
                {
                    Debug.Log("Found End", oNode);
                    List<Node2> path = new List<Node2>();
                    Node2 currentNode = oNode;
                    while (true)
                    {
                        path.Insert(0, currentNode);
                        if(currentNode.parent != null)
                        {
                            currentNode = currentNode.parent;
                        }
                        else
                        {
                            break;
                        }
                    }
                    return path;
                }
                float f = oNode.heuristic + Vector3.Distance(oNode.transform.position, oNode.parent.transform.position);
                if (smallestFNode == null)
                {
                    smallestFNum = f;
                    smallestFNode = oNode;
                    continue;
                }
                if (f < smallestFNum)
                {
                    smallestFNum = f;
                    smallestFNode = oNode;
                }
            }
            openList.Remove(smallestFNode);
            closedList.Add(smallestFNode);

            foreach (Node2 n in openList)
            {
                Debug.Log("Node: ", n);
            }
        }

        return null;
    }
}
