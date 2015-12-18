using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

    public float flycap = 50;
    private Rigidbody rb;
    private float fly = 0;
    public float flyvelocity = 5;
    private int playerNum;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        playerNum = GetComponent<PlayerController>().playerNum;
    }
	
	void FixedUpdate ()
    {
		if (Input.GetButton("Item" + playerNum))
        {
            float velocityold = rb.velocity.y;
            rb.velocity = new Vector3(rb.velocity.x, flyvelocity, rb.velocity.z);
            fly = fly - velocityold + rb.velocity.y;
            Debug.Log(transform.GetChild(3));
            transform.GetChild(3).GetComponent<Transform>().localEulerAngles += new Vector3(0, 20, 0);
        }
    }

    void OnCollisionEnter()
    {
        if(gameObject.GetComponent<PlayerController>().grounded())
        {
            fly = 0;
        }
    }
}
