using UnityEngine;
using System.Collections;

public class StoneLevel11 : MonoBehaviour {

	private Rigidbody rb;
	public Vector3 force;
	private Vector3 startpos;
	private float x_stop_dist = 20;
	public bool move;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		startpos = transform.position;
		force = new Vector3(0,0,0);
		move = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(transform.position.x>startpos.x + x_stop_dist)
		{
			rb.velocity = new Vector3(0,0,0);
			rb.isKinematic = true; //make rb kinematic
			move = false;
		}else if(move)
		{
			rb.AddForce(force);
		}
	}

//	void OnCollisionEnter (Collision col)
//	{
//		if (col.gameObject.CompareTag ("Hammer")) {
//			force = new Vector3(10000000,0,0);
//			move=true;
//		}
//	}

}
