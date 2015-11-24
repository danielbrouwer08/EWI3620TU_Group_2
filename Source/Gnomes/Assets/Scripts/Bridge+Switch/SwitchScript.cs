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
            
            if (Input.GetKeyDown("e"))
            {
                switchon = true;
                SwitchObject.GetComponent<Renderer>().material = Resources.Load("SwitchOn") as Material;
            }
        }
	}
}
