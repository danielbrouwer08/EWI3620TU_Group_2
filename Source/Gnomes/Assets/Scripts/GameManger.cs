using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class GameManger : MonoBehaviour {

	private float P1_XPOS;
	private float P1_YPOS;
	private float P1_ZPOS;
	private float P2_XPOS;
	private float P2_YPOS;
	private float P2_ZPOS;
	private int P1_Health;
	private int P1_Score;
	private string Chapter;
	private string TimeStamp;

	// Use this for initialization
	void Start () {
		StartCoroutine(GetSaveGame());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator GetSaveGame()
	{
		UnityWebRequest www = UnityWebRequest.Get("http://drproject.twi.tudelft.nl:8083/getlocation");
		yield return www.Send();
		
		if (www.isError)
		{
			Debug.Log(www.error);
		}
		else
		{
			// Show results as text
			Debug.Log(www.downloadHandler.text);
			string receivedString = www.downloadHandler.text;
			
			var a = JSON.Parse(receivedString);

			
		}
	}	

}
