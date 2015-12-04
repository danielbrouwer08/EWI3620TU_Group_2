using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

    private Rigidbody rb;
    public float floatvelocity = 0.8f;
    private bool floating = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire3Player"))
        {
            if (floating != true)
            {
                floating = true;
            }
            else
            {
                floating = false;
            }
        }
        if (floating && rb.velocity.y < -floatvelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, -floatvelocity, rb.velocity.z);
        }
    }
}
