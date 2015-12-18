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
        if (user == pass)
        {
            Registerthread();
        }
        else
        {
            menus.ShowMenu(wrongpws);
        }
    }

    IEnumerator Registerthread()
    {
        yield return new WaitForSeconds(4);
        //if (gamemanager.registerSucceed == true) menus.ShowMenu(confirmmenu);
        Debug.Log("blablabla");
    }
}
