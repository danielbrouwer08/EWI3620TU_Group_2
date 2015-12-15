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
            if(players[0].GetComponent<PlayerController>() == null)
            {

            }
        }
	}
}
