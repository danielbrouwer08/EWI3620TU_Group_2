using UnityEngine;
using System.Collections;

public class WallOfDeath : MonoBehaviour {
	
	private bool triggertrap = true;
	private bool movewall = false;
	private GameObject player1;
	private GameObject player2;
	private GameObject[] players;
	public GameObject terrain;
	private int x_offset = 20;
	private int speed = 30;


	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		player1 = players [0];
		player2 = players [1];
	}

	void Update () {
		
		//Let the player fall out of the level by lifting the terrain so it experiences the use of checkpoints.
		if(triggertrap && (player1.transform.position.x > terrain.transform.position.x + x_offset || player2.transform.position.x > terrain.transform.position.x + x_offset))
		{
			triggertrap = false;
			movewall = true;
		}

		if(movewall)
		{
			transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z - speed*Time.deltaTime);

			if(transform.position.z<0)
			{
				movewall = false;
				Destroy(this.gameObject);
			}

		}

	}
}
