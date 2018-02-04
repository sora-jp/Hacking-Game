using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum ConnectionType {
    Default, Power
}

/// <summary>
/// Node map, here is ur LinkedList wahlnöt
/// </summary>
public class NodeMap
{
    private ConnectionType type;
    public ConnectionType Type {
        get {
            return type;
        }
    }

    public Node head;

    public NodeMap(Node head, ConnectionType type)
    {
        if (head == null) throw new ArgumentNullException("head is null");
        this.head = head;
        this.type = type;

        foreach (Node n in DownFrom(head))
        {
            n.SetNodeMap(this);
        }
    }


    /// <summary>
    /// Returns all nodes up from the specified node, basically the path to the head node. To be used in this manner:
    /// <para></para>
    /// <code>foreach(Node n in list.UpFrom(child)) { ... }</code>
    /// </summary>
    /// <typeparam name="TNode">Type of the node</typeparam>
    /// <param name="child">The node to look from</param>
    /// <returns></returns>
    public IEnumerator<Node> UpFrom(Node child)
    {
        Node current = child;
        yield return child;
        while (current.GetParent(type) != null)
        {
            yield return current;
            current = current.GetParent(type);
        }
    }

    public IEnumerable<Node> DownFrom(Node node)
    {
        yield return node; 
        foreach (Node n in node.GetChildren(Type))
        {
            foreach(Node n1 in DownFrom(n))
            {
                yield return n1;
            } 
        }
    }

    public void DrawLines(ConnectionType t)
    {
        head.DrawLinesInTree(type, type == t);
    }
}

/// <summary>
/// This class basically stores parent/child information
/// </summary>
public class NodeConnectionData {
    public Node parent;
    public List<Node> children;
    public NodeMap tree;
    public LineRenderer lineRenderer;

    public NodeConnectionData(Node parent, List<Node> children, LineRenderer lineRenderer)
    {
        this.parent = parent;
        this.lineRenderer = lineRenderer;

        if (children != null)
        {
            this.children = children;
        } else
        {
            this.children = new List<Node>();
        }
    }
}

/// <summary>
/// Implement this to get a node which can be used 
/// </summary>
public abstract class Node : MonoBehaviour
{
    /// <summary>
    /// The dictionary which is responsible for keeping track of all connections
    /// </summary>
    private Dictionary<ConnectionType, NodeConnectionData> connectionsdata;

    /// <summary>
    /// This is implemented in the parent and it's responsible for telling the node what connections to have!
    /// <para>NEVER USE THIS!</para>
    /// </summary>
    /// <returns>What types of connections to have</returns>
    public abstract Dictionary<ConnectionType, NodeConnectionData> GetConnections();

    /// <summary>
    /// Is called on Awake
    /// <para>Use this function to setup a node</para>
    /// </summary>
    protected abstract void Initialize();

    private static int lineSegments = 50;

    public static Color backgroundLineColor = new Color(170f/255f, 170f/255f, 170f/255f, 40f/255f);

    public RectTransform Rect {
        get {
            return GetComponent<RectTransform>();
        }
    }

    private void Awake()
    {
        connectionsdata = GetConnections();

        GenerateLines();

        Initialize();
    }

    public void DrawLinesInTree(ConnectionType t, bool selected)
    {
        Debug.Log("Drawn Connections");

        if (!HasConnection(t)) return;

        foreach (Node n in GetChildren(t))
        {
            n.DrawLinesInTree(t, selected);
        }

        if (selected)
        {
            GetLineRenderer(t).material.SetColor("_TintColor", Color.white);
        } else
        {
            GetLineRenderer(t).material.SetColor("_TintColor", backgroundLineColor);
        }
    }

    public void GenerateLines()
    {
        foreach (ConnectionType t in connectionsdata.Keys)
        {
            if (GetParent(t) != null)
            {
                Vector3 start = new Vector3(-Rect.anchoredPosition.x + GetParent(t).Rect.rect.width / 2, -Rect.anchoredPosition.y - GetComponent<LayoutElement>().minHeight, -50);
                Vector3 end = new Vector3(0, 0, -50);

                Vector3[] path = GetLinePathOfType(t, start, end);
                GetLineRenderer(t).positionCount = path.Count();
                GetLineRenderer(t).SetPositions(path);
            }
        }
    }

    private Vector3[] GetLinePathOfType(ConnectionType t, Vector3 start, Vector3 end)
    {
        switch (t)
        {
            default:
                return LineHelper.GetEasedLine(start, end, lineSegments, 2f);

            case ConnectionType.Default:
                return LineHelper.GetEasedLine(start, end, lineSegments, 2f);

            case ConnectionType.Power:
                return LineHelper.GetStraightLine(start, end);
        }
    }

    /// <summary>
    /// Gets the head of the selected NodeMap
    /// </summary>
    /// <param name="t">The NodeMap connection type</param>
    /// <returns>The head of the NodeMap</returns>
    public Node GetHead(ConnectionType t)
    {
        return GetMap(t).head;
    }

    /// <summary>
    /// Gets all the nodes upwards from this node in the specified tree
    /// </summary>
    /// <param name="t">The NodeMap connection type</param>
    /// <returns></returns>
    public IEnumerator<Node> GetNodesUpIn(ConnectionType t)
    {
        return GetMap(t).UpFrom(this);
    }

    public NodeMap GetMap(ConnectionType t)
    {
        return connectionsdata[t].tree;
    }

    public Node GetParent(ConnectionType t)
    {
        return connectionsdata[t].parent;
    }

    public List<Node> GetChildren(ConnectionType t)
    {
        return connectionsdata[t].children;
    }

    private LineRenderer GetLineRenderer(ConnectionType t)
    {
        return connectionsdata[t].lineRenderer;
    }

    public List<ConnectionType> GetConnectionTypes()
    {
        List<ConnectionType> types = new List<ConnectionType>();

        foreach (ConnectionType t in connectionsdata.Keys)
        {
            types.Add(t);
        }

        return types;
    }

    private IEnumerable<LineRenderer> GetLineRenderers()
    {
        foreach (NodeConnectionData d in connectionsdata.Values)
        {
            yield return d.lineRenderer;
        }
    }

    public void SetNodeMap(NodeMap map)
    {
        connectionsdata[map.Type].tree = map;
    }

    public bool HasConnection(ConnectionType t)
    {
        if (GetConnectionTypes().Contains(t))
        {
            return true;
        } else
        {
            return false;
        }
    }
}