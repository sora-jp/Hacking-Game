﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hacking;

public class SecurityTerminalDevice : HackableDevice {

    public GameObject canvas;
    public new GameObject camera;
    Player player;

    public new Renderer renderer;
    public Renderer[] childRenderers;

    public int rI;
    public int crI;

    public float colorStrength;
    bool wasHacked;

    public override void Hack(Player player)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("tye");
        this.player = player;
        canvas.SetActive(true);
        player.transform.root.gameObject.SetActive(false);
        camera.SetActive(true);

        wasHacked = true;

        //TODO: Start minigame here
    }

    public override bool CanBeHacked(bool camera)
    {
        return !camera && !wasHacked;
    }

    public override bool HasBeenHacked()
    {
        return wasHacked;
    }

    public void HackCompleted()
    {
        canvas.SetActive(false);
        player.transform.root.gameObject.SetActive(true);
        camera.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        renderer.materials[rI].SetColor("_EmissionColor", new Color(0, colorStrength, 0));

        foreach (var r in childRenderers)
        {
            r.materials[crI].SetColor("_EmissionColor", new Color(0, colorStrength, 0));
        }
    }



    // Use this for initialization
    void Start () {
        canvas.SetActive(false);
        camera.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
