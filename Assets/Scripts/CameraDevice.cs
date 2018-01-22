using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDevice : HackableDevice {
    public new Camera camera;
    Camera playerCam;
    
    float angleX;
    float angleY;
    
    public float sx = 1;
    public float sy = 1;
    
    public float minAX = -150;
    public float maxAX = 150;
    public float minAY = -75;
    public float maxAY = 75;

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
        
        if (camera.enabled) 
        {
            float dx = Input.GetAxisRaw("Mouse X") * sx;
            float dy = Input.GetAxisRaw("Mouse Y") * sy;
            
            angleX = Mathf.Clamp(angleX + dx, minAY, maxAY);
            angleY = Mathf.Clamp(angleY + dy, minAX, maxAX);
            
            transform.rotation = Quaternion.Euler(angleY, angleX, 0);
        }
    }
}
