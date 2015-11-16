using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

    private Rigidbody rb;
    private PlayerScript ps;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<PlayerScript>();
    }
	
	void FixedUpdate ()
    {
        if (Input.GetButtonDown("Fire1") && Mathf.Abs(rb.velocity.y) >= 0.01)
        {
            rb.AddForce(new Vector3(0.0f, ps.jumpForce, 0.0f));
        }
    }
}
