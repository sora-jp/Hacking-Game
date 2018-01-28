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

        //float width = GetRightmostNodeInTree(GetRightmostNode(new List<Node>(GetComponentsInChildren<Node>()))).transform.position.x;
        //float height = GetBiggestTreeHeight(new List<Node>(GetComponentsInChildren<Node>()));
        GetRightmostNodeInTree(GetRightmostNode(new List<Node>(GetComponentsInChildren<Node>())));
        //(transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        //(transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    /// <summary>
    /// Gets the node which is most to the right from an array of nodes
    /// </summary>
    /// <param name="nodes">The nodes to select from</param>
    /// <returns></returns>
    private Node GetRightmostNode (List<Node> nodes)
    {
        
        if (nodes.Count > 1)
        {
            Node rightest = null;

            foreach (Node node in nodes)
            {
                if (node == nodes[0])
                {
                    rightest = nodes[0];
                    continue;
                }

                if (node.gameObject.transform.position.x > rightest.transform.position.x)
                {
                    rightest = node;
                }
            }

            return rightest;
        } else
        {
            if (nodes.Count==1)
            {
                return nodes[0];
            } else
            {
                return null;
            }
        }
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
            Debug.Log("Loooping");
            currentNode = GetRightmostNode(node.internetChildren);
        }

        return currentNode;
    }

    private float GetBiggestTreeHeight(List<Node> nodes)
    {
        List<float> heights = new List<float>();

        foreach (Node node in nodes)
        {
            heights.Add(CalculateTreeHeight(node));
        }
        return GetBiggestValue(heights);
    }

    private float CalculateTreeHeight(Node node)
    {
        List<float> heights = new List<float>();

        if (node.internetChildren.Count > 0)
        {
            foreach (Node n in node.internetChildren)
            {
                Debug.Log(n.nodeID);
                heights.Add(CalculateTreeHeight(n));
            }
            return GetBiggestValue(heights) + Mathf.Abs((node.transform as RectTransform).anchoredPosition.y);
        } else
        {
            return Mathf.Abs((node.transform as RectTransform).anchoredPosition.y);          
        }
    }

    private float GetBiggestValue(List<float> values)
    {
        values.Sort();
        return values[values.Count - 1];
    }
}
