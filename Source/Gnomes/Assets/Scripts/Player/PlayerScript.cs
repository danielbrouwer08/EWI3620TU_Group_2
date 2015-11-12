	using UnityEngine;
	using System.Collections;

	public class PlayerScript : MonoBehaviour {

		//Player properties
		private Rigidbody rb;
		public float jumpForce;
		public float moveForce;
		
		//Audio properties
		//private AudioSource jumpSound;
		//private AudioSource moveSound;
		//private AudioSource[] sounds;
		
		//Variables
		private Vector3 movement;
		private float verticalInput;
		private float horizontalInput;
		private bool gameOver = false;

		// Iinitialization
		void Start () {
			rb = GetComponent<Rigidbody>();
		}
		
		// Update is called every fixed framerate frame
		void FixedUpdate () {
			//Get input from user
			getInput ();
			//Add force to the player
			rb.AddForce(movement);
		}

		//Get input from user
		void getInput()
		{
			verticalInput = Input.GetAxis("Vertical");
			horizontalInput = Input.GetAxis("Horizontal"); 
			
			//if player presses jump button
			if(Input.GetButtonDown("Fire1"))
			{
				movement = new Vector3(horizontalInput*moveForce,jumpForce,verticalInput*moveForce);
				//rb.MoveRotation(Quaternion.LookRotation(movement, Vector3.up));
				rb.rotation = (Quaternion.LookRotation(movement, Vector3.up));

			}else
			{
				movement = new Vector3(horizontalInput*moveForce,0.0f,verticalInput*moveForce);
				//rb.MoveRotation(Quaternion.LookRotation(movement, Vector3.up));
				rb.rotation = (Quaternion.LookRotation(movement, Vector3.up));
			}
		}

	}
