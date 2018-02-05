using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hacking;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {

    public LayerMask hackableLayer;
    public bool doUpdate;
    public Image cursor;
    public Sprite normalCursor;
    [FormerlySerializedAs("hitCursor")]
    public Sprite hackCursor;
    public Sprite noHackCursor;

    private void Awake()
    {
        doUpdate = true;
    }

    void Update ()
    {
        if (FindObjectOfType<MenuManager>().MenuOpen) return;

        cursor.sprite = normalCursor;

        if (!doUpdate) return;
<<<<<<< HEAD
        var device = GetDeviceUnderCursor(Camera.main, hackableLayer);
        if (device != null)
        {
            // Hit a device
            Debug.Log("YEEEEEEEEEEEEEEEEEE");
            cursor.sprite = hitCursor;

            if (Input.GetMouseButtonDown(0) && device is IHackable)
            {
                // Hack the device we hit
                (device as IHackable).Hack(this);
            }
        }
=======
		var device = GetDeviceUnderCursor(Camera.main, hackableLayer);
		if (device != null)
		{
			// Hit a device
			if (device is IHackable)
			{
                if (CanHackDevice(device))
                {
                    cursor.sprite = hackCursor;

                    if (Input.GetMouseButtonDown(0))
                    {
                        // Hack the device we hit
                        (device as IHackable).Hack(this);
                    }
                }
                else
                {
                    cursor.sprite = noHackCursor;
                }
			}
		}
>>>>>>> hacking
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

    public static bool CanHackDevice(IDevice device, bool camera = false)
    {
        IDevice d = device;
        bool c = (device as IHackable).CanBeHacked(camera);
        int i = 0;
        while (!string.IsNullOrEmpty((d as Device).parent))
        {
            d = DeviceHelper.DeviceFromID((device as Device).parent);
            i++;
            c = c && (d is IHackable) ? (d as IHackable).HasBeenHacked() : true;
        }
        return c;
    }
}
