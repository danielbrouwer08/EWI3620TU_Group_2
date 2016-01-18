using UnityEngine;
using System.Collections;

public class PickUpRope : MonoBehaviour
{
	
	public GameObject player;
    //private float playerNum;
	public GameObject player2;
    private GameObject chain;
	private Rigidbody rb;
	public float pickupRadius;
	public float x_offset_rope;
	public float y_offset_rope;
	public float z_offset_rope;
	public bool isEndPoint;
	public bool playerGrabbedRope = false;
	public bool player2GrabbedRope = false;
    private Vector3 beginpositionchain;
    private Vector3 beginpositioncapsule;
		
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = GameObject.Find("kabouterdun");
        player2 = GameObject.Find("kabouterdik");
        chain = GameObject.Find("chain");
        beginpositionchain = chain.transform.position;
        beginpositioncapsule = transform.position;
        Debug.Log(beginpositioncapsule);
        Debug.Log(beginpositionchain);
    }
	
	// Update is called once per frame
	void Update ()
	{
        bool x_pos_player = (player.transform.position.x < transform.position.x + pickupRadius && player.transform.position.x > transform.position.x - pickupRadius);
		bool y_pos_player = (player.transform.position.y < transform.position.y + pickupRadius && player.transform.position.y > transform.position.y - pickupRadius);
		bool z_pos_player = (player.transform.position.z < transform.position.z + pickupRadius && player.transform.position.z > transform.position.z - pickupRadius);
		bool x_pos_player2 = (player2.transform.position.x < transform.position.x + pickupRadius && player2.transform.position.x > transform.position.x - pickupRadius);
		bool y_pos_player2 = (player2.transform.position.y < transform.position.y + pickupRadius && player2.transform.position.y > transform.position.y - pickupRadius);
		bool z_pos_player2 = (player2.transform.position.z < transform.position.z + pickupRadius && player2.transform.position.z > transform.position.z - pickupRadius);
		bool player2InRange = (x_pos_player2 && y_pos_player2 && z_pos_player2);
		bool playerInRange = (x_pos_player && y_pos_player && z_pos_player);

		if (playerInRange) { //if player is in the neighborhood
			if (Input.GetButton("Interact1")) {
				if(playerGrabbedRope==false && isEndPoint)
				{
                    transform.parent = player.transform;
					playerGrabbedRope = true;
                    player.GetComponent<PlayerController>().pickedrope = true;

                }
                else if(playerGrabbedRope==true && isEndPoint)
				{
					transform.parent = null;
					playerGrabbedRope = false;
					player.transform.parent = transform;
                    player.GetComponent<PlayerController>().pickedrope = false;
                }
                else if(playerGrabbedRope==false && !isEndPoint)
				{
					playerGrabbedRope= true;
                    player.GetComponent<PlayerController>().pickedrope = true;
					//transform.parent = player.transform.parent;
					//player.AddComponent<HingeJoint>().connectedBody = rb;
					//player.GetComponent<HingeJoint>().autoConfigureConnectedAnchor = true;
				}else{
                    player.GetComponent<PlayerController>().pickedrope = false;
                    playerGrabbedRope = false;
					//transform.parent = null;
					//Destroy(player.GetComponent<HingeJoint>());
				}


			}
		} else if (player2InRange) { //if player2 is in the neighborhood
            if ((Input.GetButton("Interact2")) || (Input.GetButton("Interact1") && player2.GetComponent<PlayerController>().isSinglePlayer && player2.GetComponent
                <PlayerController>().isActive)) {
				if(playerGrabbedRope==false && isEndPoint)
				{
                    player2.GetComponent<PlayerController>().pickedrope = true;
                    transform.parent = player2.transform;
					player2GrabbedRope = true;
				}else if(player2GrabbedRope==true && isEndPoint)
				{
                    player2.GetComponent<PlayerController>().pickedrope = false;
                    transform.parent = null;
					player2GrabbedRope = false;
					player2.transform.parent = transform;
					
				}else if(player2GrabbedRope==false && !isEndPoint)
				{
                    player2.GetComponent<PlayerController>().pickedrope = true;
                    player2GrabbedRope = true;
					//transform.parent = player2.transform.parent;

					//player2.AddComponent<HingeJoint>().connectedBody = rb;
					//player2.GetComponent<HingeJoint>().autoConfigureConnectedAnchor = true;
				}else{
                    player2.GetComponent<PlayerController>().pickedrope = false;
                    player2GrabbedRope = false;
					//transform.parent = null;
					//Destroy(player2.GetComponent<HingeJoint>());
				}
			
			}
		}

        if(player2.GetComponent<PlayerController>().pickedrope && player.GetComponent<PlayerController>().pickedrope)
        {
            player2.GetComponent<PlayerController>().pickedrope = false;
            player.GetComponent<PlayerController>().pickedrope = false;
        }

		if (playerGrabbedRope) {
			if (isEndPoint) {
				transform.localPosition=new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);
			} else {
				player.transform.position = transform.position + new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //player follows rope
				//player.transform.localPosition = new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);
			}

		} else if (player2GrabbedRope) {
			if (isEndPoint) {
				transform.localPosition=new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);

			} else {
				//player2.transform.localPosition=new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);
				player2.transform.position = transform.position + new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //player follows rope
			}
		}




	}

	void OnCollisionEnter(Collision collision) {
		if(isEndPoint && rb.isKinematic == false)
		{
			rb.isKinematic = true;
		}
	}
			
	

}


