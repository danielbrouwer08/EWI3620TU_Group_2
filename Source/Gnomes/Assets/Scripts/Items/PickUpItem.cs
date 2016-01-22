using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.UI;

//using UnityEditor;

public class PickUpItem : MonoBehaviour
{
	public float pickdistance = 5;
	private GameObject[] player;
	//private bool[] playerinrange;
	private int playerNum;
	private float xPosPlayer;
	private float zPosPlayer;
	public GameObject carrier;
	public float throwforce;
	//bool hasPlayer = false;
	bool beingCarried = false;
	private Rigidbody rb;
	//private Collider col;
	public string skill;
	private Vector3 startpos;
	private GameObject header;
	private GameObject text;
	private GameObject cameraSystem;
    private Transform ingamepanel;

    void Start ()
	{
		cameraSystem = GameObject.FindGameObjectWithTag("MainCamera");

        if (header != null && text != null)
        {
			header.SetActive (false);
            header = transform.FindChild("Canvas").FindChild("Header").gameObject;
            text = header.transform.FindChild("Text").gameObject;
            header.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100);
            header.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100);
            text.GetComponent<Text>().resizeTextMaxSize = 20;
		}
		startpos = GetComponent<Transform> ().position;
		rb = GetComponent<Rigidbody> ();
		//col = GetComponent<Collider> ();
		player = GameObject.FindGameObjectsWithTag ("Player");
		//playerinrange = new bool[player.Length];
		rb.isKinematic = false;
		rb.constraints = RigidbodyConstraints.None;
        ingamepanel = GameObject.FindGameObjectWithTag("IngamePanel").transform;
    }

	void OnCollisionStay (Collision other)
	{
		if(beingCarried && skill == "Demolish" && transform.parent.gameObject.GetComponent<Demolish>() != null)
		{
			transform.parent.gameObject.GetComponent<Demolish>().hammerCollision(other);
		}
	}

	void Update ()
	{
        if ((Vector3.Magnitude (transform.position - player [0].transform.position) < pickdistance || Vector3.Magnitude (transform.position - player [1].transform.position) < pickdistance) && !beingCarried && header != null) {
			header.SetActive (true);
            header.transform.parent.LookAt(cameraSystem.transform);
            header.transform.parent.Rotate(0, 180, 0);
            string displayText = "Press '.' (P1) or 'v' (P2) to pick up the " + skill + " skill";
			text.GetComponent<Text> ().text = displayText;
		} else if (header != null) {
			header.SetActive (false);
		}


		if ((transform.position.z > 100 || transform.position.z < -100 || transform.position.x < 0 || transform.position.y < -50) && carrier == null) {
			Respawnitem ();
		}

		for (int i = 0; i < player.Length; i++) {
			playerNum = player [i].GetComponent<PlayerController> ().playerNum;
            if (this.beingCarried)
            {
                if (Input.GetButtonDown("Interact" + playerNum))
                {
                   //Debug.Log("pushing the button");
                    if (player[i].GetComponent<PlayerProperties>().item.Equals(this.gameObject))
                    {
                        //Debug.Log("Trying to lose this item...");
                        this.Loseitem();
                        break;
                    }
                }
            }

            if (Vector3.Distance(transform.position, player[i].transform.position) < pickdistance)
            {
                //Debug.Log("I'm close enough!");
                if (Input.GetButtonDown("Interact" + playerNum) && player[i].GetComponent<PlayerProperties>().item == null)
                {
                    carrier = player[i];
                    carrier.GetComponent<PlayerProperties>().item = gameObject;

                    AddSkilltoPlayer(skill);
                    //rb.isKinematic = true;
                    //rb.detectCollisions = false;
					rb.constraints = RigidbodyConstraints.FreezeAll; // freeze rotations
                    transform.parent = player[i].transform;
                    transform.localPosition = new Vector3(0.0f, 4.1f, 0.0f);
                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    beingCarried = true;
                }
            }

            xPosPlayer = player [i].GetComponent<Transform> ().position.x;
			zPosPlayer = player [i].GetComponent<Transform> ().position.z;
		}
	}

	public void Loseitem ()
	{
		DeleteSkillfromPlayer (skill);
		carrier.GetComponent<PlayerProperties> ().item = null;
		//rb.isKinematic = false;
		//rb.detectCollisions = true;
		rb.constraints = RigidbodyConstraints.None; //remove constraints
		transform.parent = null;
		rb.AddForce (carrier.transform.forward * throwforce + Vector3.up * throwforce * 0.1f);
		carrier = null;
		beingCarried = false;
	}

	public void Respawnitem ()
	{
		transform.position = startpos;
		transform.eulerAngles = new Vector3 (0, 0, 0);
		rb.velocity = Vector3.zero;
	}

	void AddSkilltoPlayer (string skill)
	{
		var dict = new Dictionary<string, object> ();
		dict ["currentScene"] = Application.loadedLevelName;
		dict ["skill"] = skill;
		dict ["playerNum"] = playerNum;

		Analytics.CustomEvent ("pickUpItem", new Dictionary<string, object>
        {
            { "skill", skill },
            { "playerNum",  playerNum},
            { "xPos", xPosPlayer },
            { "zPos", zPosPlayer },
			{ "currentScene", Application.loadedLevelName }
        });

		UnityAnalyticsHeatmap.HeatmapEvent.Send ("checkpoint", gameObject.transform.position, dict);

		if (carrier.name.Equals ("kabouterdun")) {
			switch (skill) {
			case "Fly":
				carrier.AddComponent<Fly> ();
                    ingamepanel.FindChild("Player 1").FindChild("Item inventory").FindChild("Item").FindChild("Text").GetComponent<Text>().text = skill;
                    break;
			case "Float":
				carrier.AddComponent<Float> ();
                    ingamepanel.FindChild("Player 1").FindChild("Item inventory").FindChild("Item").FindChild("Text").GetComponent<Text>().text = skill;
                    break;
			// case "Build": carrier.AddComponent<Build>(); break;
			// case "Demolish": carrier.AddComponent<Demolish>(); break;
			}
		} else {

			switch (skill) {
			// case "Fly": carrier.AddComponent<Fly>(); break;
			// case "Float": carrier.AddComponent<Float>(); break;
			case "Build":
				carrier.AddComponent<Build> ();
                    ingamepanel.FindChild("Player 2").FindChild("Item inventory").FindChild("Item").FindChild("Text").GetComponent<Text>().text = skill;
                    break;
			case "Demolish":
				carrier.AddComponent<Demolish> ();
                    ingamepanel.FindChild("Player 2").FindChild("Item inventory").FindChild("Item").FindChild("Text").GetComponent<Text>().text = skill;
                    break;
			}
		}

	}

	void DeleteSkillfromPlayer (string skill)
	{
		switch (skill) {
		    case "Fly":
			    if(carrier.GetComponent<Fly> ()!=null)
    			{
	    		    Destroy (carrier.GetComponent<Fly> ());
		    	}
    			break;
	    	case "Float":
		    	if(carrier.GetComponent<Float> ()!=null)
			    {
			        Destroy (carrier.GetComponent<Float> ());
			    }
			    break;
		    case "Build":
    			if(carrier.GetComponent<Build> ()!=null)
	    		{
		    	    Destroy (carrier.GetComponent<Build> ());
			    }
    			break;
	    	case "Demolish":
		    	if(carrier.GetComponent<Demolish> ()!=null)
			    {
        		    Destroy (carrier.GetComponent<Demolish> ());
			    }
			    break;
            default:
                break;
		}
        if (carrier.name.Equals("kabouterdun"))
        {
            ingamepanel.FindChild("Player 1").FindChild("Item inventory").FindChild("Item").FindChild("Text").GetComponent<Text>().text = "None";
        }
        else
        {
            ingamepanel.FindChild("Player 2").FindChild("Item inventory").FindChild("Item").FindChild("Text").GetComponent<Text>().text = "None";
        }
    }
}
