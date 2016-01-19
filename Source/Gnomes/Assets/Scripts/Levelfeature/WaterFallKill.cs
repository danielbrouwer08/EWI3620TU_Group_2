using UnityEngine;
using System.Collections;

public class WaterFallKill : MonoBehaviour {
	public float force = 100.0f;
	private float timer = 0.0f;
	private float killTime = 4.0f;

	// Update is called once per frame
	void Update () {
		timer = timer + Time.deltaTime;
		if(transform.position.y < - 50  || timer > killTime)
		{
			Destroy(this.gameObject);
		}

	}

	void OnCollisionStay(Collision collisionInfo) {
		if(collisionInfo.collider.tag == "Player")
		{
			collisionInfo.gameObject.GetComponent<PlayerController>().ExternalForce(new Vector3(0.0f,0.0f,-force),1.0f);
		}
	}

}
