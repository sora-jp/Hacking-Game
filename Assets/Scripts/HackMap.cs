using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ConnectionMode
{
    Power, Internet
}

public class Hackmap : MonoBehaviour {

    public ConnectionMode[] unlockedModes;

    public ConnectionMode mode;

    public LinkedList powerStartingPoints, internetStartingPoints;

    public void Start()
    {
        unlockedModes = new ConnectionMode[] { ConnectionMode.Internet };
    }

    public void SetMode (string mode)
    {
        this.mode = (ConnectionMode) Enum.Parse(typeof(ConnectionMode), mode);
    }
}
