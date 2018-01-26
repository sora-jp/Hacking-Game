using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDevice : HackableDevice {
    public new Camera camera;
    Camera playerCam;
    bool isActive;
    
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
        isActive = false;
        playerCam = Camera.main;
    }

    public override void Hack(Player player)
    {
        StartCoroutine(cHack());
        //Do stuff here, but later :)
    }

    IEnumerator cHack()
    {
        yield return new WaitForEndOfFrame();
        playerCam.transform.root.gameObject.SetActive(false);
        isActive = true;
        camera.enabled = true;
    }

    void Update()
    {
        if (!isActive) return;
        cursor.sprite = normalCursor;

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerCam.transform.root.gameObject.SetActive(true);
            camera.enabled = false;
        }

        float dx = Input.GetAxisRaw("Mouse X") * sx;
        float dy = Input.GetAxisRaw("Mouse Y") * sy;
            
        angleX = Mathf.Clamp(angleX + dx, minAX, maxAX);
        angleY = Mathf.Clamp(angleY + dy, minAY, maxAY);
            
        transform.localRotation = Quaternion.Euler(angleY, angleX, 0);
        Ray ray;
        RaycastHit h;
        var device = Player.GetDeviceUnderCursor(camera, hackableLayer, out ray, out h);
        if (device != null) 
        {
            Debug.Log("Hit Device!");
            cursor.sprite = hitCursor;
            Debug.DrawLine(ray.origin, h.point, Color.green);
            if (Input.GetMouseButtonDown(0) && device is IHackable) 
            {
                if (device is CameraDevice)
                {
                    isActive = false;
                    camera.enabled = false;
                }
                (device as IHackable).Hack(null);
            }
        }
    }
}
