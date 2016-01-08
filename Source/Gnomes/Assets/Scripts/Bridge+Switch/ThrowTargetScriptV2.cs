using UnityEngine;
using System.Collections;

public class ThrowTargetScriptV2 : MonoBehaviour {
    public GameObject switcher;
    public GameObject triggereditem;

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("ThrowingItem") && switcher.GetComponent<SwitchScript>().switchon)
        {
			triggereditem.GetComponent<Stairway>().appear = true;
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
