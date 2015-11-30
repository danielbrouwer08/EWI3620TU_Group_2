using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

    public float flycap = 50;
    private Rigidbody rb;
    private float fly = 0;
    public float flyvelocity = 5;
	private string Fire3;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		Fire3 = null;
		if(this.GetComponent<PlayerController>().playerNum == 1)
		{
			Fire3 = "Fire3Player";
		}else if(this.GetComponent<PlayerController>().playerNum == 2)
		{
			Fire3 = "Fire3Companion";
		}else {
			print ("Player " + this.GetComponent<PlayerController>().playerNum + " is not valid");
		}


    }
	
	void FixedUpdate ()
    {


		if (Input.GetButton(Fire3))
        {
            float velocityold = rb.velocity.y;
            rb.velocity = new Vector3(rb.velocity.x, flyvelocity, rb.velocity.z);
            fly = fly - velocityold + rb.velocity.y;
            Debug.Log(transform.GetChild(0));
            transform.GetChild(0).GetComponent<Transform>().localEulerAngles += new Vector3(0, 20, 0);
        }
    }

    void OnCollisionStay()
    {
        if(Mathf.Abs(rb.velocity.y) < 0.01)
        {
            fly = 0;
        }
    }
}
