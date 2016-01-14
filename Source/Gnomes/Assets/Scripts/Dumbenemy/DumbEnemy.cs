using UnityEngine;
using System.Collections;

public class DumbEnemy : MonoBehaviour {

    public float rotationspeed;
    private bool goback;
    private int state;
    public GameObject pijl;

    private float nextFire;
    public float fireRate;

	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(0, 180, 0);
        state = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        switch (state)
        {
            case 0:
                transform.Rotate(0, rotationspeed, 0);

                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Vector3 rotation = transform.eulerAngles + new Vector3(0, 180, 0);
                    Instantiate(pijl, transform.position + new Vector3(0.2f, 3f, 0.2f), Quaternion.Euler(rotation));

                }
                break;
            case 1:
                //Vector3 new_rotation = Vector3.RotateTowards(transform.position, )
                break;
            default:
                break;
        }


    }
}
