using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hacking;

public class lrtest : MonoBehaviour {

    public LineRenderer r;
    public Vector2 a;
    public Vector2 b;
    public int it;
    public float ea;
    public bool down;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        r.SetPositions(LineHelper.GetEasedLine(a, b, it, ea, down));
	}
}
