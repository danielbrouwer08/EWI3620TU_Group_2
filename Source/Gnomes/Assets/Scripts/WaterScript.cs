using UnityEngine;
using System.Collections;
using System;

public class WaterScript : MonoBehaviour {
    private float time = 0f;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update () {
        
    }
    //make items 
    void OnTriggerStay(Collider other)
    {
        time += Time.deltaTime;

        if(other.GetComponent<Rigidbody>().mass < 10)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100 * time * time));
        }
        else
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(-rb.velocity.x * 2000, -1000f, 100000 * time * time));
        }
    }

    void OnTriggerExit(Collider other)
    {
        time = 0f;
    }
}
