using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ConnectionMode
{
    Power, Internet
}

public class Hackmap : MonoBehaviour {

    /// <summary>
    /// The buttons which takes you to different modes
    /// </summary>
    public Button PowerButton;
    public Button InternetButton;

    /// <summary>
    /// A dictionary that takes a mode and spits out the button for it
    /// </summary>
    private Dictionary<ConnectionMode, GameObject> modeToButton;

    /// <summary>
    /// The modes you can currently select
    /// </summary>
    private List<ConnectionMode> unlockedModes = new List<ConnectionMode>();

    /// <summary>
    /// The current mode of the map
    /// </summary>
    public ConnectionMode mode;

    /// <summary>
    /// Normal sprite for the catagory buttons
    /// </summary>
    private Sprite normalSprite;

    /// <summary>
    /// Selected sprit for the catagory buttons
    /// </summary>
    public Sprite selectedSprite;

    /// <summary>
    /// The currently viewed node
    /// </summary>
    private DefaultNode currentNode;

    private void Awake()
    {
        //Defines the dictionary which maps from mode to button
        modeToButton = new Dictionary<ConnectionMode, GameObject>() {
            { ConnectionMode.Internet, InternetButton.gameObject },
            { ConnectionMode.Power, PowerButton.gameObject }
        };

        normalSprite = InternetButton.GetComponent<Image>().sprite;
    }

    private void Start()
    {
        UnlockMode(ConnectionMode.Internet); //Unlock a mode
        SetCurrentMode("Internet");
        FindObjectOfType<FitTreeSize>().FitSize();
    }

    /// <summary>
    /// Unlocks a new mode and also adds those buttons
    /// </summary>
    /// <param name="mode">The mode to unlock</param>
    public void UnlockMode(ConnectionMode mode)
    {
        unlockedModes.Add(mode); //Add the mode the unlocked modes

        //Activate the unlocked mode buttons
        foreach (ConnectionMode m in unlockedModes)
        {
            Debug.Log(m.ToString());
            Debug.Log("Key exists: " + modeToButton.ContainsKey(m));
            modeToButton[m].SetActive(true);
        }
    }

    /// <summary>
    /// Sets the current mode of the map
    /// </summary>
    /// <param name="mode">The mode to set</param>
    public void SetCurrentMode (string mode)
    {
        modeToButton[this.mode].GetComponent<Image>().sprite = normalSprite;

        this.mode = (ConnectionMode) Enum.Parse(typeof(ConnectionMode), mode);

        modeToButton[this.mode].GetComponent<Image>().sprite = selectedSprite;
    }

    /// <summary>
    /// Set the currently viewed node
    /// </summary>
    /// <param name="node">The node to view</param>
    public void SetCurrentNode (DefaultNode node)
    {
        currentNode = node;
    }

    /// <summary>
    /// Fix me
    /// </summary>
    /// <param name="node">Not proper input, FIX ME</param>
    void AddNode(Node node) //FIX ME
    {
        FindObjectOfType<FitTreeSize>().FitSize();
    }
}

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
    public IEnumerator UpFrom<TNode>(Node child) where TNode:Node
    {
        TNode current;
        yield return child;
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
    public abstract T GetParent<T>() where T:Node;
    public abstract T[]GetChild<T>() where T:Node;
}