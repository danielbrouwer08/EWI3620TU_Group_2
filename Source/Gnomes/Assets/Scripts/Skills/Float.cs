using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

    private Rigidbody rb;
    private int playerNum;
    public float floatvelocity;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        playerNum = GetComponent<PlayerController>().playerNum;
    }
	
	void FixedUpdate ()
    {
        if (Input.GetButton("Item" + playerNum) && rb.velocity.y < -floatvelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, -floatvelocity, rb.velocity.z);
        }
    }
}
