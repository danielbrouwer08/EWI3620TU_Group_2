using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public Transform goal;
    private NavMeshAgent agent;

	// Use this for initialization
	void Awake () {
        agent = GetComponent<NavMeshAgent>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        agent.SetDestination(goal.position);

    }
}
