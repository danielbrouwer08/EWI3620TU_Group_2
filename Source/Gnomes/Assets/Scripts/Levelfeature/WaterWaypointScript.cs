using UnityEngine;
using System.Collections;

public class WaterWaypointScript : MonoBehaviour {
    public float force;
    public bool close = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward * 3);
        if (close == true)
        {
            Debug.DrawRay(transform.position, transform.forward * 3, Color.red);
        }

    }
}
