using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
    private GameObject target;
    public float speed = 3f;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("TowerTarget");
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.GetComponent<Transform>().position, step);
        if(Vector3.Distance(transform.position, target.GetComponent<Transform>().position) < 0.0001)
        {
            Destroy(gameObject);
        }
    }
}
