using UnityEngine;
using System.Collections;

public class SwitchPlayerScript : MonoBehaviour {
    private GameObject[] players;
    private bool SinglePlayer = false;
	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players[0].GetComponent<PlayerController>().isSinglePlayer)
        {
            SinglePlayer = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("SwitchPlayer") && SinglePlayer)
        {
            foreach(GameObject cur in players)
            {
                cur.GetComponent<PlayerController>().isActive = !cur.GetComponent<PlayerController>().isActive;
                //cur.GetComponent<AIPath>().enabled = !cur.GetComponent<AIPath>().enabled;
                //if (cur.GetComponent<Rigidbody>() == null)
                //{
                //    cur.AddComponent<Rigidbody>();
                //    cur.GetComponent<Rigidbody>().mass = 90;
                //    cur.GetComponent<Rigidbody>().freezeRotation = true;
                //}
                //else
                //{
                //    Destroy(cur.GetComponent<Rigidbody>());
                //}
            }
        }
	}
}
