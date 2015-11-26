using UnityEngine;
using System.Collections;

public class ThrowTargetScript : MonoBehaviour {
    public GameObject switcher;
    public Rigidbody bridge;

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("ThrowingItem") && switcher.GetComponent<SwitchScript>().switchon)
        {
            bridge.isKinematic = false;
            bridge.AddForce(new Vector3(-5.0f, 0.0f, 0.0f));
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
