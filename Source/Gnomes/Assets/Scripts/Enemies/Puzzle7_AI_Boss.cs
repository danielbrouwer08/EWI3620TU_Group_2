using UnityEngine;
using System.Collections;

public class Puzzle7_AI_Boss : MonoBehaviour
{
	private AIPath AI;
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
	private Animation mouseAnimation;
	public float takeActionDistance;
	public float eatingTime;
	public float attackTime;
	public float thinkTime;
	//private bool performingAction = false;
	private string goingAfter;
	private float eatingTimer = 0.0f;
	private float attackTimer = 0.0f;
	private float thinkTimer = 0.0f;
	//public int health = 100;
	private string actionState;
	private bool wallsAreUp = false;

	//public float attackAnimationSpeed;

	void Awake ()
	{
		mouseAnimation = this.GetComponent<Animation> ();
		player1InRange = false;
		player2InRange = false;
		cheeseInRange = false;
		players = GameObject.FindGameObjectsWithTag ("Player");
		player1 = players [0];
		player2 = players [1];
        AI = MouseNavagent.GetComponent<AIPath>();
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
			if (!wallsAreUp) { //put wall up, if not done already
				leftWall.GetComponent<WoodenPoleWall> ().appear = true;
				rightWall.GetComponent<WoodenPoleWall> ().appear = true;
				wallsAreUp = true;
			}
		} else if (player1InRange || player2InRange) {
			if (!wallsAreUp) { //put wall up, if not done already
				leftWall.GetComponent<WoodenPoleWall> ().appear = true;
				rightWall.GetComponent<WoodenPoleWall> ().appear = true;
				wallsAreUp = true;
			}
			if (Vector3.Distance (player1.transform.position, AI.transform.position) < Vector3.Distance (player2.transform.position, AI.transform.position)) { //if closer to player 1
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
				mouseAnimation.Play ("idle");
				if (AI.enabled) {
                    AI.canMove = false;
					determineInrangeObjects ();
				
					thinkTimer += Time.deltaTime; //increment the eatingTimer
					if (thinkTimer >= thinkTime) { //simulate thinking time of the mouse
						decideWhereToGo ();
						thinkTimer = 0.0f; //reset the think timer
					}

				} else {
					actionState = "idle";
				}
				break;
			}
		case "huntCheese":
			{
                AI.target = cheese.transform;
                AI.canMove = true;
				mouseAnimation.Play ("Walk");
				if (Vector3.Distance (cheese.transform.position, AI.transform.position) < takeActionDistance) { //when the mouse reaches the cheese
					actionState = "eatCheese"; //change the state to eatCheese
				}
				break;
			}
		case "eatCheese":
			{
                AI.canMove = false;
				//animation.Play("eat");
				eatingTimer += Time.deltaTime; //increment the eatingTimer
				if (this.GetComponent<EnemyProperties> ().health < 100) {
					this.GetComponent<EnemyProperties> ().health++; //regain health while eating.
				}
				if (eatingTimer >= eatingTime) {
					Destroy (cheese); //destroy the cheese gameobject
					actionState = "idle"; //when cheese is gone, goto idle state.
					eatingTimer = 0.0f; //reset the eatingtimer
				}
				if (Vector3.Distance (cheese.transform.position, AI.transform.position) > takeActionDistance) { //if cheese its position is moved out of the eatinrange, stop eating.
					actionState = "idle";
				}
				break;
			}
		case "huntPlayer1":
			{
				AI.target = player1.transform;
                AI.canMove = true;
				mouseAnimation.Play ("Walk");

				if (Vector3.Distance (player1.transform.position, AI.transform.position) > Vector3.Distance (player2.transform.position, AI.transform.position)) { //if during the hunt, player2 gets closer change the target
					actionState = "huntPlayer2"; //hunt player 2 down
				}
				
				if (Vector3.Distance (player1.transform.position, AI.transform.position) < takeActionDistance) { //when the mouse reaches player1
					mouseAnimation.Play ("Attack_01"); //play the attack animation
					actionState = "attackPlayer1"; //change the state to eatCheese
				}
				break;
			}
		case "attackPlayer1":
			{
                AI.canMove = false;
				attackTimer += Time.deltaTime;
				if (attackTimer >= attackTime) { //wait till animation for attack is finished
					//hit player1
					//print ("hitting player 1");
					player1.GetComponent<PlayerProperties> ().TakeDamage (damage);
					player1.GetComponent<PlayerController> ().ExternalForce ((player1.transform.position - AI.transform.position) / Vector3.Distance (player1.transform.position, AI.transform.position) * Knockback, nomovementtime);
					actionState = "idle"; //return to the idle state after attack
					attackTimer = 0.0f; //reset the attacktimer
				}
				break;
			}
		case "huntPlayer2":
			{
                AI.target = player2.transform;
                AI.canMove = true;
				mouseAnimation.Play ("Walk");

				if (Vector3.Distance (player1.transform.position, AI.transform.position) < Vector3.Distance (player2.transform.position, AI.transform.position)) { //if during the hunt, player2 gets closer change the target
					actionState = "huntPlayer1"; //hunt player 2 down
				}

				if (Vector3.Distance (player2.transform.position, AI.transform.position) < takeActionDistance) { //when the mouse reaches player1
					mouseAnimation.Play ("Attack_01"); //play the attack animation
					actionState = "attackPlayer2"; //change the state to eatCheese
				}
				break;
			}
		case "attackPlayer2":
			{
                AI.canMove = false;
				attackTimer += Time.deltaTime;
				if (attackTimer >= attackTime) { //wait till animation for attack is finished
					//hit player2
					player2.GetComponent<PlayerProperties> ().TakeDamage (damage);
					//player2.GetComponent<Rigidbody>().AddForce((player2.transform.position - transform.position) * Knockback);
					player2.GetComponent<PlayerController> ().ExternalForce ((player2.transform.position - AI.transform.position) / Vector3.Distance (player1.transform.position, AI.transform.position) * Knockback, nomovementtime);
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

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.CompareTag ("Player")) {
			col.gameObject.GetComponent<PlayerProperties> ().TakeDamage (damage);
			col.gameObject.GetComponent<PlayerController> ().ExternalForce ((col.gameObject.transform.position - transform.position) * Knockback, nomovementtime);
		}
	}
}
