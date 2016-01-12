using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchPLayerScript : MonoBehaviour {
    GameObject[] players;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("SwitchPlayer"))
        {
            for(int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PlayerController>().enabled = !players[i].GetComponent<PlayerController>().enabled;
                players[i].GetComponent<AIPath>().enabled = !players[i].GetComponent<AIPath>().enabled;
                players[i].GetComponent<Seeker>().enabled = !players[i].GetComponent<Seeker>().enabled;
            }
        }
	}
}
