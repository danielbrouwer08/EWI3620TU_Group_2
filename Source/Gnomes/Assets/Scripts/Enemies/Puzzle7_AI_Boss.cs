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
	public float noticeRadius;

	public float damage;
	public float Knockback;
	public float nomovementtime;
	
    void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
        player1InRange = false;
		player2InRange = false;
		cheeseInRange = false;
		players = GameObject.FindGameObjectsWithTag("Player");
		player1 = players[0];
		player2 = players[1];

        //start = transform.position;
	}
	
	void FixedUpdate ()
    {
		cheese = GameObject.FindGameObjectWithTag("cheese");
		if(cheese!=null)
		{
			bool x_pos_cheese = (cheese.transform.position.x < transform.position.x + noticeRadius && cheese.transform.position.x > transform.position.x - noticeRadius);
			bool y_pos_cheese = (cheese.transform.position.y < transform.position.y + noticeRadius && cheese.transform.position.y > transform.position.y - noticeRadius);
			bool z_pos_cheese = (cheese.transform.position.z < transform.position.z + noticeRadius && cheese.transform.position.z > transform.position.z - noticeRadius);
			cheeseInRange = (x_pos_cheese && y_pos_cheese && z_pos_cheese);
		}else{
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


		if(cheeseInRange)
		{
			agent.SetDestination(cheese.transform.position);
		}else if(player1InRange)
		{
			agent.SetDestination(player1.transform.position);
		}else if(player2InRange)
		{
			agent.SetDestination(player2.transform.position);
		}

		print(agent.destination);
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("cheese"))
        {
			Destroy(col.gameObject); //destroy the cheese with certain time delay
		}

		if(col.gameObject.CompareTag("Player"))
		{
			col.gameObject.GetComponent<PlayerProperties>().TakeDamage(damage);
			col.gameObject.GetComponent<PlayerController>().ExternalForce((col.gameObject.transform.position-transform.position)*Knockback,nomovementtime);
		}
    }
}
