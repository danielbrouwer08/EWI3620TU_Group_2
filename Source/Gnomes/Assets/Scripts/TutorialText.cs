using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {

	public GameObject header;
	public GameObject text;
	public GameObject player1;
	public GameObject player2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(between (10,40))
		{
			header.SetActive(true);
			string displayText = "P1 can move with the w,a,s,d keys. P2 can move with the I,J,K,L keys. To jump press Q (P1) or U (P2). To run press Z (P1) or M (P2)";
			text.GetComponent<Text>().text=displayText;
		}
		else if(between (70,100))
		{
			header.SetActive(true);
			string displayText = "Both players can pick up several skillitems which are needed to proceed in the level. However the functionality of each item depends on the gnome type (fat or skinny). To pickup/throw an item press E for P1 and O for P2. To use the item press R for P1 and P for P2";
			text.GetComponent<Text>().text=displayText;
		}else if (between(110,140))
		{
			header.SetActive(true);
			string displayText = "Make sure that both the players touch the checkpoint (it will turn green) because it functions as a respawn point when one of them dies";
			text.GetComponent<Text>().text=displayText;
		}else if (between(630,660))
		{
			header.SetActive(true);
			string displayText = "Some targets can trigger certain events. A target has a switch/lever that needs to be pulled (P1 press E, P2 press O). When the target has a green color it can be triggered by throwing an item against it";
			text.GetComponent<Text>().text=displayText;
		}else{
			header.SetActive(false);
		}


	}

	bool between(int x1, int x2)
	{
		if((player1.transform.position.x>x1 && player1.transform.position.x<x2)||(player2.transform.position.x>x1 && player2.transform.position.x<x2))
		{
			return true;
		}else
		{
			return false;
		}
	}
}
