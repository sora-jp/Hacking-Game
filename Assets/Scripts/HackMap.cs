using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HackMap : MonoBehaviour {

    /// <summary>
    /// The buttons which takes you to different modes
    /// </summary>
    public Button PowerButton;
    public Button DefaultButton;

    /// <summary>
    /// A dictionary that takes a mode and spits out the button for it
    /// </summary>
    private Dictionary<ConnectionType, GameObject> modeToButton;

    /// <summary>
    /// The modes you can currently select
    /// </summary>
    private List<ConnectionType> unlockedModes = new List<ConnectionType>();

    /// <summary>
    /// The current mode of the map
    /// </summary>
    private ConnectionType mode = ConnectionType.Default;

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

    /// <summary>
    /// The NodeMaps contained IN the map
    /// </summary>
    public List<NodeMap> trees = new List<NodeMap>();

    /// <summary>
    /// The gameobject called "Content" that all nodes are childs from
    /// </summary>
    public GameObject content;

    private void Awake()
    {
        //Defines the dictionary which maps from mode to button
        modeToButton = new Dictionary<ConnectionType, GameObject>() {
            { ConnectionType.Default, DefaultButton.gameObject },
            { ConnectionType.Power, PowerButton.gameObject }
        };

        normalSprite = DefaultButton.GetComponent<Image>().sprite;

        //Get and initialise all the node maps
        foreach (Node n in GetComponentsInChildren<Node>())
        {
            foreach (ConnectionType t in n.GetConnectionTypes())
            {
                if (n.GetParent(t) == null)
                {
                    NodeMap map = new NodeMap(n, t);
                    trees.Add(map);
                }
            }
        }
    }

    private void Start()
    {
        UnlockMode(ConnectionType.Default); //Unlock a mode
        UnlockMode(ConnectionType.Power);
        SetCurrentMode("Default");
    }

    /// <summary>
    /// Unlocks a new mode and also adds those buttons
    /// </summary>
    /// <param name="mode">The mode to unlock</param>
    public void UnlockMode(ConnectionType mode)
    {
        unlockedModes.Add(mode); //Add the mode the unlocked modes

        //Activate the unlocked mode buttons
        foreach (ConnectionType m in unlockedModes)
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
    public void SetCurrentMode(string mode)
    {
        modeToButton[this.mode].GetComponent<Image>().sprite = normalSprite;

        this.mode = (ConnectionType)Enum.Parse(typeof(ConnectionType), mode);

        modeToButton[this.mode].GetComponent<Image>().sprite = selectedSprite;

        DrawLines();
    }

    /// <summary>
    /// Set the currently viewed node
    /// </summary>
    /// <param name="node">The node to view</param>
    public void SetCurrentNode(DefaultNode node)
    {
        currentNode = node;
    }

    public IEnumerator<NodeMap> GetTreesOfType(ConnectionType t)
    {
        foreach (NodeMap n in trees)
        {
            if (n.Type == t)
            {
                yield return n;
            }
        }
    }

    public void DrawLines () {
        foreach (NodeMap t in trees)
        {
            t.DrawLines(mode);
        }
    }
}