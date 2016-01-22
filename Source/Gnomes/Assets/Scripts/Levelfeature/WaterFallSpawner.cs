using UnityEngine;
using System.Collections;

public class WaterFallSpawner : MonoBehaviour
{
	public GameObject WaterFall;
	public float spawnTime = 0.1f;
	public float waterwidth = 24;
	public bool spawnnow = true;
	private GameObject[] player;
	public float playerInRangeDistance = 80;
	public float spawnheight = 5.0f;
	public float spawn_z_offset = -2.0f;
	public float speed = 10.0f;
	public bool running = false;
	// Use this for initialization

	// Update is called once per frame
	void Start()
	{
		player = GameObject.FindGameObjectsWithTag("Player");
		spawn_z_offset = -1.0f;
		//StartCoroutine(waterSpawner());
	}

	void Update()
	{
		//only start the waterfall if player is close for performance.
		if(running==false && ((player[0].transform.position.x>this.transform.position.x-playerInRangeDistance && player[0].transform.position.x<this.transform.position.x+playerInRangeDistance) || (player[1].transform.position.x>this.transform.position.x-playerInRangeDistance && player[1].transform.position.x<this.transform.position.x+playerInRangeDistance)))
		{
			spawnnow = true;
			StartCoroutine(waterSpawner());
		}else
		{
			spawnnow = false;
		}
	}


	IEnumerator waterSpawner()
	{
		running = true;
		while(spawnnow)
		{
			yield return new WaitForSeconds(spawnTime);
			Vector3 position = new Vector3 (0.0f, spawnheight, spawn_z_offset) + this.transform.position;
			GameObject temp = (GameObject)GameObject.Instantiate (WaterFall, position, Quaternion.Euler (0.0f, 0.0f, 0.0f));
			temp.transform.localScale = temp.transform.localScale + new Vector3((waterwidth/2),0.0f,0.0f);
			//float speed = Random.Range(20.0f,25.0f); //give water random speed

			temp.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f,0.0f,-speed),ForceMode.VelocityChange);
		}
		running = false;
	}
	
}
