using UnityEngine;
using System.Collections;

public class Offline : MonoBehaviour {

    public void SetOffline()
    {
        if (PlayerPrefs.GetString("menuteamname") == null)
        {
            PlayerPrefs.SetString("menuteamname", "Offline Mode");
        }
        Application.LoadLevel("Main Menu");
    }
}
