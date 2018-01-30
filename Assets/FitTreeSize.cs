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

    private void Start()
    {
        FitSize();
    }

    /// <summary>
    /// Fits the size of the window the the space needed
    /// </summary>
    public void FitSize()
    {
        List<Node> nodes = new List<Node>(GetComponentsInChildren<Node>());

        nodes.Sort(new CompareByX());
        float width = Mathf.Abs(GetWidthFromRoot(nodes[0]));

        nodes.Sort(new CompareByY());
        float height = Mathf.Abs(GetHeightFromRoot(nodes[0]));

        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    private float GetWidthFromRoot(Node node)
    {
        Node currentNode = node;
        float width = 0;
        while (currentNode.internetParent != null)
        {
            width += currentNode.GetComponent<RectTransform>().anchoredPosition.x;
            currentNode = currentNode.internetParent;
        }
        return width +  currentNode.GetComponent<RectTransform>().anchoredPosition.x;
    }

    private float GetHeightFromRoot(Node node)
    {
        Node currentNode = node;
        float height = 0;
        while (currentNode.internetParent != null)
        {
            height += currentNode.GetComponent<RectTransform>().anchoredPosition.y;
            currentNode = currentNode.internetParent;
        }
        return height + currentNode.GetComponent<RectTransform>().anchoredPosition.y;
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