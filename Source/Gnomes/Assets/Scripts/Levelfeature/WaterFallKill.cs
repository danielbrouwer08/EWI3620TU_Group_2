using UnityEngine;
using System.Collections;

public class WaterFallKill : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
		if(transform.position.y < - 50)
		{
			Destroy(this.gameObject);
		}

	}

}
