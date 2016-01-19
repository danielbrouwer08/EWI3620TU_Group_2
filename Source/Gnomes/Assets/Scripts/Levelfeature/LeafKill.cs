using UnityEngine;
using System.Collections;

public class LeafKill : MonoBehaviour {

	private float timer = 0.0f;
	private float killTime = 4.0f;

	// Update is called once per frame
	void Update () {
		timer = timer + Time.deltaTime;
		if(transform.position.y < - 50 || timer > killTime)
		{
			Destroy(this.gameObject);
		}
	}
}
