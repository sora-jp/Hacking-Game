﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DefaultNode : Node {

    public LineRenderer lineRenderer;

    public DefaultNode[] powerChildren = new DefaultNode[0];
    public DefaultNode powerParent;

    public override Dictionary<ConnectionType, NodeConnectionData> GetConnections()
    {
        NodeConnectionData DefaultData = new NodeConnectionData(transform.parent.GetComponent<Node>(), new List<Node>(GetChildren()), transform.Find("Default Connection").GetComponent<LineRenderer>());
        NodeConnectionData PowerData = new NodeConnectionData(powerParent, new List<Node>(powerChildren), transform.Find("Power Connection").GetComponent<LineRenderer>());

        return new Dictionary<ConnectionType, NodeConnectionData>() { { ConnectionType.Default, DefaultData }, { ConnectionType.Power, PowerData }};
    }

    protected override void Initialize()
    {
        
    }

    private IEnumerable<Node> GetChildren()
    {
        Debug.Log("Children");
        foreach (Node n in GetComponentsInChildren<Node>())
        {
            if (n.transform.parent == transform)
            {
                yield return n;
            }
        }
    }
}