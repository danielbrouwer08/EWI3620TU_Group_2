using UnityEngine;
using System.Collections;

public class WibWabScript : MonoBehaviour {
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    
	}

    void OnCollisionStay(Collision other)
    {
        if(other.transform.position.z > 28.62f)
        {
            //Debug.Log("groter");
            rb.AddTorque(10000f, 0f, 0f);
        }
        else if(other.transform.position.z < 28.62f)
        {
            //Debug.Log("kleiner");
            rb.AddTorque(-10000f,0f, 0f);
        }
    }
}
