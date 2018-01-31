using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Node map, here is ur LinkedList wahlnöt
/// </summary>
/// <typeparam name="T"></typeparam>
public class NodeMap<T> where T : Node
{
    public T head;

    public NodeMap(T head)
    {
        if (head == null) throw new ArgumentNullException("head is null");
        this.head = head;
    }


    /// <summary>
    /// Returns all nodes up from the specified node, basically the path to the head node. To be used in this manner:
    /// <para></para>
    /// <code>foreach(Node n in list.UpFrom(child)) { ... }</code>
    /// </summary>
    /// <typeparam name="TNode">Type of the node</typeparam>
    /// <param name="child">The node to look from</param>
    /// <returns></returns>
    public IEnumerator<TNode> UpFrom<TNode>(Node child) where TNode : Node
    {
        TNode current;
        yield return child as TNode;
        while ((current = child.GetParent<TNode>()) != null)
        {
            yield return current;
        }
    }
}

/// <summary>
/// Implement this to get a node which can be used 
/// </summary>
public abstract class Node : MonoBehaviour
{
    public abstract T GetParent<T>() where T : Node;
    public abstract T[] GetChild<T>() where T : Node;
}