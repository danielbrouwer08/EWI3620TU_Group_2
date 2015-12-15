using UnityEngine;
using System.Collections;

public class arrowcollider : MonoBehaviour {

    private Rigidbody rb;
    public float shootSpeed;
    private float starttime;
    private float lifetime;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-1 * transform.forward * shootSpeed);
        starttime = Time.time;
        lifetime = 5;	

	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - starttime >= lifetime)
        {
            Destroy(gameObject);
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");

        if(other.gameObject.tag == "DumbEnemy")
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
        }

        Destroy(gameObject);

        
    }
}
