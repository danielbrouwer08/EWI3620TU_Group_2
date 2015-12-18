using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private Transform goal;
    //private NavMeshAgent agent;
    private int playerinrange;
    public float triggerdistance = 10;
    public float lookdistance = 15;
    private GameObject[] player;
    private Vector3 start;
    private float damage = 20;
    public float Knockback;
    public float nomovementtime;

    void Awake ()
    {
        //agent = GetComponent<NavMeshAgent>();
        playerinrange = -1;
        player = GameObject.FindGameObjectsWithTag("Player");
        start = transform.position;
	}
	
	void FixedUpdate ()
    {
        if(playerinrange == -1)
        {
            for (int i = 0; i < 2; i++)
            {
                if (Vector3.Magnitude(transform.position - player[i].transform.position) < triggerdistance)
                {
                    playerinrange = i;
                    goal = player[playerinrange].transform;
                }
            }
        }
        else
        {
            if(Vector3.Magnitude(transform.position - player[playerinrange].transform.position) > lookdistance)
            {
                playerinrange = -1;
                //agent.SetDestination(start);
            }
            else
            {
                //agent.SetDestination(goal.position);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerProperties>().TakeDamage(damage);
            col.gameObject.GetComponent<PlayerController>().ExternalForce((col.gameObject.transform.position-transform.position)*Knockback,nomovementtime);
        }
    }
}
