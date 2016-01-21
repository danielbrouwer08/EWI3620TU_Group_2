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
	public  int currentslot = 0;
	DateTime serverTimeStamp;
	//private Savegame[] online = new Savegame[3];
	public bool loginSucceed = false;
	public bool registerSucceed = false;
    //private bool onlinemode;
    //private Savegame[] serversaves = new Savegame[3];
    private string oldusername;
        

	void Awake ()
	{
        //PlayerPrefs.DeleteAll();
        saves = readPlayerPrefs ();
		currentslot = PlayerPrefs.GetInt ("saveslot");

		//int online = PlayerPrefs.GetInt ("onlinemode");

		if(PlayerPrefs.GetString("teamname")!=null)
		{
			username = PlayerPrefs.GetString("teamname");
			password = PlayerPrefs.GetString("password");
		}

		//Debug.Log(PlayerPrefs.GetString ("timeStamp"));

		//onlineMode("Daniel","mijn_eerste_password");

	}

    public int getscore(int player)
    {
        if (player == 1)
        {
            return saves[currentslot].P1Score;
        }
        else
        {
            return saves[currentslot].P2Score;
        }
    }

	public void register (string user, string pass)
	{
		Debug.Log ("Register");
		//this.password = pass;
		//this.username = user;
		//PlayerPrefs.SetString("teamname",user);
		//PlayerPrefs.SetString("password",pass);
		
		StartCoroutine (registerOnServer (user,pass));
	}

    void Update()
    {
        //Debug.Log(username);
    }

	public void onlineMode (string user, string pass)
	{
		PlayerPrefs.SetString("teamname",user);
		PlayerPrefs.SetString("password",pass);
        oldusername = username;
		this.password = pass;
		this.username = user;
        

		StartCoroutine (GetTimeStamp ()); //get timestamp from server (blocking)
		StartCoroutine (getSaves()); // login and get online saves


	}

	private void offlineMode ()
	{
		//Savegame[] local = readPlayerPrefs (); //get local saves

		Debug.Log ("using local save file");
		//saves = local;
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
		updatePlayerPrefs ();

		//upload data to the server
		StartCoroutine (sendSave ());
	}

	public void emptySaves()
	{
		//PlayerPrefs.DeleteAll();
		Vector3 P1Pos = new Vector3(23.5f, 3.5f, 25.0f);
		Vector3 P2Pos = new Vector3(26.5f, 3.5f, 25.0f);
		float P1Health = 100;
		float P2Health = 100;
		int P1Score = 0;
		int P2Score = 0;
		currentslot = 0;
		//Debug.Log ("Adding the following savefile to the playerprefs: " + savegame.toString ());
		for(int i=0;i<saves.Length;i++)
		{
			saves[i] = new Savegame(P1Pos, P1Health, P1Score, P2Pos, P2Health, P2Score,"Chapter1");
		}
	
		updatePlayerPrefs ();
	}

	public Savegame emptySave ()
	{
		Vector3 P1Pos = new Vector3(23.5f, 3.5f, 25.0f);
		Vector3 P2Pos = new Vector3(26.5f, 3.5f, 25.0f);
		float P1Health = 100;
		float P2Health = 100;
		int P1Score = 0;
		int P2Score = 0;
		currentslot = 0;
		//Debug.Log ("Adding the following savefile to the playerprefs: " + savegame.toString ());
		return new Savegame(P1Pos, P1Health, P1Score, P2Pos, P2Health, P2Score,"Chapter1");
		//updatePlayerPrefs ();
		
		//upload data to the server
		//StartCoroutine (sendSave ());
	}



    //Add JSON saves array to the playerprefs
    private void updatePlayerPrefs()
    {
        PlayerPrefs.SetString("timeStamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        for (int i = 0; i < saveslots; i++)
        {
            if ((saves[i]) != null)
            {
                PlayerPrefs.SetString("saveNo" + i, Savegame.getJSON(saves[i]));
            }
            else
            {
                PlayerPrefs.SetString("saveNo" + i, Savegame.getJSON(emptySave()));
            }
        }
        // Save local highscores
        if (PlayerPrefs.GetInt("Total9") < saves[currentslot].P1Score + saves[currentslot].P2Score)
        {
            int j = -1;
            int k = 9;
            for (int i = 9; i >= 0; i--)
            {
                Debug.Log(i + ": " + PlayerPrefs.GetString("Team" + i) + " " + username);
                if (PlayerPrefs.GetString("Team" + i).Equals(username))
                {
                    k = i;
                }
                if (i == 0 && j == -1)
                {
                    j = i;
                }
                if (PlayerPrefs.GetInt("Total" + (i - 1)) >= saves[currentslot].P1Score + saves[currentslot].P2Score && j == -1)
                {
                    j = i;
                }
            }
            Debug.Log(k + " " + j);
            if (j <= k)
            {
                for (int i = k; i > j; i--)
                {
                    PlayerPrefs.SetString("Team" + i, PlayerPrefs.GetString("Team" + (i - 1)));
                    PlayerPrefs.SetInt("Total" + i, PlayerPrefs.GetInt("Total" + (i - 1)));
                    PlayerPrefs.SetInt("P1" + i, PlayerPrefs.GetInt("P1" + (i - 1)));
                    PlayerPrefs.SetInt("P2" + i, PlayerPrefs.GetInt("P2" + (i - 1)));
                }
                PlayerPrefs.SetString("Team" + j, username);
                PlayerPrefs.SetInt("Total" + j, saves[currentslot].P1Score + saves[currentslot].P2Score);
                PlayerPrefs.SetInt("P1" + j, saves[currentslot].P1Score);
                PlayerPrefs.SetInt("P2" + j, saves[currentslot].P2Score);
            }
        }
    }



    //Read the playerprefs and return the saves
    private  Savegame[] readPlayerPrefs ()
	{
		Savegame[] saves = new Savegame[saveslots];

		for (int i=0; i<saveslots; i++) {
			if (PlayerPrefs.GetString ("saveNo" + i) != null) {
				saves [i] = Savegame.parseJSON (PlayerPrefs.GetString ("saveNo" + i));
				//Debug.Log ("Wat ik heb geparst: " + saves [i].toString ());
			}else
			{
				saves[i] = emptySave();
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
		
		UnityWebRequest www = UnityWebRequest.Post ("https://drproject.twi.tudelft.nl:8083/sendSave", form);

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

		UnityWebRequest www = UnityWebRequest.Get ("https://drproject.twi.tudelft.nl:8083/getTimeStamp");

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
	IEnumerator registerOnServer (string user,string pass)
	{
		WWWForm form = new WWWForm ();
			
		form.AddField ("user", user);
		form.AddField ("pass", pass);
			

		Debug.Log (form.data.GetLength (0));
			
		UnityWebRequest www = UnityWebRequest.Post ("https://drproject.twi.tudelft.nl:8083/register", form);
			
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
		Savegame[] online = new Savegame[saveslots]; //get local saves

		UnityWebRequest www = UnityWebRequest.Get ("https://drproject.twi.tudelft.nl:8083/getSaves");

		www.SetRequestHeader ("Authorization", "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes (username + ":" + password)));

		yield return www.Send ();
		
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			// Show results as text
			string receivedString = www.downloadHandler.text;

			if (receivedString == "401 Unauthorized") {
				loginSucceed = false;
			} else {
				loginSucceed = true;
				
				string[] parts = receivedString.Split (new string[] {"},{"}, System.StringSplitOptions.None);
				
				parts [0] = parts [0].Replace ("[{", "");
				parts [parts.Length - 1] = parts [parts.Length - 1].Replace ("}]", "");
				Savegame temp;
				
				for (int i=0; i<parts.Length; i++) {
					parts [i] = "{" + parts [i] + "}";
					temp = Savegame.parseJSON (parts [i]);
					online [i] = temp; //add saves to saves list


				}


			}

			DateTime currentTimeStamp = DateTime.Parse (PlayerPrefs.GetString ("timeStamp"));
			
			int datecompare = DateTime.Compare (currentTimeStamp, serverTimeStamp);
			
			if (datecompare < 0 || username != oldusername) {
				saves = online;
				updatePlayerPrefs ();
			} else{
				//saves = local;
				//updatePlayerPrefs ();
			}



		}
	}	

}
