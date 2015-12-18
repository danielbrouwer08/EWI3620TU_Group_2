﻿using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEditor;
//using UnityEngine.Experimental.Networking;

public class checkpoint : MonoBehaviour
{

	private bool player1In = false;
	private bool player2In = false;
	private GameObject player1;
	private GameObject player2;
	private int state = 0;
	private GameObject gamemanager;

	void Start()
	{
		gamemanager = GameObject.FindWithTag("GameManager");
	}


	// Update is called once per frame
	void Update ()
	{

		if (player1In && player2In && (state == 0 || state == 1)) {
			state = 2;
			this.GetComponent<Renderer> ().material = Resources.Load ("checkpointstate2") as Material;
			player1In = false;
			player2In = false;
			saveCheckpoint ();
		} else if ((player1In || player2In) && state == 0) {
			state = 1;
			this.GetComponent<Renderer> ().material = Resources.Load ("checkpointstate1") as Material;
		}
	}

	void saveCheckpoint ()
	{

		PlayerProperties p1prop = player1.GetComponent<PlayerProperties> ();
		PlayerProperties p2prop = player2.GetComponent<PlayerProperties> ();
		PlayerController p1cont = player1.GetComponent<PlayerController> ();
		PlayerController p2cont = player2.GetComponent<PlayerController> ();

		Vector3 p1SpanwPos = transform.position + new Vector3(-1.5f, 3.0f, 0.0f);
		Vector3 p2SpanwPos = transform.position + new Vector3(1.5f, 3.0f, 0.0f);

		GameManger GM = gamemanager.GetComponent<GameManger>();

		//add new save to the savegame 
		GM.addNewSave (new Savegame(p1SpanwPos, p1prop.health, p1prop.score, p2SpanwPos, p2prop.health, p2prop.score, Application.loadedLevelName));
//
//		//store p1 health and score
//		PlayerPrefs.SetFloat ("P1_Health", p1prop.health);
//		PlayerPrefs.SetFloat ("P1_Score", p1prop.score);
//
//		//store p2 health and score
//		PlayerPrefs.SetFloat ("P2_Health", p2prop.health);
//		PlayerPrefs.SetFloat ("P2_Score", p2prop.score);
//	
//		//store p1 spawn location
//		PlayerPrefs.SetFloat ("P1_XPOS", transform.position.x - 1.5f);
//		PlayerPrefs.SetFloat ("P1_YPOS", transform.position.y + 3.0f);
//		PlayerPrefs.SetFloat ("P1_ZPOS", transform.position.z);
//
//		//store p2 spawn location
//		PlayerPrefs.SetFloat ("P2_XPOS", transform.position.x + 1.5f);
//		PlayerPrefs.SetFloat ("P2_YPOS", transform.position.y + 3.0f);
//		PlayerPrefs.SetFloat ("P2_ZPOS", transform.position.z);
//
//		//store date and time of savefile
//		PlayerPrefs.SetString ("TimeStamp", System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss"));
//
//
//		//store current chaptername
//		PlayerPrefs.SetString ("Chapter", Application.loadedLevelName);
//
//		//Upload the data
//		StartCoroutine (Upload ());
//
//		//PlayerPrefs.SetString("TimeStamp",System.DateTime.Now);

	}



	void OnCollisionEnter (Collision collision)
	{

		if (collision.gameObject.tag.Equals ("Player")) {
            var dict2 = new Dictionary<string, object>();

            if (collision.gameObject.GetComponent<PlayerController> ().playerNum == 1) {


                if (!player1In)
                {
                    dict2["currentScene"] = EditorApplication.currentScene;
                    dict2["playerNum"] = 1;

                    Analytics.CustomEvent("checkPoint", new Dictionary<string, object>
                    {
                        { "x-location",  gameObject.transform.position.x},
                        { "z-location", gameObject.transform.position.z},
                        { "playerNum",  1}
                    });

                    UnityAnalyticsHeatmap.HeatmapEvent.Send("checkpoint", gameObject.transform.position, dict2);
                }

                player1In = true;
				player1 = collision.gameObject;
			} else if (collision.gameObject.GetComponent<PlayerController> ().playerNum == 2) {

                if (!player2In)
                {
                    dict2["currentScene"] = EditorApplication.currentScene;
                    dict2["playerNum"] = 1;

                    Analytics.CustomEvent("checkPoint", new Dictionary<string, object>
                    {
                        { "x-location",  gameObject.transform.position.x},
                        { "z-location", gameObject.transform.position.z},
                        { "playerNum",  2}
                    });

                    UnityAnalyticsHeatmap.HeatmapEvent.Send("checkpoint", gameObject.transform.position, dict2);
                }


                player2In = true;
				player2 = collision.gameObject;
			}
		
		}
	}

}

