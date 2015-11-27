using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	//Player properties
	private Rigidbody rb;
	public float jumpForce;
	public float walkSpeed;
	public float runSpeed;
	public float slideSpeed;
	public int playerNum;

	//Audio properties
	//private AudioSource jumpSound;
	//private AudioSource moveSound;
	//private AudioSource[] sounds;

	//Variables
	private Vector3 movement;
	private Vector3 jump;
	private float VerticalPlayerInput;
	private float HorizontalPlayerInput;
	//private bool gameOver = false;

	// Iinitialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called every fixed framerate frame
	void FixedUpdate ()
	{
		//Get input from p1 or p2
		if (playerNum == 1) {
			getPlayerInput ();
		} else if (playerNum == 2) {
			getCompanionInput ();
		} else {
			print ("Player " + playerNum + " is not valid");
		}

		//Only execute if movement isn't all zero because transform.forward generates lots of 'annoying' notifications then.
		if (!movement.Equals (new Vector3 (0.0f, 0.0f, 0.0f))) {
			//Make player look in direction of movement
			transform.forward = new Vector3 (movement.x, 0, movement.z);
		}
		//Move the player
		if (!Physics.Raycast (transform.position, transform.forward, 1.207f)) {
			rb.velocity = new Vector3 (movement.x, rb.velocity.y, movement.z);
		} else {
			rb.AddForce (movement / 10, ForceMode.VelocityChange);
		}

		//if button pressed, do a jump
		rb.AddForce (jump, ForceMode.VelocityChange);
	}

<<<<<<< HEAD
    //Get input from user
    void getInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        //if player presses jump button and is not already in a jump (y velocuty is zero)

        //RAYCAST IS AFHANKELIJK VAN GROOTTE VAN PLAYER

        if (Input.GetButtonDown("Fire1") && Physics.Raycast(transform.position, -Vector3.up, 1.208f)) 
        {
=======
	//Get input from player 1
	void getPlayerInput ()
	{
	
		VerticalPlayerInput = Input.GetAxis ("VerticalPlayer");
		HorizontalPlayerInput = Input.GetAxis ("HorizontalPlayer");
		//if player presses jump button and is not already in a jump (y velocuty is zero)
		if (Input.GetButtonDown ("Fire1Player") && Physics.Raycast (transform.position, -Vector3.up, 1.208f)) {
>>>>>>> feature/Puzzle.NO3
            
			jump = new Vector3 (0, jumpForce, 0);
		} else {
			jump = new Vector3 (0, 0, 0);
		}

		//if player presses run button
        
		if (Input.GetButton ("Fire2Player")) {
			movement = new Vector3 (HorizontalPlayerInput * runSpeed, 0, VerticalPlayerInput * runSpeed);
		} else {
			movement = new Vector3 (HorizontalPlayerInput * walkSpeed, 0, VerticalPlayerInput * walkSpeed);
		}
	}

	//Get input from player 1
	void getCompanionInput ()
	{
			
		VerticalPlayerInput = Input.GetAxis ("VerticalCompanion");
		HorizontalPlayerInput = Input.GetAxis ("HorizontalCompanion");
		//if player presses jump button and is not already in a jump (y velocuty is zero)
		if (Input.GetButtonDown ("Fire1Companion") && Physics.Raycast (transform.position, -Vector3.up, 1.208f)) {
				
			jump = new Vector3 (0, jumpForce, 0);
		} else {
			jump = new Vector3 (0, 0, 0);
		}
			
		//if player presses run button
			
		if (Input.GetButton ("Fire2Companion")) {
			movement = new Vector3 (HorizontalPlayerInput * runSpeed, 0, VerticalPlayerInput * runSpeed);
		} else {
			movement = new Vector3 (HorizontalPlayerInput * walkSpeed, 0, VerticalPlayerInput * walkSpeed);
		}
	}


}