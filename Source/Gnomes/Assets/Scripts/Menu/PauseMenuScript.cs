using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {

    public MenuManager menu;
    public MenuController pausemenu;
    public Image menubackground;
    public MenuController optionsmenu;

    void Update () {
        if (Input.GetKey("escape"))
        {
            PauseMenu();
        }
	}

    void PauseMenu()
    {
        //Time.timeScale = 0; Still a problem atm, since also the menu's themselves will freeze because of this.
        menu.ShowMenu(pausemenu);
    }
}
