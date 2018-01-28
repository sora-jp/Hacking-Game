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

    private void Start()
    {
        FitSize();
    }

    /// <summary>
    /// Fits the size of the window the the space needed
    /// </summary>
    public void FitSize()
    {
        Debug.Log("Fit size");

        float width = GetRightestNodeFromTrees(new List<Node>(GetComponentsInChildren<Node>())).transform.position.x;
        float height = GetBiggestTreeHeight(new List<Node>(GetComponentsInChildren<Node>()));
        
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    private Node GetRightestNodeFromTrees(List<Node> nodes)
    {
        nodes.Sort(new CompareByX());
        return CalculateRightestNode(nodes[0]);
    }

    private Node CalculateRightestNode(Node node)
    {
        if (node.internetChildren.Count != 0)
        {
            List<Node> children = node.internetChildren;
            children.Sort(new CompareByX());
            return CalculateRightestNode(children[0]);
        } else
        {
            return node;
        }
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

class CompareByX : IComparer<Node>
{
    public int Compare(Node n1, Node n2)
    {
        if (n1.transform.position.x > n2.transform.position.x)
        {
            return -1;
        } else if (n1.transform.position.x < n2.transform.position.x)
        {
            return 1;
        } else
        {
            return 0;
        }
    }
}
