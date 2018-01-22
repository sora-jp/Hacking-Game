using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public LayerMask hackableLayer;
    public bool doUpdate;

    private void Awake()
    {
        doUpdate = true;
    }

    void Update ()
    {
        if (!doUpdate) return;
		var device = GetDeviceUnderCursor(Camera.main, hackableLayer);
		if (device != null)
		{
			// Hit a device
			Debug.Log("YEEEEEEEEEEEEEEEEEE");

			if (Input.GetMouseButtonDown(0) && device is IHackable)
			{
				// Hack the device we hit
				(device as IHackable).Hack(this);
			}
		}
    }
	
    public static IDevice GetDeviceUnderCursor(Camera c, LayerMask layer) {
	// Get a Ray from the center of the screen, so we can check what is under the cursor
        var ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hackableLayer))
        {
            // Hit something
            var device = hit.transform.root.GetComponent<IDevice>();
            return device;
        }    
    }
}
