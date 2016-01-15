using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	//Player properties
	private Rigidbody rb;
	public float jumpForcebegin;
	public float walkSpeedbegin;
	public float runSpeedbegin;
    public float jumpForce;
    public float walkSpeed;
    public float runSpeed;
    public float slideSpeed;
	public int playerNum;
    public float rotatespeed = 8;
    public Animation anim;
    public bool walking;
    public bool running;

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
    public bool isSinglePlayer = true;
    public bool isActive;
    private int input;

	private GameManger gameManager;

	//private bool gameOver = false;

	// Iinitialization
	void Start ()
	{
        jumpForce = jumpForcebegin;
        walkSpeed = walkSpeedbegin;
        runSpeed = runSpeedbegin;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
        //Debug.Log("Ik zit hier naar te kijken: " + PlayerPrefs.GetString("playermode"));
        //Debug.Log("Ik zit hier naar te kijken: " + PlayerPrefs.GetString("playermode"));
        if (PlayerPrefs.GetString("playermode") == "single")
        {
            isSinglePlayer = true;
        }
        else
        {
            isSinglePlayer = false;
        }

		if (playerNum == 2 && isSinglePlayer)
		{
			isActive = false;
		}
		else
		{
			isActive = true;
		}

        if (isSinglePlayer)
        {
            input = 1;
        }
        else
        {
            input = playerNum;
        }

		if(loadLastCheckpoint==true)
		{
            Vector3 position = getLastSavedPos();
			transform.position = position;
		}

		rb = GetComponent<Rigidbody> ();
        anim = GetComponent<Animation>();
        if (playerNum == 1)
        {
            anim["Springen"].speed = animjump1;
            anim["Lopen"].speed = animwalk1;
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
            if (isActive)
            {
                //Get input from p1 or p2
                getPlayerInput();
            }
            else
            {
                movement = new Vector3(0, 0, 0);
                anim.Play("Stilstaan");
            }

            //Only execute if movement isn't all zero because transform.forward generates lots of 'annoying' notifications then.
            if (!movement.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
            {
                //Make player look in direction of movement
                transform.forward = Vector3.RotateTowards(transform.forward, new Vector3(movement.x, 0, movement.z),Time.fixedDeltaTime*rotatespeed,0);
            }
            //Move the player
            //if (!Physics.Raycast(transform.position, transform.forward, 1.207f)) MOET GEFIXT WORDEN
			if(true)
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
                rb.AddForce(movement / 20, ForceMode.VelocityChange);
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
                    if (GetComponent <Fly> () != null && Input.GetButton("Item" + playerNum))
                    {
                    } 
                    else
                    {
                        anim.Play("Lopen");
                    }  
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

    /*public bool grounded()
    {
        RaycastHit[] hit;
        hit = new RaycastHit[8];
        float[] distance;
        distance = new float[8];
        for(int i = 0; i<8; i++)
        {
            Vector3 displacement;
            if (i < 2)
            {
                displacement = Vector3.forward * 0.1f;
            }
            else if(i < 4)
            {
                displacement = - Vector3.forward * 0.1f;
            }
            else if(i < 6)
            {
                displacement = Vector3.right * 0.1f;
            }
            else
            {
                displacement = -Vector3.right * 0.1f;
            }
            Physics.Raycast(transform.position + displacement, Vector3.up * (i % 2 * 2 - 1), out hit[i], 0.25f);
            distance[i] = hit[i].distance;
            //Debug.Log(distance[i]);
        }
        float som = 0;
        for (int i = 0; i < 4; i++)
        {
            float afstand = Mathf.Max(distance[i * 2], distance[i * 2 + 1]);
            if(afstand == 0)
            {
                afstand = 0.25f;
            }
            som += afstand;
        }
        return som < 0.5f;
    }*/

    public bool grounded()
    {
        //return Physics.Raycast(transform.position + 0.1f * Vector3.up, -Vector3.up, 0.20f);
		return true; // temporary fix MOET AAN GESLEUTELD WORDEN!!!
    }

    public void ExternalForce(Vector3 force, float nomovementtime)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
        this.nomovementtime = nomovementtime;
    }

public Vector3 getLastSavedPos()
    {
        Vector3 spawnpos;

        if (playerNum == 1)
        {
            spawnpos = gameManager.returnCurrent().P1Pos;
            print("p1 spawn pos: " + spawnpos);
        }
        else
        {
            spawnpos = gameManager.returnCurrent().P2Pos;
            print("p2 spawn pos: " + spawnpos);
        }

        transform.position = spawnpos;
        return spawnpos;
    }
}