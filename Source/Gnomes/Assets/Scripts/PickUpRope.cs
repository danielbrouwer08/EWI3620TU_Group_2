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
			if (Input.GetKeyDown ("t")) {
				if(playerGrabbedRope==false)
				{
					playerGrabbedRope = true;
				}else
				{
					playerGrabbedRope= false;
				}
			}
		} else if (companionInRange) { //if companion is in the neighborhood
			if (Input.GetKeyDown ("t")) {
				if(companionGrabbedRope==false)
				{
					companionGrabbedRope = true;
				}else
				{
					companionGrabbedRope= false;
				}
			
			}
		}



		if (playerGrabbedRope) {
			if (isEndPoint) {
				transform.position = player.transform.position + new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //rope follows player
			} else {
				player.transform.position = transform.position - new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //player follows rope
			}

		} else if (companionGrabbedRope) {
			if (isEndPoint) {
				transform.position = companion.transform.position + new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //rope follows companion
			} else {
				companion.transform.position = transform.position - new Vector3 (x_offset_rope, y_offset_rope, z_offset_rope); //companion follows rope
			}
		}


		transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));

	}
			
	

}


