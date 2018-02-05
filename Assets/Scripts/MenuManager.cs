using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuManager : MonoBehaviour
{

    public static Animator anim;

    public bool mouseVisible;

    public Camera menuCam;

    public Canvas UI;

    public bool MenuOpen {
        get {
            if (menuCam.depth == 100)
            {
                return true;
            }
            return false;
        }
    }

    public void LoadMainMenu() { anim.SetTrigger("MainMenu");  menuCam.depth = 100; mouseVisible = true; UI.gameObject.SetActive(false); }
    public void UnloadMenu() { menuCam.depth = -100; mouseVisible = false; UI.gameObject.SetActive(true); }
    public void LoadOptions() { anim.SetTrigger("OptionsMenu");}
    public void EndGame() { Application.Quit(); }
    public void Back() { anim.SetTrigger("Back"); }
    public void LoadMap() { anim.SetTrigger("Map"); UpdateMap(); }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadMainMenu();
        }

        if (mouseVisible != Cursor.visible) {
            if (mouseVisible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = mouseVisible;
        }
    }

    void UpdateMap()
    {
        //FindObjectOfType<FitTreeSize>().FitSize();
    }
}
