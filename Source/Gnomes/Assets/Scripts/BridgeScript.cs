using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour {
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(true);
            rb.isKinematic = false;
            rb.AddForce(new Vector3(-5.0f, 0.0f, 0.0f));
        }
	}
}
