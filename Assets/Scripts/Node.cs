using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public Dictionary<ConnectionMode, LinkedList<Node>> connections;

    /// <summary>
    /// All variables below are set in the inspector
    /// </summary>
    public Node[] powerChilds;
    public Node[] internetChilds;

    public Node powerParent;
    public Node internetParent;

	// Use this for initialization
	void Start () {
        connections = new Dictionary<ConnectionMode, LinkedList<Node>> {
            { ConnectionMode.Internet, new LinkedList<Node>(internetParent, new List<Node>(internetChilds)) },
            { ConnectionMode.Power, new LinkedList<Node>(powerParent, new List<Node>(powerChilds))} };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

/// <summary>
/// A custom linked list for nodes
/// </summary>
/// <typeparam name="T">The type of the linked list to create, should always be Nodes</typeparam>
public class LinkedList<T> {

    public T parent;
    public List<T> children;

    public LinkedList(T parent, List<T> children)
    {
        children = new List<T>(children);
        this.parent = parent;
    }
}