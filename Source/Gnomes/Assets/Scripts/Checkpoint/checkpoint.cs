using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class checkpoint : MonoBehaviour {

	private bool player1In = false;
	private bool player2In = false;
	private GameObject player1;
	private GameObject player2;
	private int state = 0;

	// Update is called once per frame
	void Update () {

		if(player1In && player2In && (state == 0 || state == 1))
		{
			state = 2;
			this.GetComponent<Renderer>().material = Resources.Load("checkpointstate2") as Material;
			player1In = false;
			player2In = false;
			saveCheckpoint();
		}else if ((player1In || player2In) && state == 0)
		{
			state = 1;
			this.GetComponent<Renderer>().material = Resources.Load("checkpointstate1") as Material;
		}
	}

	void saveCheckpoint(){
		PlayerProperties p1prop = player1.GetComponent<PlayerProperties>();
		PlayerProperties p2prop = player2.GetComponent<PlayerProperties>();
		PlayerController p1cont = player1.GetComponent<PlayerController>();
		PlayerController p2cont = player2.GetComponent<PlayerController>();

		//store p1 health and score
		PlayerPrefs.SetFloat("P1_Health",p1prop.health);
		PlayerPrefs.SetFloat("P1_Score",p1prop.score);

		//store p2 health and score
		PlayerPrefs.SetFloat("P2_Health",p2prop.health);
		PlayerPrefs.SetFloat("P2_Score",p2prop.score);
	
		//store p1 spawn location
		PlayerPrefs.SetFloat("P1_XPOS",transform.position.x - 1.5f);
		PlayerPrefs.SetFloat("P1_YPOS",transform.position.y + 3.0f);
		PlayerPrefs.SetFloat("P1_ZPOS",transform.position.z);

		//store p2 spawn location
		PlayerPrefs.SetFloat("P2_XPOS",transform.position.x + 1.5f);
		PlayerPrefs.SetFloat("P2_YPOS",transform.position.y + 3.0f);
		PlayerPrefs.SetFloat("P2_ZPOS",transform.position.z);

		//store date and time of savefile
		PlayerPrefs.SetString ("TimeStamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

		//store current chaptername
		PlayerPrefs.SetString("Chapter",Application.loadedLevelName);

		//Upload the data
		StartCoroutine(Upload());

		//PlayerPrefs.SetString("TimeStamp",System.DateTime.Now);

	}

	IEnumerator Upload()
	{
		Debug.Log("in upload()");
		WWWForm form = new WWWForm();
		string location = transform.position.x + " " + transform.position.y + " " + transform.position.z;
		//Debug.Log(location);
		//form.AddField("location",location);

		form.AddField("P1_Health",PlayerPrefs.GetFloat("P1_Health").ToString());
		form.AddField("P1_Score",PlayerPrefs.GetFloat("P1_Score").ToString());

		form.AddField("P2_Health",PlayerPrefs.GetFloat("P2_Health").ToString());
		form.AddField("P1_Score",PlayerPrefs.GetFloat("P2_Score").ToString());

		//send p1 spawn location
		form.AddField("P1_XPOS",PlayerPrefs.GetFloat("P1_XPOS").ToString());
		form.AddField("P1_YPOS",PlayerPrefs.GetFloat("P1_YPOS").ToString());
		form.AddField("P1_ZPOS",PlayerPrefs.GetFloat("P1_ZPOS").ToString());

		//send p2 spawn location
		form.AddField("P2_XPOS",PlayerPrefs.GetFloat("P2_XPOS").ToString());
		form.AddField("P2_YPOS",PlayerPrefs.GetFloat("P2_YPOS").ToString());
		form.AddField("P2_ZPOS",PlayerPrefs.GetFloat("P2_ZPOS").ToString());

		form.AddField("TimeStamp",PlayerPrefs.GetString ("TimeStamp"));

		form.AddField("Chapter",PlayerPrefs.GetString("Chapter"));


		Debug.Log(form.data.GetLength(0));
		
		UnityWebRequest www = UnityWebRequest.Post("http://drproject.twi.tudelft.nl:8083/sendlocation", form);
		
		Debug.Log("in using");
		yield return www.Send();
		Debug.Log("na return");
		if (www.isError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log("Form upload complete!");
		}
	}

	void OnCollisionEnter(Collision collision) {

		if(collision.gameObject.tag.Equals("Player"))
		   {
			if (collision.gameObject.GetComponent<PlayerController>().playerNum == 1)
			{
				player1In = true;
				player1 = collision.gameObject;
			}
			else if (collision.gameObject.GetComponent<PlayerController>().playerNum == 2)
			{
				player2In = true;
				player2 = collision.gameObject;
			}
		
		}
	}

}

