using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDevice : HackableDevice {
    public new Camera camera;
    Camera playerCam;

    private void Start()
    {
        if (camera == null) camera = GetComponentInChildren<Camera>();
        camera.enabled = false;
        playerCam = Camera.main;
    }

    public override void Hack(Player player)
    {
        playerCam.transform.root.gameObject.SetActive(false);
        camera.enabled = true;
        //Do stuff here, but later :)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && camera.enabled)
        {
            playerCam.transform.root.gameObject.SetActive(true);
            camera.enabled = false;
        }
    }
}
