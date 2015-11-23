using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    private Rigidbody me;
    private GameObject[] playergo;
    private Rigidbody[] player;

    private Renderer rend;

    void Start ()
    {
        me = GetComponent<Rigidbody>();
        playergo = GameObject.FindGameObjectsWithTag("Player");
        player = new Rigidbody[playergo.Length];
        for(int i = 0; i < playergo.Length; i++)
        {
            player[i] = playergo[i].GetComponent<Rigidbody>();
        }
        rend = GetComponent<Renderer>();

    }
	
    void Update ()
    {
        // Sets its color to red if the enemy sees a player
        if(See())
        {
            rend.material.color = Color.red;
        }
        // Sets its color to blue if the enemy doesn't see a player
        else
        {
            rend.material.color = Color.blue;
        }
    }

    // Returns true if the enemy can see any gameobject tagged with "Player"
	bool See ()
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (Vector3.Magnitude(player[i].position - me.position) < 8)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position + transform.forward * 0.51f, player[i].position - me.position, out hit);
                if (hit.rigidbody == player[i])
                {
                    return true;
                }
            }
        }
        return false;
	}
}
