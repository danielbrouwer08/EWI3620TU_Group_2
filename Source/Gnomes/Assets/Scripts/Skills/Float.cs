using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

    private Rigidbody rb;
    public float floatvelocity;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
    {
        if (Input.GetButton("Fire3") && rb.velocity.y < -floatvelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, -floatvelocity, rb.velocity.z);
        }
    }
}
