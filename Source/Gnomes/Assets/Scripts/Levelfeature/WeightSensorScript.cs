using UnityEngine;
using System.Collections;

public class WeightSensorScript : MonoBehaviour {
    public Transform rotator;
    public int objectCount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("aantal objecten op mij is:" + objectCount);
        if(objectCount > 0)
        {
            //Debug.Log("Ik ben aan het draaien");
            Vector3 rotation = Vector3.RotateTowards(rotator.transform.forward, new Vector3(0, 0, 1), 0.6f * Time.deltaTime, 0.0f);
            rotator.transform.forward = rotation;
        }
        if(objectCount == 0){
            //Debug.Log("Ik ga weer terug draaien");
            Vector3 rotation = Vector3.RotateTowards(rotator.transform.forward, new Vector3(0, 1, 0), 0.6f * Time.deltaTime, 0.0f);
            rotator.transform.forward = rotation;
        }
 
    }

    void OnCollisionEnter(Collision other)
    {

        objectCount += 1;

    }

    void OnCollisionExit(Collision other)
    {
        objectCount -= 1;
        Debug.Log(objectCount);

    }
}
