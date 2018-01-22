using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static Animator anim;
    public string lastState;

    public bool mouseVisible;
    public bool MouseVisible {
        get {
            return mouseVisible;
        }

        set {
            mouseVisible = value;
            //FindObjectOfType<Pla>
        }
    }

    public void LoadMainMenu() { anim.SetTrigger("MainMenu"); lastState = "MainMenu"; }
    public void UnloadMenu() { anim.SetTrigger("Unload"); lastState = "Unload"; }
    public void LoadOptions() { anim.SetTrigger("OptionsMenu"); lastState = "OptionsMenu"; }
    public void EndGame() { Application.Quit(); }
    public void Back() { anim.SetTrigger("Back");  }

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
    }
}
