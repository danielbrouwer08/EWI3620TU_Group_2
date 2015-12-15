﻿using UnityEngine;
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
    public float rotatespeed = 8;
    private Animation anim;
    private bool walking;
    private bool running;

    //animation speeds
    public float animwalk1 = 3.5f;
    public float animwalk2 = 2.8f;
    public float animjump1 = 5f;
    public float animjump2 = 4.5f;
    public float animrun1 = 20f;
    public float animrun2 = 20f;
    public float animidle1;
    public float animidle2;

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
	public bool loadLastCheckpoint = true;

	//private bool gameOver = false;

	// Iinitialization
	void Start ()
	{
		if(loadLastCheckpoint==true)
		{
			Vector3 spawnpos = new Vector3(PlayerPrefs.GetFloat("P" + playerNum + "_XPOS"),PlayerPrefs.GetFloat("P" + playerNum + "_YPOS"),PlayerPrefs.GetFloat("P" + playerNum + "_ZPOS"));
			transform.position = spawnpos;
		}

		rb = GetComponent<Rigidbody> ();
        anim = GetComponent<Animation>();
        if (playerNum == 1)
        {
            anim["Springen"].speed = animjump1;
            anim["Lopen0"].speed = animwalk1;
            anim["Rennen"].speed = animrun1;

        }
        else
        {
            anim["Springen"].speed = animjump2;
            anim["Lopen"].speed = animwalk2;
            anim["Rennen"].speed = animrun2;
        }
    }

	// Update is called every fixed framerate frame
	void FixedUpdate ()
	{
        
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
                transform.forward = Vector3.RotateTowards(transform.forward, new Vector3(movement.x, 0, movement.z),Time.fixedDeltaTime*rotatespeed,0);
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

        //if player presses run button
        if (Input.GetButton("Run" + playerNum))
        {
            running = true;
            movement = new Vector3(HorizontalPlayerInput * runSpeed * Mathf.Abs(Mathf.Cos(angle)), 0, VerticalPlayerInput * runSpeed * Mathf.Abs(Mathf.Sin(angle)));
        }
        else
        {
            running = false;
            movement = new Vector3(HorizontalPlayerInput * walkSpeed * Mathf.Abs(Mathf.Cos(angle)), 0, VerticalPlayerInput * walkSpeed * Mathf.Abs(Mathf.Sin(angle)));
        }

        //if player presses jump button and is not already in a jump (y velocuty is zero)
        if (Input.GetButtonDown("Jump" + playerNum) && grounded())
        {
            anim.Play("Springen");
            jump = true;
        }
        else if ((HorizontalPlayerInput != 0 || VerticalPlayerInput != 0))
        {
            if (!anim.IsPlaying("Springen"))
            {
                if (!running)
                {
                    anim.Play("Lopen0");
                    anim.Play("Lopen");
                }
                else
                {
                    anim.Play("Rennen");
                }
            }

            jump = false;
        }
        else
        {
            if (!anim.IsPlaying("Springen"))
                anim.Play("Stilstaan");
            jump = false;
        }
}

    public bool grounded()
    {
        return Physics.Raycast(transform.position + Vector3.forward * 0.5f, -Vector3.up, 1.1f) ||
               Physics.Raycast(transform.position - Vector3.forward * 0.5f, -Vector3.up, 1.1f) ||
               Physics.Raycast(transform.position + Vector3.right * 0.5f, -Vector3.up, 1.1f) ||
               Physics.Raycast(transform.position - Vector3.right * 0.5f, -Vector3.up, 1.1f);
    }

    public void ExternalForce(Vector3 force, float nomovementtime)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
        this.nomovementtime = nomovementtime;
    }
}