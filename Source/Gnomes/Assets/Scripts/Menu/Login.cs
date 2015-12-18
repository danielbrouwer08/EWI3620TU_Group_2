using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

    public Text Teamname;
    public Text pw;

    private GameManger gamemanager;
    public MenuManager menus;
    public MenuController wrongpws;

    void Awake()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
    }

    public void TeamLogin()
    {
        string user = Teamname.text;
        string pass = pw.text;
        gamemanager.onlineMode(user, pass);
        StartCoroutine(Registerthread());
    }

    IEnumerator Registerthread()
    {
        yield return new WaitForSeconds(2);
        if (gamemanager.loginSucceed == true)
        {
            PlayerPrefs.SetString("menuteamname", Teamname.text);
            Application.LoadLevel("Main Menu");
        }
        else
        {
            menus.ShowMenu(wrongpws);
        }
    }
}
