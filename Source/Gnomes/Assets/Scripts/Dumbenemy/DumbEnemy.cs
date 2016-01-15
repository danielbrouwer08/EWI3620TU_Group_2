using UnityEngine;
using System.Collections;

public class DumbEnemy : MonoBehaviour {

    public float rotationspeed;
    private bool goback;
    private int state;
    public GameObject pijl;
    private EnemySight sight;
    private float nextFire;
    public float fireRate;

	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(0, 180, 0);
        sight = GetComponent<EnemySight>();
        state = Random.Range(0, 4);
	}

    void shootArrow()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 rotation = transform.eulerAngles + new Vector3(0, 180, 0);
            Instantiate(pijl, transform.position + new Vector3(0.2f, 3f, 0.2f), Quaternion.Euler(rotation));

        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        switch (state)
        {
            case 0:
                transform.Rotate(0, rotationspeed, 0);
                shootArrow();
                break;
            case 1:
                if(sight.seenEnemy != null)
                {
                    transform.LookAt(sight.seenEnemy.transform);
                    if(sight.seenEnemy.transform.position.z > transform.position.z)
                    {
                        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 5f, 0);
                    }
                    else if(sight.seenEnemy.transform.position.z < transform.position.z)
                    {
                        transform.eulerAngles = transform.eulerAngles + new Vector3(0, -5f, 0);
                    }
                    shootArrow();
                }
                break;
            case 2:
                transform.LookAt(sight.seenEnemy.transform);
                shootArrow();
                break;
            case 3:
                shootArrow();
                break;
            default:
                break;
        }


    }
}
