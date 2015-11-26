using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {
    public GameObject player;
    public GameObject SwitchObject;
    public bool switchon = false;
	// Use this for initialization
	void Start () {
	    
	}

	// Update is called once per frame
	void Update () {
	    if(Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            
            if (Input.GetKeyDown("e") && !switchon)
            {
                transform.GetChild(0).transform.RotateAround(transform.position, new Vector3(1, 0, 0), -120f);
                switchon = true;
                SwitchObject.GetComponent<Renderer>().material = Resources.Load("SwitchOn") as Material;
            }
            else if(Input.GetKeyDown("e") && switchon)
            {
                transform.GetChild(0).transform.RotateAround(transform.position, new Vector3(1, 0, 0), 120f);
                switchon = false;
                SwitchObject.GetComponent<Renderer>().material = Resources.Load("SwitchOff") as Material;
            }
        }
	}
}
