using UnityEngine;
using System.Collections;

public class Puzzle7_AI_Boss : MonoBehaviour
{
	private NavMeshAgent agent;
	private bool player1InRange;
	private bool player2InRange;
	private bool cheeseInRange;
	private GameObject player1;
	private GameObject player2;
	private GameObject[] players = new GameObject[2];
	private GameObject cheese;
	public GameObject MouseNavagent;
	public GameObject leftWall;
	public GameObject rightWall;
	public float noticeRadius;
	public float damage;
	public float Knockback;
	public float nomovementtime;
	private Animation animation;
	public float takeActionDistance;
	public float eatingTime;
	public float attackTime;
	public float thinkTime;
	private bool performingAction = false;
	private string goingAfter;
	private float eatingTimer = 0.0f;
	private float attackTimer = 0.0f;
	private float thinkTimer = 0.0f;
	public int health = 100;
	private string actionState;
	private bool wallsAreUp = false;

	//public float attackAnimationSpeed;

	void Awake ()
	{
		animation = this.GetComponent<Animation> ();
		agent = MouseNavagent.GetComponent<NavMeshAgent> ();
		player1InRange = false;
		player2InRange = false;
		cheeseInRange = false;
		players = GameObject.FindGameObjectsWithTag ("Player");
		player1 = players [0];
		player2 = players [1];

		//animation["Attack_01"].speed = attackAnimationSpeed;

		actionState = "idle";

		//animation.Play ("idle");
		//start = transform.position;
	}

	void determineInrangeObjects ()
	{
		cheese = GameObject.FindGameObjectWithTag ("cheese");
		
		if (cheese != null) {
			bool x_pos_cheese = (cheese.transform.position.x < transform.position.x + noticeRadius && cheese.transform.position.x > transform.position.x - noticeRadius);
			bool y_pos_cheese = (cheese.transform.position.y < transform.position.y + noticeRadius && cheese.transform.position.y > transform.position.y - noticeRadius);
			bool z_pos_cheese = (cheese.transform.position.z < transform.position.z + noticeRadius && cheese.transform.position.z > transform.position.z - noticeRadius);
			cheeseInRange = (x_pos_cheese && y_pos_cheese && z_pos_cheese);
		} else {
			cheeseInRange = false;
		}
		
		//check if one of the players is in range.
		bool x_pos_player1 = (player1.transform.position.x < transform.position.x + noticeRadius && player1.transform.position.x > transform.position.x - noticeRadius);
		bool y_pos_player1 = (player1.transform.position.y < transform.position.y + noticeRadius && player1.transform.position.y > transform.position.y - noticeRadius);
		bool z_pos_player1 = (player1.transform.position.z < transform.position.z + noticeRadius && player1.transform.position.z > transform.position.z - noticeRadius);
		bool x_pos_player2 = (player2.transform.position.x < transform.position.x + noticeRadius && player2.transform.position.x > transform.position.x - noticeRadius);
		bool y_pos_player2 = (player2.transform.position.y < transform.position.y + noticeRadius && player2.transform.position.y > transform.position.y - noticeRadius);
		bool z_pos_player2 = (player2.transform.position.z < transform.position.z + noticeRadius && player2.transform.position.z > transform.position.z - noticeRadius);
		player1InRange = (x_pos_player1 && y_pos_player1 && z_pos_player1);
		player2InRange = (x_pos_player2 && y_pos_player2 && z_pos_player2);
	}

	void decideWhereToGo ()
	{
		//determine where to go after
		if (cheeseInRange) {
			actionState = "huntCheese";
			if(!wallsAreUp) //put wall up, if not done already
			{
				leftWall.GetComponent<WoodenPoleWall>().appear = true;
				rightWall.GetComponent<WoodenPoleWall>().appear = true;
				wallsAreUp = true;
			}
		} else if (player1InRange || player2InRange) {
			if(!wallsAreUp) //put wall up, if not done already
			{
				leftWall.GetComponent<WoodenPoleWall>().appear = true;
				rightWall.GetComponent<WoodenPoleWall>().appear = true;
				wallsAreUp = true;
			}
			if (Vector3.Distance (player1.transform.position, agent.transform.position) < Vector3.Distance (player2.transform.position, agent.transform.position)) { //if closer to player 1
				actionState = "huntPlayer1"; //hunt player 1 down
			} else {
				actionState = "huntPlayer2"; //else hunt player 2 down
			}
		} else {
			actionState = "idle";
		}
	}
	
