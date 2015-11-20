using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
    private float speed = 10;
    public Transform target;

    // Use this for initialization
    void Start()
    {
        new GameObject()
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        if (transform.position.Equals(target.position))
        {
            Destroy(gameObject);
        }
    }
}
