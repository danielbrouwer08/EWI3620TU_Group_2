using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    private Rigidbody me;
    private GameObject[] playergo;
    private Rigidbody[] player;
    private AIPath AI;
    private Renderer rend;
    public GameObject seenEnemy;
    public Transform leftBound;
    public Transform rightBound;
    public float sightlenght = 15;

    void Start ()
    {
        //me = GetComponent<Rigidbody>();
        AI = GetComponent<AIPath>();
        if(AI != null)
        {
            AI.canSearch = false;
        }
        playergo = GameObject.FindGameObjectsWithTag("Player");
        player = new Rigidbody[playergo.Length];
        for(int i = 0; i < playergo.Length; i++)
        {
            player[i] = playergo[i].GetComponent<Rigidbody>();
        }
        //rend = GetComponent<Renderer>();

    }
	
    void Update ()
    {
        if(See() && AI != null)
        {
            AI.canSearch = true;
            AI.canMove = true;
            AI.target = seenEnemy.transform;
        }
        if (AI != null && AI.target != null)
            if(leftBound != null && rightBound != null)
            {
                if (AI.target.position.x > rightBound.position.x | AI.target.position.x < leftBound.position.x)
                {
                    AI.target = null;
                    AI.canMove = false;
                }
            }

    }

    // Returns true if the enemy can see any gameobject tagged with "Player"
	public bool See ()
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (Vector3.Magnitude(player[i].position - transform.position) < sightlenght)
            {
                RaycastHit hit;
                Debug.DrawLine(transform.position + transform.forward * 1.5f, player[i].position);
                Physics.Raycast(transform.position + transform.forward * 1.5f, player[i].position - transform.position, out hit);
                Debug.Log(hit.rigidbody);
                if (hit.rigidbody == player[i])
                {
                    seenEnemy = playergo[i];
                    return true;
                }
            }
        }
        return false;
	}

    void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Muis raakt player");
            other.transform.GetComponent<PlayerProperties>().health -= 1;
        }
    }
}
