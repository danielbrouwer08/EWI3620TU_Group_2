using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WaterScript : MonoBehaviour
{
    private float time = 0f;
    private List<Transform> waypoints = new List<Transform>();
    public Transform WaypointList;

    // Use this for initialization
    void Start()
    {
        foreach(Transform child in WaypointList)
        {
            foreach(Transform waypoint in child)
            {
                waypoints.Add(waypoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //make items 
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().walkSpeed = 2;
            other.GetComponent<PlayerController>().runSpeed = 2;

        }
        GameObject closestWaypoint = FindClosestWaypoint(other);
        float force = closestWaypoint.GetComponent<WaterWaypointScript>().force;
        other.gameObject.GetComponent<Rigidbody>().AddForce(closestWaypoint.transform.forward * force);

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().walkSpeed = 8;
            other.GetComponent<PlayerController>().runSpeed = 15;

        }
    }

    GameObject FindClosestWaypoint(Collider other)
    {

        Transform closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = other.transform.position;
        foreach (Transform cur in waypoints)
        {
            Vector3 diff = cur.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = cur;
                distance = curDistance;
            }
        }
        return closest.gameObject;
    }
}
