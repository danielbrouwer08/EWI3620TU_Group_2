using UnityEngine;
using System.Collections;

public class DumbEnemy : MonoBehaviour {

    public float rotationspeed;
    private bool goback;

    public GameObject pijl;

    private float nextFire;
    public float fireRate;

	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(0, 180, 0);
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(0, rotationspeed, 0);

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 rotation = transform.eulerAngles + new Vector3(0, 180, 0);
            Instantiate(pijl, transform.position + new Vector3(0.2f, 3f, 0.2f), Quaternion.Euler(rotation));
  
        }

    }
}
