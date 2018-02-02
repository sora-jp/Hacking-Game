using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ConnectionType {
    Default, Power
}

/// <summary>
/// Node map, here is ur LinkedList wahlnöt
/// </summary>
public class NodeMap
{
    public ConnectionType type;

    public Node head;

    public NodeMap(Node head, ConnectionType type)
    {
        if (head == null) throw new ArgumentNullException("head is null");
        this.head = head;
        this.type = type;
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
}

/// <summary>
/// This class basically stores parent/child information
/// </summary>
public class NodeConnectionData {
    public Node parent;
    public List<Node> children;
    public NodeMap tree;

    public NodeConnectionData(Node parent = null, List<Node> children = null, NodeMap tree = null)
    {
        this.parent = parent;
        this.tree = tree;

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
    private Dictionary<ConnectionType, NodeConnectionData> data;

    public abstract ConnectionType[] GetConnectionType();

    private void Awake()
    {
        data = new Dictionary<ConnectionType, NodeConnectionData>();

        foreach (ConnectionType t in GetConnectionType())
        {
            data.Add(t, new NodeConnectionData());
        }
    }

    public void Initialize()
    {

    }

    public Node GetHead(ConnectionType t)
    {
        return GetMap(t).head;
    }

    public IEnumerator<Node> GetNodesUpIn(ConnectionType t)
    {
        return GetMap(t).UpFrom(this);
    }

    public NodeMap GetMap(ConnectionType t)
    {
        return data[t].tree;
    }

    public Node GetParent(ConnectionType t)
    {
        return data[t].parent;
    }

    public List<Node> GetChildren(ConnectionType t)
    {
        return data[t].children;
    }
}