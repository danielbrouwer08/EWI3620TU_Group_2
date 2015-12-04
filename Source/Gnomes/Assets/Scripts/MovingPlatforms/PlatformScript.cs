using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {
    public Transform[] waypoints;
    private int curwaypoint = 0;
    public float speed = 1;
    public SwitchScript switcher;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (switcher.switchon)
        {
            //Check if you are on a waypoint, if not:
            if (transform.position != waypoints[curwaypoint].position)
            {
                Vector3 vec = Vector3.MoveTowards(transform.position, waypoints[curwaypoint].position, speed);
                transform.position = vec;
            }
            //if a waypoint is reached:
            else curwaypoint = (curwaypoint + 1) % waypoints.Length;
        }


    }
}
