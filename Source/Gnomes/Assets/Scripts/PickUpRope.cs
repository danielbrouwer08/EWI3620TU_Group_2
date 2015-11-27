using UnityEngine;
using System.Collections;

public class PickUpRope : MonoBehaviour
{
	
	public GameObject player;
	public GameObject companion;
	private Rigidbody rb;
	public float pickupRadius;
	public float x_offset_rope;
	public float y_offset_rope;
	public float z_offset_rope;
	public bool isEndPoint;
	public bool playerGrabbedRope = false;
	public bool companionGrabbedRope = false;
		
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
			
		bool x_pos_player = (player.transform.position.x < transform.position.x + pickupRadius && player.transform.position.x > transform.position.x - pickupRadius);
		bool y_pos_player = (player.transform.position.y < transform.position.y + pickupRadius && player.transform.position.y > transform.position.y - pickupRadius);
		bool z_pos_player = (player.transform.position.z < transform.position.z + pickupRadius && player.transform.position.z > transform.position.z - pickupRadius);
		bool x_pos_companion = (companion.transform.position.x < transform.position.x + pickupRadius && companion.transform.position.x > transform.position.x - pickupRadius);
		bool y_pos_companion = (companion.transform.position.y < transform.position.y + pickupRadius && companion.transform.position.y > transform.position.y - pickupRadius);
		bool z_pos_companion = (companion.transform.position.z < transform.position.z + pickupRadius && companion.transform.position.z > transform.position.z - pickupRadius);
		bool companionInRange = (x_pos_companion && y_pos_companion && z_pos_companion);
		bool playerInRange = (x_pos_player && y_pos_player && z_pos_player);

		if (playerInRange) { //if player is in the neighborhood
			if (Input.GetButton("Fire3Player")) {
				if(playerGrabbedRope==false && isEndPoint)
				{
					transform.parent = player.transform;
					playerGrabbedRope = true;
				}else if(playerGrabbedRope==true && isEndPoint)
				{
					transform.parent = null;
					playerGrabbedRope = false;
					player.transform.parent = transform;

				}else if(playerGrabbedRope==false && !isEndPoint)
				{
					playerGrabbedRope= true;
					//transform.parent = player.transform.parent;
					//player.AddComponent<HingeJoint>().connectedBody = rb;
					//player.GetComponent<HingeJoint>().autoConfigureConnectedAnchor = true;
				}else{
					playerGrabbedRope = false;
					//transform.parent = null;
					//Destroy(player.GetComponent<HingeJoint>());
				}


			}
		} else if (companionInRange) { //if companion is in the neighborhood
			if (Input.GetButton("Fire3Companion")) {
				if(playerGrabbedRope==false && isEndPoint)
				{
					transform.parent = companion.transform;
					companionGrabbedRope = true;
				}else if(companionGrabbedRope==true && isEndPoint)
				{
					transform.parent = null;
					companionGrabbedRope = false;
					companion.transform.parent = transform;
					
				}else if(companionGrabbedRope==false && !isEndPoint)
				{
					companionGrabbedRope= true;
					//transform.parent = companion.transform.parent;

					//companion.AddComponent<HingeJoint>().connectedBody = rb;
					//companion.GetComponent<HingeJoint>().autoConfigureConnectedAnchor = true;
				}else{
					companionGrabbedRope = false;
					//transform.parent = null;
					//Destroy(companion.GetComponent<HingeJoint>());
				}
			
			}
		}



		if (playerGrabbedRope) {
			if (isEndPoint) {
				transform.localPosition=new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);
			} else {
				player.transform.position = transform.position + new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //player follows rope
				//player.transform.localPosition = new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);
			}

		} else if (companionGrabbedRope) {
			if (isEndPoint) {
				transform.localPosition=new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);

			} else {
				//companion.transform.localPosition=new Vector3(x_offset_rope, y_offset_rope, z_offset_rope);
				companion.transform.position = transform.position + new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //player follows rope
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


