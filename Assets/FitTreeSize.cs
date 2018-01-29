using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FitTreeSize : MonoBehaviour
{

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
        List<Node> nodes = new List<Node>(GetComponentsInChildren<Node>());

        nodes.Sort(new CompareByX());
        float width = Mathf.Abs(transform.position.x - nodes[0].transform.position.x);
        Debug.Log(nodes[0].NodeID);

        nodes.Sort(new CompareByY());
        float height = Mathf.Abs(transform.position.y - nodes[0].transform.position.y);

        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
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

class CompareByY : IComparer<Node>
{
    public int Compare(Node n1, Node n2)
    {
        if (n1.transform.position.y > n2.transform.position.y)
        {
            return 1;
        }
        else if (n1.transform.position.y < n2.transform.position.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}