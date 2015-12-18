using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;
using SimpleJSON;
using System.Collections.Generic;
using System;

public class GameManger : MonoBehaviour
{
	private string username;
	private string password;
	public  int saveslots = 3;
	public  Savegame[] saves = new Savegame[3];
	private  int currentslot;
	DateTime serverTimeStamp;
	private Savegame[] online = new Savegame[3];
	private bool loginSucceed = false;
	private bool registerSucceed = false;
	private bool onlinemode;
	//private Savegame[] serversaves = new Savegame[3];

	void Awake ()
	{
		currentslot = PlayerPrefs.GetInt ("saveslot");

		int online = PlayerPrefs.GetInt ("onlinemode");

		if (online == 1) {
			onlinemode = true;
		} else {
			onlinemode = false;
		}

		//TESTING PURPOSE:
		//register ("blabla", "huehuehue");
		//StartCoroutine (getSaves ()); //get new saves from server and wait (blocking)

		//onlineMode (); 
		offlineMode();

	}

	public void register (string user, string pass)
	{
		Debug.Log ("Register");
		this.password = pass;
		this.username = user;
		
		StartCoroutine (registerOnServer ());
	}

	public void onlineMode ()
	{
		StartCoroutine (getSaves()); // login and get online saves
		StartCoroutine (GetTimeStamp ()); //get timestamp from server (blocking)

		Savegame[] local = readPlayerPrefs (); //get local saves
		
		DateTime currentTimeStamp = DateTime.Parse (PlayerPrefs.GetString ("timeStamp"));

		int temp = DateTime.Compare (currentTimeStamp, serverTimeStamp);

		Debug.Log("Getting the latest save game...");

		if (temp > 0) {
			Debug.Log ("using local save file");
			saves = local;
		} else{
			Debug.Log ("using server save file");
			saves = online;
		}
	}

	private void offlineMode ()
	{
		Savegame[] local = readPlayerPrefs (); //get local saves

		Debug.Log ("using local save file");
		saves = local;
	}
	
	public Savegame returnCurrent ()
	{
		return saves [currentslot];
	}

	//add new savegame to the savesarray and add to the playerprefs the json string
	public  void addNewSave (Savegame savegame)
	{
		Debug.Log ("Adding the following savefile to the playerprefs: " + savegame.toString ());
		saves [currentslot] = savegame;
		addToPlayerPrefs (savegame);

		//upload data to the server
		if (onlinemode) {
			StartCoroutine (sendSave ());
		}
	}
	

	//Add JSON saves array to the playerprefs
	private  void addToPlayerPrefs (Savegame savegame)
	{
		PlayerPrefs.SetString ("timeStamp", System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss"));
		for (int i = 0; i<saveslots; i++) {
			if ((saves [i]) != null) {
				PlayerPrefs.SetString ("saveNo" + i, Savegame.getJSON (saves [i]));
			}
		}

	}
	
	//Read the playerprefs and return the saves
	private  Savegame[] readPlayerPrefs ()
	{
		Savegame[] saves = new Savegame[saveslots];
		string temp;

		for (int i=0; i<saveslots; i++) {
			if (PlayerPrefs.GetString ("saveNo" + i) != null) {
				saves [i] = Savegame.parseJSON (PlayerPrefs.GetString ("saveNo" + i));
				//Debug.Log ("Wat ik heb geparst: " + saves [i].toString ());
			}
		}

		return saves;
	}

	IEnumerator sendSave ()
	{
		Debug.Log ("in upload()");
		WWWForm form = new WWWForm ();

		//Add timestamp to data 
		form.AddField ("timeStamp", System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss"));
		form.AddField ("currentSlot", currentslot);

		//Add current savefile to data
		form.AddField ("save", PlayerPrefs.GetString ("saveNo" + currentslot));
		
		Debug.Log (form.data.GetLength (0));
		
		UnityWebRequest www = UnityWebRequest.Post ("http://drproject.twi.tudelft.nl:8083/sendSave", form);

		www.SetRequestHeader ("Authorization", "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes (username + ":" + password)));

		Debug.Log ("in using");
		yield return www.Send ();
		Debug.Log ("na return");
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Form upload complete!");

		}
	}

	IEnumerator GetTimeStamp ()
	{
		//WWWForm form = new WWWForm ();
		//Hashtable headers = form.headers;
		//headers["Authorization"] = "Basic " + System.Convert.ToBase64String(
		//	System.Text.Encoding.ASCII.GetBytes(username + ":" + password));

		UnityWebRequest www = UnityWebRequest.Get ("http://drproject.twi.tudelft.nl:8083/getTimeStamp");

		www.SetRequestHeader ("Authorization", "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes (username + ":" + password)));

		yield return www.Send ();
		
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			// Show results as text
			Debug.Log (www.downloadHandler.text);
			string receivedString = www.downloadHandler.text;
			Debug.Log ("Timestamp received from server: " + receivedString);
			serverTimeStamp = DateTime.Parse (receivedString.Replace ("\"", ""));
			//Debug.Log (receivedString);
		}
	}

	//register on the server
	IEnumerator registerOnServer ()
	{
		WWWForm form = new WWWForm ();
			
		form.AddField ("user", this.username);
		form.AddField ("pass", this.password);
			

		Debug.Log (form.data.GetLength (0));
			
		UnityWebRequest www = UnityWebRequest.Post ("http://drproject.twi.tudelft.nl:8083/register", form);
			
		//www.SetRequestHeader("Authorization","Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username + ":" + password)));

		Debug.Log ("in using");
		yield return www.Send ();
		Debug.Log ("na return");
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Register complete!");
			Debug.Log (www.downloadHandler.text);
			string receivedString = www.downloadHandler.text;
				
			if(receivedString=="true")
			{
				Debug.Log("Registration succesful :D");
				registerSucceed = true;
			}else
			{
				Debug.Log("Registration failed :(");
				registerSucceed = false;
			}
		}
	}



	//Get saves over the interwebs
	IEnumerator getSaves ()
	{

		UnityWebRequest www = UnityWebRequest.Get ("http://drproject.twi.tudelft.nl:8083/getSaves");

		www.SetRequestHeader ("Authorization", "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes (username + ":" + password)));

		yield return www.Send ();
		
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			// Show results as text
			Debug.Log (www.downloadHandler.text);
			string receivedString = www.downloadHandler.text;
			Debug.Log (receivedString);

			if (receivedString == "401 Unauthorized") {
				loginSucceed = false;
			} else {
				loginSucceed = true;

				Debug.Log ("received:");
				Debug.Log (receivedString);
				
				string[] parts = receivedString.Split (new string[] {"},{"}, System.StringSplitOptions.None);
				
				parts [0] = parts [0].Replace ("[{", "");
				parts [parts.Length - 1] = parts [parts.Length - 1].Replace ("}]", "");
				Savegame temp;
				
				Debug.Log ("The parts:");
				
				for (int i=0; i<parts.Length; i++) {
					parts [i] = "{" + parts [i] + "}";
					temp = Savegame.parseJSON (parts [i]);
					online [i] = temp; //add saves to saves list
					//Debug.Log("Partsno" + i + ": " + parts[i]);
					//Debug.Log("Extracted savefile" + i + ": " + temp.toString());


				}


			}



		}
	}	

}