	void FixedUpdate ()
	{
		//print ("current state: " + actionState);
	
		switch (actionState) {
		case "idle":
			{
				agent.Stop ();
				animation.Play ("idle");
				determineInrangeObjects ();
				
				thinkTimer += Time.deltaTime; //increment the eatingTimer
				if (thinkTimer >= thinkTime) { //simulate thinking time of the mouse
					decideWhereToGo ();
					thinkTimer = 0.0f; //reset the think timer
				}
				break;
			}
		case "huntCheese":
			{
				agent.SetDestination (cheese.transform.position);
				agent.Resume ();
				animation.Play ("Walk");
				if (Vector3.Distance (cheese.transform.position, agent.transform.position) < takeActionDistance) { //when the mouse reaches the cheese
					actionState = "eatCheese"; //change the state to eatCheese
				}
				break;
			}
		case "eatCheese":
			{
				agent.Stop ();
				//animation.Play("eat");
				eatingTimer += Time.deltaTime; //increment the eatingTimer
				if (health < 100) {
					health++; //regain health while eating.
				}
				if (eatingTimer >= eatingTime) {
					Destroy (cheese); //destroy the cheese gameobject
					actionState = "idle"; //when cheese is gone, goto idle state.
					eatingTimer = 0.0f; //reset the eatingtimer
				}
				if(Vector3.Distance (cheese.transform.position, agent.transform.position) > takeActionDistance) //if cheese its position is moved out of the eatinrange, stop eating.
				{
					actionState = "idle";
				}
				break;
			}
		case "huntPlayer1":
			{
				agent.SetDestination (player1.transform.position);
				agent.Resume ();
				animation.Play ("Walk");

				if (Vector3.Distance (player1.transform.position, agent.transform.position) > Vector3.Distance (player2.transform.position, agent.transform.position)) { //if during the hunt, player2 gets closer change the target
					actionState = "huntPlayer2"; //hunt player 2 down
				}
				
				if (Vector3.Distance (player1.transform.position, agent.transform.position) < takeActionDistance) { //when the mouse reaches player1
					animation.Play ("Attack_01"); //play the attack animation
					actionState = "attackPlayer1"; //change the state to eatCheese
				}
				break;
			}
		case "attackPlayer1":
			{
				agent.Stop ();
				attackTimer += Time.deltaTime;
				if (attackTimer >= attackTime) { //wait till animation for attack is finished
					//hit player1
					//print ("hitting player 1");
					player1.GetComponent<PlayerProperties> ().TakeDamage (damage);
				player1.GetComponent<PlayerController> ().ExternalForce ((player1.transform.position - agent.transform.position)/Vector3.Distance(player1.transform.position,agent.transform.position) * Knockback, nomovementtime);
					actionState = "idle"; //return to the idle state after attack
					attackTimer = 0.0f; //reset the attacktimer
				}
				break;
			}
		case "huntPlayer2":
			{
				agent.SetDestination (player2.transform.position);
				agent.Resume ();
				animation.Play ("Walk");

				if (Vector3.Distance (player1.transform.position, agent.transform.position) < Vector3.Distance (player2.transform.position, agent.transform.position)) { //if during the hunt, player2 gets closer change the target
					actionState = "huntPlayer1"; //hunt player 2 down
				}

				if (Vector3.Distance (player2.transform.position, agent.transform.position) < takeActionDistance) { //when the mouse reaches player1
					animation.Play ("Attack_01"); //play the attack animation
					actionState = "attackPlayer2"; //change the state to eatCheese
				}
				break;
			}
		case "attackPlayer2":
			{
				agent.Stop ();
				attackTimer += Time.deltaTime;
				if (attackTimer >= attackTime) { //wait till animation for attack is finished
					//hit player2
					player2.GetComponent<PlayerProperties> ().TakeDamage (damage);
					//player2.GetComponent<Rigidbody>().AddForce((player2.transform.position - transform.position) * Knockback);
				player2.GetComponent<PlayerController> ().ExternalForce ((player2.transform.position - agent.transform.position)/Vector3.Distance(player1.transform.position,agent.transform.position) * Knockback, nomovementtime);
					actionState = "idle"; //return to the idle state after attack
					attackTimer = 0.0f; //reset the attacktimer
				}
				break;
			}

		default:
			{
				actionState = "idle"; //deafault -> return to idle.
				break;
			}


		}

	
	}

//	void OnCollisionEnter (Collision col)
//	{
//		if (col.gameObject.CompareTag ("cheese")) {
//			Destroy (col.gameObject); //destroy the cheese with certain time delay
//		}
//
//		if (col.gameObject.CompareTag ("Player")) {
//			col.gameObject.GetComponent<PlayerProperties> ().TakeDamage (damage);
//			col.gameObject.GetComponent<PlayerController> ().ExternalForce ((col.gameObject.transform.position - transform.position) * Knockback, nomovementtime);
//		}
//	}
}
