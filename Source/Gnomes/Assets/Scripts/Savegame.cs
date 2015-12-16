using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;
using SimpleJSON;
using System;

public class Savegame{

	//private int id;
	//private static int next;
	public Vector3 P1Pos;
	public float P1Health;
	public int P1Score;
	public Vector3 P2Pos;
	public float P2Health;
	public int P2Score;
	public string chapter;
	//private DateTime timeStamp;

	public String toString()
	{
		String temp = P1Pos.ToString() + "," + P1Health + "," + P1Score + "," + P2Pos.ToString() + "," + P2Health + "," + P2Score;
		return temp;
	}

	public Savegame(Vector3 P1Pos,float P1Health, int P1Score, Vector3 P2Pos, float P2Health, int P2Score, string chapter)
	{
		//this.id = next;
		//Savegame.next = Savegame.next + 1;
		this.P1Pos = P1Pos;
		this.P1Health = P1Health;
		this.P1Score = P1Score;
		this.P2Pos = P2Pos;
		this.P2Health = P2Health;
		this.P2Score = P2Score;
		this.chapter = chapter;
		//this.timeStamp = timeStamp;
	}

	//get savegame in JSON format
	public static String getJSON(Savegame savegame){
		JSONClass JSONsavegame = new JSONClass();

		//JSONsavegame.Add("id",new JSONData(savegame.id));

		JSONsavegame.Add("P1XPos",new JSONData(savegame.P1Pos.x));
		JSONsavegame.Add("P1YPos",new JSONData(savegame.P1Pos.y));
		JSONsavegame.Add("P1ZPos",new JSONData(savegame.P1Pos.z));
		JSONsavegame.Add("P1Health",new JSONData(savegame.P1Health));
		JSONsavegame.Add("P1Score",new JSONData(savegame.P1Score));

		JSONsavegame.Add("P2XPos",new JSONData(savegame.P2Pos.x));
		JSONsavegame.Add("P2YPos",new JSONData(savegame.P2Pos.y));
		JSONsavegame.Add("P2ZPos",new JSONData(savegame.P2Pos.z));
		JSONsavegame.Add("P2Health",new JSONData(savegame.P2Health));
		JSONsavegame.Add("P2Score",new JSONData(savegame.P2Score));

		JSONsavegame.Add("chapter",new JSONData(savegame.chapter));
		//JSONsavegame.Add("timeStamp",new JSONData(savegame.timeStamp.ToString("yyyy-MM-dd HH:mm:ss")));

		return JSONsavegame.ToString();
	}

	public static Savegame parseJSON(String savegame){
		var temp = JSON.Parse(savegame);
		Vector3 P1Pos = new Vector3(temp["P1XPos"].AsFloat,temp["P1YPos"].AsFloat,temp["P1ZPos"].AsFloat);
		float P1Health = temp["P1Health"].AsFloat; 
		int P1Score = temp["P1Score"].AsInt; 
		Vector3 P2Pos = new Vector3(temp["P2XPos"].AsFloat,temp["P2YPos"].AsFloat,temp["P2ZPos"].AsFloat);; 
		float P2Health  = temp["P2Health"].AsFloat;
		int P2Score  = temp["P2Score"].AsInt;
		string chapter = temp["chapter"].Value; 
		//DateTime timeStamp = DateTime.Parse(temp["chapter"].Value); 

		return new Savegame(P1Pos,P1Health,P1Score,P2Pos,P2Health,P2Score,chapter);
	}

//	public Savegame returnLatest(Savegame savegame)
//	{
//		int temp = DateTime.Compare(this.timeStamp,savegame.gettimeStamp());
//		if (temp>0)
//		{
//			return this;
//		}else
//		{
//			return savegame;
//		}
//	}

	//getter for the timeStamp
//	public DateTime gettimeStamp(){
//		return this.timeStamp;
//	}
	
}
