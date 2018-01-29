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
        float width = GetMaximumWidthFromTrees(GetTopLevelNodeChilds(gameObject));
        float height = GetBiggestTreeHeight(GetTopLevelNodeChilds(gameObject));
        
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    private float GetMaximumWidthFromTrees(List<Node> nodes)
    {
        nodes.Sort(new CompareByX());
        return CalculateTreeWidth(nodes[0]);
    }

    private float CalculateTreeWidth(Node node)
    {
        if (node.internetChildren.Count != 0)
        {
            List<Node> children = node.internetChildren;
            children.Sort(new CompareByX());
            return CalculateTreeWidth(children[0]) + (node.transform as RectTransform).anchoredPosition.x;
        }
        else
        {
            Debug.Log(node.NodeID + "  " + (node.transform as RectTransform).anchoredPosition);
            return (node.transform as RectTransform).anchoredPosition.x;
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

    private List<Node> GetTopLevelNodeChilds(GameObject g)
    {
        List<Node> Childs = new List<Node>();

        foreach (Node n in g.GetComponentsInChildren<Node>())
        {
            Debug.Log("Child");
            if (n.transform.parent == g.transform)
            {
                Childs.Add(n);
                Debug.Log("Child Added");
            }
        }
        Debug.Log("Length: " + Childs.Count);
        return Childs;
    }
}

class CompareByX : IComparer<Node>
{
    public int Compare(Node n1, Node n2)
    {
        if (n1.transform.position. x < n2.transform.position.x)
        {
            return 1;
        } else if (n1.transform.position.x > n2.transform.position.x)
        {
            return -1;
        } else
        {
            return 0;
        }
    }
}
