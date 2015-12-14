﻿using UnityEngine;
using System.Collections;

public class LeafScript : MonoBehaviour {

	// Use this for initialization
	void Awake() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
            Physics.IgnoreCollision(transform.GetComponent<Collider>(), players[i].GetComponent<Collider>());
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.z > 50)
        {
            Destroy(gameObject);
        }
	}
}
