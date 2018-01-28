using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hacking;

public class CameraDevice : HackableDevice {
    public new Camera camera;
    public GameObject ui;
    public GameObject graphics;
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

    GameObject head;

    private void Start()
    {
        if (camera == null) camera = GetComponentInChildren<Camera>();
        camera.enabled = false;
        ui.SetActive(false);
        isActive = false;
        head = graphics.transform.GetChild(0).gameObject;
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
        graphics.SetActive(false);
        ui.SetActive(true);
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
            ui.SetActive(false);
            isActive = false;
            graphics.SetActive(true);
        }

        float dx = Input.GetAxisRaw("Mouse X") * sx;
        float dy = Input.GetAxisRaw("Mouse Y") * sy;
            
        angleX = Mathf.Clamp(angleX + dx, minAX, maxAX);
        angleY = Mathf.Clamp(angleY + dy, minAY, maxAY);
            
        camera.transform.localRotation = Quaternion.Euler(-angleY, angleX + 180, 0);
        head.transform.localRotation = Quaternion.Euler(angleY, 0, angleX);
        Ray ray;
        RaycastHit h;
        var device = Player.GetDeviceUnderCursor(camera, hackableLayer, out ray, out h);
        if (device != null) 
        {
            Debug.Log("Hit Device!");
            Debug.DrawLine(ray.origin, h.point, Color.green);
            if (device is IHackable) 
            {
                if ((device as IHackable).CanBeHacked(true))
                {
                    cursor.sprite = hitCursor;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (device is CameraDevice)
                        {
                            isActive = false;
                            camera.enabled = false;
                            graphics.SetActive(true);
                            ui.SetActive(false);
                        }
                        (device as IHackable).Hack(null);
                    }
                }
            }
        }
    }
}
