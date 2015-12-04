using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour {
    private Rigidbody rb;
    public SwitchScript switcher;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}


	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ThrowingItem") && switcher.switchon)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(-5.0f, 0.0f, 0.0f));
        }
    }
}
