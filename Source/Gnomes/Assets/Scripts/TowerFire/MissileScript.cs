using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {
    public Rigidbody missile;
    public float speed = 3f;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody clone = (Rigidbody) Instantiate(missile, transform.position, transform.rotation);
            clone.velocity = transform.forward * speed;
        }
	}
}
