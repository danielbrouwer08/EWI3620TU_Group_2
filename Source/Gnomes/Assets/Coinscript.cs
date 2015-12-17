using UnityEngine;
using System.Collections;

public class Coinscript : MonoBehaviour {

    public float rotationSpeed;
    public int score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.Rotate(0, rotationSpeed, 0);

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            other.GetComponent<PlayerProperties>().UpdateScore(score);
            Destroy(gameObject);
        }

    }
}
