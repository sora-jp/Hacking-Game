using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultNode : MonoBehaviour {

    public Dictionary<ConnectionMode, LinkedList> connections;

    /// <summary>
    /// All variables below are set in the inspector
    /// </summary>
    public Node[] powerChilds;
    public Node[] internetChilds;

    public Node powerParent;
    public Node internetParent;
}

/// <summary>
/// A custom linked list for nodes
/// </summary>
public class LinkedList {
    
}