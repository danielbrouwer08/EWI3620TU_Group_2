using UnityEngine;
using System.Collections;

public class CompanionScript : MonoBehaviour {
    NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }        
        }
        if (agent.isOnOffMeshLink)
        {
            if(agent.velocity.y < 0.0f)
            {
                agent.speed = 8;
            }
            else if(agent.velocity.y > 0.0f)
            {
                agent.speed = 0.5f;
            }
        }
        else
        {
            agent.speed = 6;
        }
    }
}
