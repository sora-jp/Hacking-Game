using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuManager : MonoBehaviour
{

    public static Animator anim;
    public string lastState;

    public bool mouseVisible;

    public void LoadMainMenu() { anim.SetTrigger("MainMenu"); lastState = "MainMenu"; }
    public void UnloadMenu() { anim.SetTrigger("Unload"); lastState = "Unload"; }
    public void LoadOptions() { anim.SetTrigger("OptionsMenu"); lastState = "OptionsMenu"; }
    public void EndGame() { Application.Quit(); }
    public void Back() { anim.SetTrigger("Back"); }

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

        if (mouseVisible != Cursor.visible)
        {
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
}
