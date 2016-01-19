using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public MenuController currMenu;

    // Use this for initialization
    public void Start() {
        if (currMenu != null)
        {
            ShowMenu(currMenu);
        }
    }

    public void ShowMenu(MenuController menu)
    {
        if (currMenu != null)
            currMenu.IsOpen = false;

        currMenu = menu;
        currMenu.IsOpen = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
