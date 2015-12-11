using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    public MenuManager menu;
    public MenuController pausemenu;
    public MenuController optionsmenu;
    public BackgroundController backgroundimage;
    public Button resumebutton;

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            PauseMenu();
            backgroundimage.IsPause = true;
        }
        resumebutton.onClick.AddListener(delegate () { ResumeTime(); });
    }

    void PauseMenu()
    {
        Time.timeScale = 0; //Still a problem atm, since also the menu's themselves will freeze because of this.
        menu.ShowMenu(pausemenu);
    }

    void ResumeTime()
    {
        Time.timeScale = 1;
        backgroundimage.IsPause = false;
    }
}
