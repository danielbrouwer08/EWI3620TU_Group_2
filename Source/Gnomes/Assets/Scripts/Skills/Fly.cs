using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

    public float flycap = 3;
    private Rigidbody rb;
    private PlayerScript ps;
    private float fly = 0;
    public float flyvelocity;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<PlayerScript>();
    }
	
	void FixedUpdate ()
    {
        if (Input.GetButton("Fire3") && fly < flycap)
        {
            float velocityold = rb.velocity.y;
            rb.velocity = new Vector3(rb.velocity.x, flyvelocity, rb.velocity.z);
            fly = fly - velocityold + rb.velocity.y;
            Debug.Log(fly);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if(Mathf.Abs(rb.velocity.y) < 0.01)
        {
            fly = 0;
        }
    }
}
