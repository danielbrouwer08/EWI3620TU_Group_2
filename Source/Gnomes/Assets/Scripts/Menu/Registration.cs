using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Registration : MonoBehaviour {

    public Text teamname;
    public Text pw1;
    public Text pw2;
    private GameManger gamemanager;
    public MenuManager menus;
    public MenuController confirmmenu;
    public MenuController wrongpws;

    void Awake()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
    }
	
    public void Register()
    {
        string teamtxt = teamname.text;
        string pass1 = pw1.text;
        string pass2 = pw2.text;
        if (pass1 == pass2)
        {
            gamemanager.register(teamtxt, pass1);
            StartCoroutine(Registerthread());
        }
        else
        {
            menus.ShowMenu(wrongpws);
        }
    }

    IEnumerator Registerthread()
    {
        yield return new WaitForSeconds(2);
        if (gamemanager.registerSucceed == true) menus.ShowMenu(confirmmenu);
    }

}
