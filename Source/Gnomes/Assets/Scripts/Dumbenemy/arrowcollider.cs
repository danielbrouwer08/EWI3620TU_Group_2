using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class arrowcollider : MonoBehaviour {

    private Rigidbody rb;
    public float shootSpeed;
    private float starttime;
    private float lifetime;
    public float damage;

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

        if(other.gameObject.tag == "DumbEnemy")
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            other.GetComponent<PlayerProperties>().TakeDamage(damage);

            Analytics.CustomEvent("playerHit", new Dictionary<string, object>
            {
                { "x-location",  gameObject.transform.position.x},
                { "z-location", gameObject.transform.position.z},
                { "playerNum", other.gameObject.GetComponent<PlayerController>().playerNum }
            });

        }

        Destroy(gameObject);

        
    }
}
