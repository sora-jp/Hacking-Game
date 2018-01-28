using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public Dictionary<ConnectionMode, LinkedList> connections;

    /// <summary>
    /// All variables below are set in the inspector
    /// </summary>
    public Node[] powerChilds;
    public Node powerParent;

    public int nodeID;

    /// <summary>
    /// Theese nodes are somehow contained in the nodes parents/children relations
    /// </summary>
    [HideInInspector] public List<Node> internetChildren = new List<Node>();
    [HideInInspector] public Node internetParent;

    private void Awake()
    {
        if (transform.parent.parent.GetComponent<Node>() != null)
        {
            internetParent = transform.parent.parent.GetComponent<Node>();
            (transform as RectTransform).pivot = new Vector2(1, 0);
        } else
        {
            (transform as RectTransform).pivot = new Vector2(1, 1);
        }

        foreach(Node n in transform.Find("Children").GetComponentsInChildren<Node>())
        {
            internetChildren.Add(n);
        }
    }

    public void MakeCurrentNode()
    {
        FindObjectOfType<Hackmap>().SetCurrentNode(this);
    }
}

/// <summary>
/// A custom linked list for nodes
/// </summary>
public class LinkedList {
    
}