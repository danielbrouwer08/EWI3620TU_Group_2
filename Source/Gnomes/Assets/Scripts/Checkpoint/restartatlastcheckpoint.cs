using UnityEngine;
using System.Collections;

public class restartatlastcheckpoint : MonoBehaviour {

    public PauseMenuScript pausemenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void restart()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject cur in players)
        {
            cur.GetComponent<PlayerProperties>().health = -10;
        }
        pausemenu.ResumeTime();
    }
}
