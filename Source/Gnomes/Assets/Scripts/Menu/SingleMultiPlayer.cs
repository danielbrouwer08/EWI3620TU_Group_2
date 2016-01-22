using UnityEngine;
using System.Collections;

public class SingleMultiPlayer : MonoBehaviour {

    public void SetSinglePlayer()
    {
        PlayerPrefs.SetString("playermode", "single");
        //Debug.Log(PlayerPrefs.GetString("player_mode"));
    }

    public void SetMultiPLayer()
    {
        PlayerPrefs.SetString("playermode", "multi");
        //Debug.Log(PlayerPrefs.GetString("player_mode"));
    }

    public void SwitchMode()
    {
        GameObject[] players =  GameObject.FindGameObjectsWithTag("Player");
        for (int i=0; i<players.Length; i++)
        {
            players[i].GetComponent<PlayerController>().isSinglePlayer = !players[i].GetComponent<PlayerController>().isSinglePlayer;
        }
    }

}
