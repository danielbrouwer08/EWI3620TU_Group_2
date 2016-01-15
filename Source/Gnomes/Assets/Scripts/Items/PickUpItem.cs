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
	private bool[] playerinrange;
	private int playerNum;
	private float xPosPlayer;
	private float zPosPlayer;
	private GameObject carrier;
	public float throwforce;
	bool hasPlayer = false;
	bool beingCarried = false;
	private Rigidbody rb;
	private Collider col;
	public string skill;
	private Vector3 startpos;
	public GameObject header;
	public GameObject text;

	void OnCollisionExit (Collision other)
	{
		hasPlayer = false;
	}

	void Start ()
	{
		if (header != null && text != null) {
			header.SetActive (false);
		}
		startpos = GetComponent<Transform> ().position;
		rb = GetComponent<Rigidbody> ();
		col = GetComponent<Collider> ();
		player = GameObject.FindGameObjectsWithTag ("Player");
		playerinrange = new bool[player.Length];
	}

	void Update ()
	{
		if ((Vector3.Magnitude (transform.position - player [0].transform.position) < pickdistance || Vector3.Magnitude (transform.position - player [1].transform.position) < pickdistance) && !beingCarried && header != null) {
			header.SetActive (true);
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
          
			if (Vector3.Magnitude (transform.position - player [i].transform.position) < pickdistance) {
				if (Input.GetButtonDown ("Interact" + playerNum) && player [i].GetComponent<PlayerProperties> ().item == null) {
					carrier = player [i];
					carrier.GetComponent<PlayerProperties> ().item = gameObject;
					AddSkilltoPlayer (skill);
					rb.isKinematic = true;
					rb.detectCollisions = false;
					transform.parent = player [i].transform;
					transform.localPosition = new Vector3 (0.0f, 4.1f, 0.0f);
					transform.localEulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
					beingCarried = true;
				}else if(Input.GetButtonDown("Interact" + playerNum) && player [i].GetComponent<PlayerProperties> ().item != null)
				{
					Loseitem();
				}

				xPosPlayer = player [i].GetComponent<Transform> ().position.x;
				zPosPlayer = player [i].GetComponent<Transform> ().position.z;
			}




		}

        
	}

	public void Loseitem ()
	{
		DeleteSkillfromPlayer (skill);
		carrier.GetComponent<PlayerProperties> ().item = null;
		rb.isKinematic = false;
		rb.detectCollisions = true;
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
				break;
			case "Float":
				carrier.AddComponent<Float> ();
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
				break;
			case "Demolish":
				carrier.AddComponent<Demolish> ();
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
		}
	}
}
