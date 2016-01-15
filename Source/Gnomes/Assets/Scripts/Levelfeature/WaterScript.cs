﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WaterScript : MonoBehaviour
{
    private List<Transform> waypoints = new List<Transform>();
    private List<float> distances;
    public Transform WaypointList;
    public float maxWaypointDistance = 20;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in WaypointList)
        {
            foreach (Transform waypoint in child)
            {
                waypoints.Add(waypoint);
            }
        }
    }

    //make items 
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().walkSpeed = 2;
            other.GetComponent<PlayerController>().runSpeed = 2;
            other.GetComponent<PlayerController>().jumpForce = 2;

            List<Transform> closestWaypoints = FindClosestWaypoint(other);
            Vector3 total = new Vector3(0, 0, 0);
            for (int i = 0; i < closestWaypoints.Count; i++)
            {
                total += (closestWaypoints[i].transform.forward / distances[i]) * closestWaypoints[i].gameObject.GetComponent<WaterWaypointScript>().force;
            }
            total = total / total.magnitude;
            Debug.DrawRay(other.transform.position, total * 3);
            float force = 12000;
            other.gameObject.GetComponent<Rigidbody>().AddForce(total * force);
        }
		else if(other.GetComponent<Rigidbody>()!=null)
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            List<Transform> closestWaypoints = FindClosestWaypoint(other);
            Vector3 total = new Vector3(0, 0, 0);
            for (int i = 0; i < closestWaypoints.Count; i++)
            {
                total += (closestWaypoints[i].transform.forward / distances[i]) * closestWaypoints[i].gameObject.GetComponent<WaterWaypointScript>().force;
            }
            total = total / total.magnitude;
            Debug.DrawRay(other.transform.position, total * 3);
            float force = 500;
            other.gameObject.GetComponent<Rigidbody>().AddForce(total * force);
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().walkSpeed = other.GetComponent<PlayerController>().walkSpeedbegin;
            other.GetComponent<PlayerController>().runSpeed = other.GetComponent<PlayerController>().runSpeedbegin;
            other.GetComponent<PlayerController>().jumpForce = other.GetComponent<PlayerController>().jumpForcebegin;
        }
		else if(other.GetComponent<Rigidbody>()!=null)
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    List<Transform> FindClosestWaypoint(Collider other)
    {
        distances = new List<float>();
        List<Transform> closestWaypoints = new List<Transform>();
        Vector3 position = other.transform.position;
        foreach (Transform cur in waypoints)
        {
            cur.GetComponent<WaterWaypointScript>().close = false;
            Vector3 diff = cur.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < maxWaypointDistance)
            {
                closestWaypoints.Add(cur);
                distances.Add(curDistance);
                cur.GetComponent<WaterWaypointScript>().close = true;
            }
        }
        
        return closestWaypoints;
    }
}
