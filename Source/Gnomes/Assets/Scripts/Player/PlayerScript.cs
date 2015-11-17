using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

	//Player properties
	private Rigidbody rb;
	public float jumpForce;
	public float walkForce;
	public float runForce;
		
	//Audio properties
	//private AudioSource jumpSound;
	//private AudioSource moveSound;
	//private AudioSource[] sounds;
		
	//Variables
	private Vector3 movement;
	private Vector3 jump;
	private float verticalInput;
	private float horizontalInput;
	private bool gameOver = false;

	// Iinitialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
		
	// Update is called every fixed framerate frame
	void FixedUpdate ()
	{
		//Get input from user
		getInput ();

		//Only execute if movement isn't all zero because transform.forward generates lots of 'annoying' notifications then.
		if(!movement.Equals(new Vector3(0.0f,0.0f,0.0f)))
		{
			//Make player look in direction of movement
			transform.forward = movement;
		}	
		//Move the player
		rb.AddForce(movement,ForceMode.VelocityChange);

		//if button pressed, do a jump
		rb.AddForce (jump,ForceMode.VelocityChange);
	}

	//Get input from user
	void getInput ()
	{
		verticalInput = Input.GetAxis ("Vertical");
		horizontalInput = Input.GetAxis ("Horizontal"); 

		//if player presses jump button and is not already in a jump (y velocuty is zero)
		if (Input.GetButtonDown ("Fire1") && Mathf.Abs (rb.velocity.y) < 0.01) {
			jump = new Vector3 (0.0f, jumpForce, 0.0f);
		} else {
			jump = new Vector3 (0.0f, 0.0f, 0.0f);
		}

        //if player presses run button
        if (Input.GetButton("Fire2"))
        {
            movement = new Vector3(horizontalInput * runForce / 10, 0.0f, verticalInput * runForce / 10);
        }
        else
        {
            movement = new Vector3(horizontalInput * walkForce / 10, 0.0f, verticalInput * walkForce / 10);
        }
        if(Mathf.Abs(rb.velocity.y) >= 0.01)
        {
            movement = movement * 0.1f;
        }
	}

}
