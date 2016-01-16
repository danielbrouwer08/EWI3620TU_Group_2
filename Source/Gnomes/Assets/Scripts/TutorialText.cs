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
		if(between (10,60))
		{
			header.SetActive(true);
			string displayText = "P1 can move with the <color=red>[arrow]</color> keys. P2 can move with the <color=red>[W,A,S,D]</color> keys. To run press <color=red>[RIGHT-SHIFT] (P1)</color> or <color=red>[LEFT-SHIFT] (P2)</color>. To jump press <color=red>[/] (P1)</color> or <color=red>[C] (P2)</color>. Don't let the wave get to close!";
			text.GetComponent<Text>().text=displayText;
		}
		else if(between (70,100))
		{
			header.SetActive(true);
			string displayText = "Both players can pick up several skillitems which are needed to proceed in the level. However the functionality of each item depends on the gnome type (fat or skinny). To pickup/throw an item press <color=red>[.] (P1)</color> or <color=red>[V] (P2)</color>. To use the item press <color=red>[,] (P1)</color> or <color=red>[B] (P2)</color>";
			text.GetComponent<Text>().text=displayText;
		}else if (between(110,140))
		{
			header.SetActive(true);
			string displayText = "Make sure that both the players touch the checkpoint (it will turn green) because it functions as a respawn point when one of them dies.";
			text.GetComponent<Text>().text=displayText;
		}else if (between(200,340))
		{
			header.SetActive(true);
			string displayText = "In this part of the stage you should avoid waterpools and getting hit by arrows as it will decrease your health. Mind you, the enemies in this part of the stage are invulnerable so don't try anything funny. Try to pickup as many coins as possible to gain score.";
			text.GetComponent<Text>().text=displayText;
		}
		else if (between(360,400))
		{
			header.SetActive(true);
			string displayText = "With the wingsuit the skinny player can Float to the other side. To enable/disable Float mode press <color=red>[,]</color> (P1). The hammer can be used by the fat player. To use it press <color=red>[B] (P2)</color>. Try to hit the boulder and see what happends. Remember: to pickup/throw an item press <color=red>[.] (P1)</color> or <color=red>[V] (P2)</color>.";
			text.GetComponent<Text>().text=displayText;
		}
		else if (between(630,660))
		{
			header.SetActive(true);
			string displayText = "Some targets can trigger certain events. A target has a switch/lever that needs to be pulled <color=red>(P1 press [.], P2 press [V])</color>. When the target has a green color it can be triggered by throwing a block against it";
			text.GetComponent<Text>().text=displayText;
		}else if (between(830,980))
		{
			header.SetActive(true);
			string displayText = "Both of you, step on the giant checkpoint to proceed to the next stage!";
			text.GetComponent<Text>().text=displayText;
		}else
		{
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
