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
	private bool jump;
	private float VerticalPlayerInput;
	private float HorizontalPlayerInput;
    private float nomovementtime = 0;
	//private bool gameOver = false;

	// Iinitialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called every fixed framerate frame
	void FixedUpdate ()
	{
        Debug.Log(Vector3.Magnitude(rb.velocity));
        if (nomovementtime > 0)
        {
            nomovementtime -= Time.fixedDeltaTime;
        }
        else
        {
            //Get input from p1 or p2
            getPlayerInput();

            //Only execute if movement isn't all zero because transform.forward generates lots of 'annoying' notifications then.
            if (!movement.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
            {
                //Make player look in direction of movement
                transform.forward = new Vector3(movement.x, 0, movement.z);
            }
            //Move the player
            if (!Physics.Raycast(transform.position, transform.forward, 1.207f))
            {
                if (jump)
                {
                    rb.velocity = new Vector3(movement.x, jumpForce, movement.z);
                }
                else
                {
                    rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
                }
            }
            else
            {
                if(jump)
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                }
                rb.AddForce(movement / 10, ForceMode.VelocityChange);
            }
        }
	}

	//Get input from player
	void getPlayerInput ()
    {
		VerticalPlayerInput = Input.GetAxis ("Vertical" + playerNum);
		HorizontalPlayerInput = Input.GetAxis ("Horizontal" + playerNum);
        float angle;
        if (HorizontalPlayerInput == 0)
        {
            angle = Mathf.PI * (1 + 0.5f * Mathf.Sign(-VerticalPlayerInput));
        }
        else
        {
            angle = Mathf.Atan(VerticalPlayerInput / HorizontalPlayerInput);
        }
        //if player presses jump button and is not already in a jump (y velocuty is zero)
        if (Input.GetButtonDown ("Jump" + playerNum) && grounded()) {
			jump = true;
		} else {
			jump = false;
		}

		//if player presses run button
		if (Input.GetButton ("Run" + playerNum)) {
			movement = new Vector3 (HorizontalPlayerInput * runSpeed * Mathf.Abs(Mathf.Cos(angle)), 0, VerticalPlayerInput * runSpeed * Mathf.Abs(Mathf.Sin(angle)));
		} else {
			movement = new Vector3 (HorizontalPlayerInput * walkSpeed * Mathf.Abs(Mathf.Cos(angle)), 0, VerticalPlayerInput * walkSpeed * Mathf.Abs(Mathf.Sin(angle)));
		}
	}

    bool grounded()
    {
        return Physics.CheckCapsule(transform.position - new Vector3(0, 1.1f, 0), transform.position - new Vector3(0, 1.5f, 0), 0.5f);
    }

    public void ExternalForce(Vector3 force, float nomovementtime)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
        this.nomovementtime = nomovementtime;
    }
}