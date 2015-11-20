using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {
    public GameObject missile;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(missile, transform.position, transform.rotation);
        }
	}
}
