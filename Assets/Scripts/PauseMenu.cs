using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public MenuObject baseMenu;
    private MenuObject currentMenu;
    [HideInInspector]
    public bool isListening = true;
    void Start()
    {
        InputController.instance.onButtonInput += ListenPause;
        gameObject.SetActive(false);
    }

    void ListenPause(string button, InputState state)
    {
        if(isListening)
        {
            if(button == "Menu" && state == InputState.Released)
            {
                if(currentMenu == null)
                {
                    StartMenu();
                } else
                {
                    if(currentMenu.backMenu != null)
                    {
                        ChangeMenu(currentMenu.backMenu);
                    } else
                    {
                        ExitMenu();
                    }
                }
            }
        }
    }

    public void SetListening(bool state) {
        isListening = state;
    }

    private void StartMenu() {
        gameObject.SetActive(true);
        currentMenu = baseMenu;
        currentMenu.gameObject.SetActive(true);
    }
    public void ChangeMenu(MenuObject newMenu) {
        currentMenu.gameObject.SetActive(false);
        currentMenu = newMenu;
        currentMenu.gameObject.SetActive(true);
    }
    public void ExitMenu() {
        currentMenu.gameObject.SetActive(false);
        currentMenu = null;
        gameObject.SetActive(false);
    }
}
