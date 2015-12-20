using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class WaterHealth : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player in water");

            other.GetComponent<PlayerProperties>().TakeDamage(damage);

            Analytics.CustomEvent("playerInWater", new Dictionary<string, object>
            {
                { "x-location",  gameObject.transform.position.x},
                { "z-location", gameObject.transform.position.z},
                { "playerNum", other.gameObject.GetComponent<PlayerController>().playerNum }
            });

        }

    }
}
