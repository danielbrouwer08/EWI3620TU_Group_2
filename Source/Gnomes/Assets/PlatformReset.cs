using UnityEngine;
using System.Collections;

public class PlatformReset : MonoBehaviour {
    public GameObject[] player;
    public Transform[] waypoints;
    private int curwaypoint = 0;
    public float speed = 1;
    public SwitchScript[] switcher;

    void Start ()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }
	
	void Update ()
    {
        if(!switcher[0].switchon && !switcher[1].switchon)
        {
            if (transform.position.x - player[0].transform.position.x > 10 && transform.position.x - player[1].transform.position.x > 10)
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
}
