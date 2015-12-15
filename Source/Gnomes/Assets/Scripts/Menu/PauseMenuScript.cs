using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    public MenuManager menu;
    public MenuController pausemenu;
    public MenuController optionsmenu;
    public BackgroundController backgroundimage;
    public BackgroundController ingameinterface;
    public Button resumebutton;

    bool Paused = false;
<<<<<<< HEAD
    
=======
>>>>>>> 675bd35fad532a10756c2f0d8bcc8b1fe002474a

    void Update()
    {
        Debug.Log(Paused);
        if (Input.GetKeyDown("escape")){
            if (Paused == true)
            {
                ResumeTime();
            }
            else if (Paused == false)
            {
                PauseMenu();
            }
        }
        resumebutton.onClick.AddListener(delegate () { ResumeTime(); });
    }


    void PauseMenu()
    {
        Time.timeScale = 0;
        backgroundimage.IsPause = true;
        ingameinterface.IsPause = true;
        menu.ShowMenu(pausemenu);
        Paused = true;

    }

    void ResumeTime()
    {
        Time.timeScale = 1;
        backgroundimage.IsPause = false;
        ingameinterface.IsPause = false;
        pausemenu.IsOpen = false;
        Paused = false;
    }
}
