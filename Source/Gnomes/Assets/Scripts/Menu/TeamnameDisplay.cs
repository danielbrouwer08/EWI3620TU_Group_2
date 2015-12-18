using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeamnameDisplay : MonoBehaviour {

    private Text teamname;

    void Awake()
    {
        teamname = GetComponent<Text>();
    }

    void Start()
    {
        string team = PlayerPrefs.GetString("menuteamname");
        teamname.text = team;
    }
}
