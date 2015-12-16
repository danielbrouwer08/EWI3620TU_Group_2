using UnityEngine;
using System.Collections;

public class SavegameTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Vector3 p1SpawnPos = new Vector3(1.0f,3.3f,44.5f);
		Vector3 p2SpawnPos = new Vector3(0.5f,2.4f,33.1f);
		float p1Health = 10.5f;
		float p2Health = 15f;
		int p1Score = 100;
		int p2Score = 10099;
		string levelName = "Chapter1";

		Savegame testsave = new Savegame(p1SpawnPos, p1Health, p1Score, p2SpawnPos, p2Health, p2Score, levelName);
		string jsontest = Savegame.getJSON(testsave);
		Debug.Log("jsonstring: " + jsontest);
		Savegame parsedsave = Savegame.parseJSON(jsontest);
		Debug.Log (parsedsave.toString());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
