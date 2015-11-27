﻿using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    public GameObject prefab;
    public int blockcap;
    private Rigidbody rb;
    private GameObject[] blocks;
    private int i = 0;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blocks = new GameObject[blockcap];
	}
	
	void Update ()
    {
		string Fire3 = null;
		if(this.GetComponent<PlayerController>().playerNum == 1)
		{
			Fire3 = "Fire3Player";
		}else if(this.GetComponent<PlayerController>().playerNum == 2)
		{
			Fire3 = "Fire3Companion";
		}else {
			print ("Player " + this.GetComponent<PlayerController>().playerNum + " is not valid");
		}


		if(Input.GetButtonDown(Fire3))
        {
            Destroy(blocks[i]);
            blocks[i] = Instantiate(prefab, rb.position + transform.forward + new Vector3(0, -0.5f, 0), Quaternion.identity) as GameObject;
            i++;
            if(i >= blockcap)
            {
                i = 0;
            }
        }
	}
}
