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
    private Node currentNode;

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
    public void SetCurrentNode (Node node)
    {
        currentNode = node;
    }
}
