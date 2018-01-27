using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FitTreeSize : MonoBehaviour {

    public bool ManualFitSize;

    private void OnValidate()
    {
        if (ManualFitSize)
        {
            ManualFitSize = false;
        }

        FitSize();
    }

    /// <summary>
    /// Fits the size of the window the the space needed
    /// </summary>
    public void FitSize()
    {
        Debug.Log("Fit size");

        float width = GetRightmostNodeInTree(GetRightmostNode(new List<Node>(GetComponentsInChildren<Node>()))).transform.position.x;
        float height = GetLowestNodeInTrees(new List<Node>(GetComponentsInChildren<Node>())).transform.position.y;

        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    /// <summary>
    /// Gets the node which is most to the right from an array of nodes
    /// </summary>
    /// <param name="nodes">The nodes to select from</param>
    /// <returns></returns>
    private Node GetRightmostNode (List<Node> nodes)
    {
        Node rightest = nodes[0];

        foreach(Node node in nodes)
        {
            if (node == rightest)
            {
                continue;
            }

            if (node.gameObject.transform.position.x > rightest.transform.position.x)
            {
                rightest = node;
            }
        }

        return rightest;
    }

    /// <summary>
    /// Gets the node which is most to the left from an array of nodes
    /// </summary>
    /// <param name="nodes">The nodes to select from</param>
    /// <returns></returns>
    private Node GetLeftmostNode(List<Node> nodes)
    {
        Node leftest = nodes[0];

        foreach (Node node in nodes)
        {
            if (node == leftest)
            {
                continue;
            }

            if (node.gameObject.transform.position.x < leftest.transform.position.x)
            {
                leftest = node;
            }
        }

        return leftest;
    }

    /// <summary>
    /// Gets the node wich is most right from a node and its children
    /// </summary>
    /// <param name="node"></param>
    /// <returns>The starting node</returns>
    private Node GetRightmostNodeInTree(Node node)
    {
        Node currentNode = node;

        while (currentNode.internetChildren.Count != 0)
        {
            currentNode = GetRightmostNode(node.internetChildren);
        }

        return currentNode;
    }

    private Node GetLeftmostNodeInTree(Node node)
    {
        Node currentNode = node;

        while (node.internetChildren.Count != 0)
        {
            currentNode = GetLeftmostNode(node.internetChildren);
        }

        return currentNode;
    }

    private Node GetLowestNodeInTree(Node startNode)
    {
        List<Node> checkedNodes = new List<Node>();
        Node currentNode = startNode;
        Node lowestNode = startNode;

        while (ContainsUncheckedChildren(currentNode, checkedNodes))
        {

            while (currentNode.internetChildren.Count != 0)
            {
                if (!checkedNodes.Contains(currentNode))
                {
                    checkedNodes.Add(currentNode);
                }

                foreach (Node node in currentNode.internetChildren)
                {
                    if (!checkedNodes.Contains(node))
                    {
                        currentNode = node;
                        break;
                    }
                }
            }

            checkedNodes.Add(currentNode);

            if (currentNode.transform.position.y < lowestNode.transform.position.y)
            {
                lowestNode = currentNode;
            }

            while (!ContainsUncheckedChildren(currentNode, checkedNodes) && currentNode.internetParent != null)
            {
                currentNode = currentNode.internetParent;
            }
        }

        return lowestNode;
    }

    private bool ContainsUncheckedChildren(Node node, List<Node> checkedNodes)
    {
        foreach(Node n in node.internetChildren)
        {
            if (!checkedNodes.Contains(n))
            {
                return true;
            }
        }

        return false;
    }

    private Node GetLowestNodeInTrees(List<Node> trees)
    {
        Node currentLowest = trees[0];
        foreach (Node node in trees)
        {
            if (GetLowestNodeInTree(node).transform.position.y < currentLowest.transform.position.y)
            {
                currentLowest = GetLowestNodeInTree(node);
            }
        }

        return currentLowest;
    }
}
