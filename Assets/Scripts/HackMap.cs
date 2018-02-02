using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hackmap : MonoBehaviour {

    /// <summary>
    /// The buttons which takes you to different modes
    /// </summary>
    public Button PowerButton;
    public Button InternetButton;

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
    public ConnectionType mode;

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
        modeToButton = new Dictionary<ConnectionType, GameObject>() {
            { ConnectionType.Default, InternetButton.gameObject },
            { ConnectionType.Power, PowerButton.gameObject }
        };

        normalSprite = InternetButton.GetComponent<Image>().sprite;
    }

    private void Start()
    {
        UnlockMode(ConnectionType.Default); //Unlock a mode
        SetCurrentMode("Internet");
        FindObjectOfType<FitTreeSize>().FitSize();
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
    public void SetCurrentMode (string mode)
    {
        modeToButton[this.mode].GetComponent<Image>().sprite = normalSprite;

        this.mode = (ConnectionType) Enum.Parse(typeof(ConnectionType), mode);

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