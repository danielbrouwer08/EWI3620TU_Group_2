using UnityEngine;
using System.Collections;

public class killMouse : MonoBehaviour {

    public int damage;
    public GameObject hammer;

    private Vector3 position;
    private Quaternion rotation;

	// Use this for initialization
	void Start () {

        position = GetComponent<Transform>().position;
        rotation = GetComponent<Transform>().rotation;
	
	}
	
	void OnTriggerEnter (Collider other) {
	    if(other.gameObject.tag == "Enemy")
        {
            Instantiate(hammer, position, rotation);
            Destroy(gameObject);

            other.gameObject.GetComponent<EnemyProperties>().health = other.gameObject.GetComponent<EnemyProperties>().health - damage; 
        }
	}
}
