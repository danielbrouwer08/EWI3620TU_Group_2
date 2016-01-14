using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.Audio;
using System.Collections.Generic;
//using UnityEditor;

public class PauseMenuScript : MonoBehaviour
{

    public MenuManager menu;
    public MenuController pausemenu;
    public MenuController optionsmenu;
    public BackgroundController backgroundimage;
    public BackgroundController ingameinterface;
    public AudioMixerSnapshot audiopaused;
    public AudioMixerSnapshot audiounpaused;

    public Button resumebutton;
    public GameObject[] players;

    bool Paused = false;
    
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        //Debug.Log(Paused);
        if (Input.GetKeyDown("escape")){
            if (Paused == true)
            {
                ResumeTime();
            }
            else if (Paused == false)
            {
                PauseMenu();
          
                Analytics.CustomEvent("pauseClicked", new Dictionary<string, object>
                {
                    { "x-location1",  players[0].transform.position.x},
                    { "z-location1", players[0].transform.position.z},
                    { "x-location2",  players[1].transform.position.x},
                    { "z-location2", players[1].transform.position.z},
					{ "currentScene", Application.loadedLevelName }
                });
               
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
        audiopaused.TransitionTo(.01f);

    }

    void ResumeTime()
    {
        Time.timeScale = 1;
        backgroundimage.IsPause = false;
        ingameinterface.IsPause = false;
        pausemenu.IsOpen = false;
        Paused = false;
        audiounpaused.TransitionTo(.01f);
    }

    public void Mainmenu()
    {
		ResumeTime();
        Application.LoadLevel("Main Menu");
    }
}
