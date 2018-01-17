using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public LayerMask hackableLayer;
    public bool doUpdate;

	void Update ()
    {
        if (!doUpdate) return;

        var ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hackableLayer))
        {
            //Debug.Log("HitIt " + hit.transform.gameObject.name);
            var device = hit.transform.root.GetComponent<IDevice>();
            if (device == null) return;
            Debug.Log("YEEEEEEEEEEEEEEEEEE");

            if (Input.GetMouseButtonDown(0) && device is IHackable)
            {
                (device as IHackable).Hack(this);
            }
        }
	}
}
