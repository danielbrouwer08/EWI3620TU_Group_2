using UnityEngine;
using System.Collections;

public class WoodenPoleWall : MonoBehaviour {
	
	public bool appear = false;
	public float appearSpeed = 30;
	public float heightIncrease = 10;
	private float finalYPosition;

	void Start(){
		finalYPosition = transform.position.y + heightIncrease;
	}

	// Update is called once per frame
	void Update () {

		//print ("appear: " + appear);
		if(appear && transform.position.y < finalYPosition)
		{
			transform.position = transform.position + new Vector3(0.0f,appearSpeed*Time.deltaTime,0.0f);
		}
	}
}
