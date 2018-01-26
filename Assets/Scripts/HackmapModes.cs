using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ConnectionMode
{
    Power, Internet
}

public class HackmapModes : MonoBehaviour {

    public ConnectionMode mode;

	public void SetMode (string mode)
    {
        this.mode = (ConnectionMode) Enum.Parse(typeof(ConnectionMode), mode);
    }
}
