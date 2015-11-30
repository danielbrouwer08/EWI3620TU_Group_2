using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update () {

	}
    //make items 
    void OnTriggerStay(Collider other)
    {

        if(other.GetComponent<Rigidbody>().mass < 10)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100));
        }
        else
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100000));
        }

    }
}
