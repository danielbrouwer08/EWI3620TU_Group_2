using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour {

	private bool player1In = false;
	private bool player2In = false;
	private GameObject player1;
	private GameObject player2;
	private int state = 0;

	// Update is called once per frame
	void Update () {

		if(player1In && player2In && (state == 0 || state == 1))
		{
			state = 2;
			this.GetComponent<Renderer>().material = Resources.Load("checkpointstate2") as Material;
			player1In = false;
			player2In = false;
			saveCheckpoint();
		}else if ((player1In || player2In) && state == 0)
		{
			state = 1;
			this.GetComponent<Renderer>().material = Resources.Load("checkpointstate1") as Material;
		}
	}

	void saveCheckpoint(){
		PlayerProperties p1prop = player1.GetComponent<PlayerProperties>();
		PlayerProperties p2prop = player2.GetComponent<PlayerProperties>();
		PlayerController p1cont = player1.GetComponent<PlayerController>();
		PlayerController p2cont = player2.GetComponent<PlayerController>();

		//store p1 health and score
		PlayerPrefs.SetFloat("P1 Health",p1prop.health);
		PlayerPrefs.SetFloat("P1 Score",p1prop.score);

		//store p2 health and score
		PlayerPrefs.SetFloat("P2 Health",p2prop.health);
		PlayerPrefs.SetFloat("P2 Score",p2prop.score);
	
		//store p1 spawn location
		PlayerPrefs.SetFloat("P1 XPOS",transform.position.x - 1.5f);
		PlayerPrefs.SetFloat("P1 YPOS",transform.position.y + 3.0f);
		PlayerPrefs.SetFloat("P1 ZPOS",transform.position.z);

		//store p2 spawn location
		PlayerPrefs.SetFloat("P2 XPOS",transform.position.x + 1.5f);
		PlayerPrefs.SetFloat("P2 YPOS",transform.position.y + 3.0f);
		PlayerPrefs.SetFloat("P2 ZPOS",transform.position.z);

		//store date and time of savefile
		PlayerPrefs.SetString ("TimeStamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		//PlayerPrefs.SetString("TimeStamp",System.DateTime.Now);

	}

	void OnCollisionEnter(Collision collision) {

		if(collision.gameObject.tag.Equals("Player"))
		   {
			if (collision.gameObject.GetComponent<PlayerController>().playerNum == 1)
			{
				player1In = true;
				player1 = collision.gameObject;
			}
			else if (collision.gameObject.GetComponent<PlayerController>().playerNum == 2)
			{
				player2In = true;
				player2 = collision.gameObject;
			}
		
		}
	}

}

