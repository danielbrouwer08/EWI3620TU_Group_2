using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public static Vector3 location1;
    public static Vector3 location2;
    public float speed = 5;
    
    private bool aware = false;
    private Vector3 targetlocation = location1;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        if(!aware)
        {
            Patternwalk();
        }    
	}

    void Patternwalk()
    {
        rb.AddRelativeForce(Vector3.forward*speed);
        if((targetlocation - rb.position).magnitude < 1)
        {
            targetlocation = 
        }
    }

}
