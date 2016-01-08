using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {
    public GameObject[] player;
    public GameObject SwitchObject;
    public bool switchon = false;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectsWithTag("Player");
    }


	// Update is called once per frame
	void Update () {
        for (int i = 0; i < player.Length; i++)
        {
            if (Vector3.Distance(player[i].transform.position, transform.position) < 3)
            {

                if (Input.GetButtonDown("Interact" + player[i].GetComponent<PlayerController>().playerNum) && !switchon)
                {
                    transform.GetChild(0).transform.RotateAround(transform.position, new Vector3(1, 0, 0), -120f);
                    switchon = true;
                    SwitchObject.GetComponent<Renderer>().material = Resources.Load("SwitchOn") as Material;
                }
                else if (Input.GetButtonDown("Interact" + player[i].GetComponent<PlayerController>().playerNum) && switchon)
                {
                    transform.GetChild(0).transform.RotateAround(transform.position, new Vector3(1, 0, 0), 120f);
                    switchon = false;
                    SwitchObject.GetComponent<Renderer>().material = Resources.Load("SwitchOff") as Material;
                }
            }
        }
        if(transform.position.x - player[0].transform.position.x > 30 && transform.position.x - player[1].transform.position.x > 30)
        {
            transform.GetChild(0).transform.RotateAround(transform.position, new Vector3(1, 0, 0), 120f);
            switchon = false;
            SwitchObject.GetComponent<Renderer>().material = Resources.Load("SwitchOff") as Material;
        }
	}
}
