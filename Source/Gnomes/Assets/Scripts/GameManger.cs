using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;
using SimpleJSON;
using System.Collections.Generic;
using System;

public class GameManger : MonoBehaviour
{
	public  int saveslots = 3;
	private  Savegame[] saves = new Savegame[3];
	public  int currentslot;
	DateTime serverTimeStamp;
	//private Savegame[] serversaves = new Savegame[3];

	void Awake()
	{
		StartCoroutine(GetTimeStamp()); //get timestamp from server
		Savegame[] local = readPlayerPrefs (); //get local saves

		DateTime currentTimeStamp = DateTime.Parse(PlayerPrefs.GetString ("timeStamp"));
	
		int temp = DateTime.Compare(currentTimeStamp,serverTimeStamp);
		if (temp>0)
		{
			Debug.Log("using local save file");
			saves = local;
		}else
		{
			Debug.Log("using server save file");
			StartCoroutine(GetSaveGame()); //get new saves from server
		}

	}

	public Savegame returnCurrent(){
		return saves[currentslot];
	}

	//add new savegame to the savesarray and add to the playerprefs the json string
	public  void addNewSave (Savegame savegame)
	{
		saves [currentslot] = savegame;
		addToPlayerPrefs (savegame);


		//upload data to the server
		StartCoroutine (Upload ());
	}
	

	//Add JSON saves array to the playerprefs
	private  void addToPlayerPrefs (Savegame savegame)
	{
		PlayerPrefs.SetString("timeStamp",System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss"));
		for(int i = 0;i<saveslots;i++)
		{
			PlayerPrefs.SetString ("saveNo" + i, Savegame.getJSON (saves[i]));
		}

	}
	
	//Read the playerprefs and return the saves
	private  Savegame[] readPlayerPrefs ()
	{
		Savegame[] saves = new Savegame[saveslots];
		string temp;

		for (int i=0; i<saveslots; i++) {
			saves [i] = Savegame.parseJSON (PlayerPrefs.GetString ("saveNo" + i));
			//Debug.Log("Wat ik heb geparst: " + saves[i].toString());
		}

		return saves;
	}

	IEnumerator Upload ()
	{
		Debug.Log ("in upload()");
		WWWForm form = new WWWForm ();

		//Debug.Log("Trying to upload: " + toJSON ());

		//Add timestamp to data 
		form.AddField ("timeStamp",System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss"));
		form.AddField ("saveslots",saveslots);

		//Add all savestates to data in JSON format
		for(int i = 0;i<saveslots;i++)
		{
			form.AddField("saveNo" + i, PlayerPrefs.GetString ("saveNo" + i));
		}
		
		Debug.Log (form.data.GetLength (0));
		
		UnityWebRequest www = UnityWebRequest.Post ("http://drproject.twi.tudelft.nl:8083/sendsaves", form);
		
		Debug.Log ("in using");
		yield return www.Send ();
		Debug.Log ("na return");
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Form upload complete!");
		}
	}

	IEnumerator GetTimeStamp(){
		UnityWebRequest www = UnityWebRequest.Get ("http://drproject.twi.tudelft.nl:8083/getTimeStamp");
		yield return www.Send ();
		
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			// Show results as text
			Debug.Log (www.downloadHandler.text);
			string receivedString = www.downloadHandler.text;
			serverTimeStamp = DateTime.Parse(receivedString.Replace("\"",""));
			Debug.Log(receivedString);
		}
	}

	//Get saves over the interwebs
	IEnumerator GetSaveGame ()
	{
		UnityWebRequest www = UnityWebRequest.Get ("http://drproject.twi.tudelft.nl:8083/getsaves");
		yield return www.Send ();
		
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			// Show results as text
			Debug.Log (www.downloadHandler.text);
			string receivedString = www.downloadHandler.text;

			Debug.Log("received:");
			Debug.Log(receivedString);

			string[] parts = receivedString.Split(new string[] {"},{"}, System.StringSplitOptions.None);

			parts[0] = parts[0].Replace("[{","");
			parts[parts.Length-1] = parts[parts.Length-1].Replace("}]","");
			Savegame temp;

			Debug.Log("The parts:");
		
			for(int i=0;i<parts.Length;i++)
			{
				parts[i] = "{" + parts[i] + "}";
				temp = Savegame.parseJSON(parts[i]);
				saves[i] = temp; //add saves to saves list
				//Debug.Log("Partsno" + i + ": " + parts[i]);
				//Debug.Log("Extracted savefile" + i + ": " + temp.toString());
			}



		}
	}	

}
