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

    public string NodeID;

    /// <summary>
    /// Theese nodes are somehow contained in the nodes parents/children relations
    /// </summary>
    public List<Node> internetChildren = new List<Node>();
    public Node internetParent;

    public LineRenderer lineRenderer;

    private void Awake()
    {
        if (transform.parent.parent.GetComponent<Node>() != null)
        {
            internetParent = transform.parent.parent.GetComponent<Node>();
        } 

        foreach(Node n in transform.Find("Children").GetComponentsInChildren<Node>())
        {
            internetChildren.Add(n);
        }

        if (internetChildren.Count == 0)
        {
            (transform as RectTransform).pivot = new Vector2(2, -1);
        } else
        {
            (transform as RectTransform).pivot = new Vector2(0, 1);
        }

        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        DrawLinesToChildren();
        Debug.Log("Drawn lines");
    }

    public void MakeCurrentNode()
    {
        FindObjectOfType<Hackmap>().SetCurrentNode(this);
    }

    public void DrawLinesToChildren()
    {
        foreach(Node node in internetChildren)
        {
            DrawLineToChild(node);
        }
    }

    public void DrawLineToChild(Node node)
    {
        node.lineRenderer.SetPositions(LineHelper.GetEasedLine(transform.position, node.transform.position, 10, 1));
    }
}

/// <summary>
/// A custom linked list for nodes
/// </summary>
public class LinkedList {
    
}