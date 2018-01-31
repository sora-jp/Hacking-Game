using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultNode : Node {

    public Dictionary<ConnectionMode, LinkedList> connections;

    /// <summary>
    /// All variables below are set in the inspector
    /// </summary>
    public DefaultNode[] powerChilds;
    public DefaultNode powerParent;

    public DefaultNode parent;
    public DefaultNode[] children;

    public Vector3 ChildLineAdjustment;
    public Vector3 ParentLineAdjustment;

    public string NodeID;

    /// <summary>
    /// Theese nodes are somehow contained in the nodes parents/children relations
    /// </summary>
    public List<DefaultNode> internetChildren = new List<DefaultNode>();
    public DefaultNode internetParent;

    public LineRenderer lineRenderer;

    public override T GetParent<T>()
    {
        return parent as T;
    }
    public override T[] GetChild<T>()
    {
        return children as T[];
    }

    private void Awake()
    {
        if (transform.parent.parent.GetComponent<DefaultNode>() != null)
        {
            internetParent = transform.parent.parent.GetComponent<DefaultNode>();
        } 

        foreach(DefaultNode n in transform.Find("Children").GetComponentsInChildren<DefaultNode>())
        {
            internetChildren.Add(n);
        }

        if (internetChildren.Count != 0)
        {
            GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        }

        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Setup() 
    {
        ChildLineAdjustment = new Vector3(-GetComponent<RectTransform>().rect.width / 2, GetComponent<RectTransform>().rect.height);
        ParentLineAdjustment = new Vector3(transform.Find("Children").GetComponent<RectTransform>().rect.width / 2, -transform.Find("Children").GetComponent<RectTransform>().rect.height);

        DrawLinesToParent();

        foreach(DefaultNode node in internetChildren)
        {
            node.Setup();
        }
    }

    private void Start()
    {
        if (internetParent == null)
        {
            Setup();
        }
    }

    public void MakeCurrentNode()
    {
        FindObjectOfType<Hackmap>().SetCurrentNode(this);
    }

    public void DrawLinesToParent ()
    {
        foreach(DefaultNode node in internetChildren)
        {
            DrawLineToParent(node);
        }
    }

    public void DrawLineToParent(DefaultNode child)
    {
        RectTransform Childrt = child.GetComponent<RectTransform>();
        Vector3[] positions = LineHelper.GetEasedLine(new Vector3(0 , 0, -50)/*+ChildLineAdjustment*/, new Vector3(-Childrt.anchoredPosition.x, -Childrt.anchoredPosition.y, -50)/*+ParentLineAdjustment*/, 50, 3);
        child.lineRenderer.positionCount = positions.Length;
        child.lineRenderer.SetPositions(positions);
    }
}

/// <summary>
/// A custom linked list for nodes
/// </summary>
public class LinkedList {
    
}