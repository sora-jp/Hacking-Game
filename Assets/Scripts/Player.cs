﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hacking;

public class Player : MonoBehaviour {

    public LayerMask hackableLayer;
    public bool doUpdate;
    public Image cursor;
    public Sprite normalCursor;
    public Sprite hitCursor;

    private void Awake()
    {
        doUpdate = true;
    }

    void Update ()
    {
        cursor.sprite = normalCursor;

        if (!doUpdate) return;
		var device = GetDeviceUnderCursor(Camera.main, hackableLayer);
		if (device != null)
		{
			// Hit a device

			if (device is IHackable)
			{
                if ((device as IHackable).CanBeHacked(false))
                {
                    cursor.sprite = hitCursor;

                    if (Input.GetMouseButtonDown(0))
                    {
                        // Hack the device we hit
                        (device as IHackable).Hack(this);
                    }
                }
			}
		}
    }
	
    public static IDevice GetDeviceUnderCursor(Camera c, LayerMask layer)
    {
	    // Get a Ray from the center of the screen, so we can check what is under the cursor
        var ray = c.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        ray.origin += ray.direction * 0.5f;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            // Hit something
            var device = hit.transform.root.GetComponent<IDevice>();
            return device;
        }
        return null;
    }

    public static IDevice GetDeviceUnderCursor(Camera c, LayerMask layer, out Ray ray, out RaycastHit hit)
    {
        // Get a Ray from the center of the screen, so we can check what is under the cursor
        ray = c.ViewportPointToRay(Vector3.one * 0.5f);
        ray.origin += ray.direction * 0.5f;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            // Hit something
            var device = hit.transform.root.GetComponent<IDevice>();
            return device;
        }
        return null;
    }
}
