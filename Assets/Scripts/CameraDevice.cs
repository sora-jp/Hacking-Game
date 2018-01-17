using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDevice : HackableDevice {
    public new Camera camera;

    private void Start()
    {
        if (camera == null) camera = GetComponentInChildren<Camera>();
        camera.enabled = false;
    }

    public override void Hack(Player player)
    {
        Camera.main.enabled = false;
        camera.enabled = true;
        player.doUpdate = false;
        //Do stuff here, but later :)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && camera.enabled)
        {
            Camera.main.enabled = true;
            camera.enabled = false;
        }
    }
}
