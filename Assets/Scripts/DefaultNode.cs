using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultNode : Node {

    /// <summary>
    /// All variables below are set in the inspector
    /// </summary>
    public DefaultNode[] powerChilds;
    public DefaultNode powerParent;

    public DefaultNode parent;
    public DefaultNode[] children;

    public Vector3 ParentLineAdjustment;

    public string NodeID;

    public bool setup;

    public float graphicWidth;
    public float graphicHeight;

    /// <summary>
    /// Theese nodes are somehow contained in the nodes parents/children relations
    /// </summary>
    public List<DefaultNode> internetChildren = new List<DefaultNode>();
    public DefaultNode internetParent;

    public LineRenderer lineRenderer;

    private void Awake()
    {
        if (transform.parent.GetComponent<DefaultNode>() != null)
        {
            internetParent = transform.parent.GetComponent<DefaultNode>();
        } 

        foreach(DefaultNode n in GetComponentsInChildren<DefaultNode>())
        {
            internetChildren.Add(n);
        }

        graphicWidth = GetComponent<LayoutElement>().minWidth;
        graphicHeight = GetComponent<LayoutElement>().minHeight;

        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        ParentLineAdjustment = new Vector3((transform.parent as RectTransform).rect.size.x/2, -graphicHeight);

        if (internetParent != null) {
            DrawLinesToParent();
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
        Vector3[] positions = LineHelper.GetEasedLine(new Vector3(0 , 0, -50), new Vector3(-Childrt.anchoredPosition.x, -Childrt.anchoredPosition.y, 50)+child.ParentLineAdjustment, -50, 3);
        child.lineRenderer.positionCount = positions.Length;
        child.lineRenderer.SetPositions(positions);
        Debug.Log("Thing");
    }

    private void OnValidate()
    {
        if (setup)
        {
            setup = false;
            DrawLinesToParent();
            Debug.Log("Setup");
        }
    }

    public override ConnectionType[] GetConnectionType()
    {
        throw new System.NotImplementedException();
    }
}