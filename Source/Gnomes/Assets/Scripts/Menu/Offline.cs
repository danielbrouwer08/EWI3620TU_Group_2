using UnityEngine;
using System.Collections;

public class Offline : MonoBehaviour {

    public void SetOffline()
    {
        PlayerPrefs.SetString("menuteamname", "Offline Mode");
        Application.LoadLevel("Main Menu");
    }
}
