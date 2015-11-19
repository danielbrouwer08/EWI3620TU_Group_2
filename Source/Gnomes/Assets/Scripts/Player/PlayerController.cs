using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	//Player properties
	private Rigidbody rb;
    public float jumpForce;
    public float walkSpeed;
    public float runSpeed;

    //Audio properties
    //private AudioSource jumpSound;
    //private AudioSource moveSound;
    //private AudioSource[] sounds;

    //Variables
    private Vector3 movement;
    private Vector3 jump;
    private float verticalInput;
    private float horizontalInput;
    //private bool gameOver = false;

    // Iinitialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called every fixed framerate frame
    void FixedUpdate()
    {
        //Get input from user
        getInput();

        //Only execute if movement isn't all zero because transform.forward generates lots of 'annoying' notifications then.
        if (!movement.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
        {
            //Make player look in direction of movement
            transform.forward = new Vector3(movement.x,0,movement.z);
        }
        //Move the player
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        //if button pressed, do a jump
        rb.AddForce(jump, ForceMode.VelocityChange);
    }

    //Get input from user
    void getInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //if player presses jump button and is not already in a jump (y velocuty is zero)
        if (Input.GetButtonDown("Fire1") && Mathf.Abs(rb.velocity.y) < 0.01)
        {
            jump = new Vector3(0,jumpForce,0);
        }
        else
        {
            jump = new Vector3(0,0,0);
        }

        //if player presses run button
        if (Input.GetButton("Fire2"))
        {
            movement = new Vector3(horizontalInput * runSpeed / 10, 0, verticalInput * runSpeed / 10);
        }
        else
        {
            movement = new Vector3(horizontalInput * walkSpeed / 10, 0, verticalInput * walkSpeed / 10);
        }
    }

}