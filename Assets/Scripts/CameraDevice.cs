using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDevice : HackableDevice {
    public new Camera camera;
    Camera playerCam;
    
    public LayerMask hackableLayer;
    
    float angleX;
    float angleY;
    
    public float sx = 1;
    public float sy = 1;
    
    public float minAX = -150;
    public float maxAX = 150;
    public float minAY = -75;
    public float maxAY = 75;

    public Image cursor;
    public Sprite normalCursor;
    public Sprite hitCursor;

    private void Start()
    {
        if (camera == null) camera = GetComponentInChildren<Camera>();
        camera.enabled = false;
        playerCam = Camera.main;
    }

    public override void Hack(Player player)
    {
        playerCam.transform.root.gameObject.SetActive(false);
        
        foreach(Camera c in FindObjectsOfType<Camera>()) {
            c.enabled = false;
        }
        
        camera.enabled = true;
        //Do stuff here, but later :)
    }

    private void Update()
    {
        cursor.sprite = normalCursor;

        if (Input.GetKeyDown(KeyCode.R) && camera.enabled)
        {
            playerCam.transform.root.gameObject.SetActive(true);
            camera.enabled = false;
        }
        
        if (camera.enabled) 
        {
            float dx = Input.GetAxisRaw("Mouse X") * sx;
            float dy = Input.GetAxisRaw("Mouse Y") * sy;
            
            angleX = Mathf.Clamp(angleX + dx, minAX, maxAX);
            angleY = Mathf.Clamp(angleY + dy, minAY, maxAY);
            
            transform.rotation = Quaternion.Euler(angleY, angleX, 0);
            
            var device = Player.GetDeviceUnderCursor(camera, hackableLayer);
            if (device != null) 
            {
                cursor.sprite = hitCursor;
                if (Input.GetMouseButtonDown(0) && device is IHackable) 
                {
                    ((IHackable)device).Hack(null);
                }
            }
        }
    }
}
