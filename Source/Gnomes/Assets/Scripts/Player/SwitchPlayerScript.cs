using UnityEngine;
using System.Collections;

public class SwitchPlayerScript : MonoBehaviour {
    private GameObject[] players;
    private bool SinglePlayer = true;
	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (PlayerPrefs.GetString("playermode") == "single")
        {
            SinglePlayer = true;
            players[1].GetComponent<PlayerController>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("SwitchPlayer") && SinglePlayer)
        {
            foreach(GameObject cur in players)
            {
                cur.GetComponent<PlayerController>().isActive = !cur.GetComponent<PlayerController>().isActive;
                cur.GetComponent<PlayerController>().playerNum = 3 - cur.GetComponent<PlayerController>().playerNum;
                cur.GetComponent<PlayerController>().enabled = !cur.GetComponent<PlayerController>().enabled;
                cur.GetComponent<AIPath>().enabled = !cur.GetComponent<AIPath>().enabled;
            }
        }
	}
}
