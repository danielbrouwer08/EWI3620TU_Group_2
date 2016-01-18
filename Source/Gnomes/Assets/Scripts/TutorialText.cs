using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {

	public GameObject header;
	public GameObject text;
	public GameObject player1;
	public GameObject player2;
	private bool isSinglePlayer;
	private string displayText;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString("playermode") == "single")
		{
			isSinglePlayer = true;
		}
		else
		{
			isSinglePlayer = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(between (10,60))
		{
			header.SetActive(true);
			if(isSinglePlayer)
			{
				displayText = "To move use the <color=red>[ARROW]</color> keys. Press <color=red>[RIGHT-SHIFT]</color> to run and <color=red>[/]</color> to jump. To switch between players press <color=red>[']</color>. With the <color=red>[;]</color> you can toggle follow mode on/off for the inactive player. Don't let the wave get too close!";
			}else
			{
				displayText = "P1 can move with the <color=red>[ARROW]</color> keys. P2 can move with the <color=blue>[W,A,S,D]</color> keys. To run press <color=red>[RIGHT-SHIFT] (P1)</color> or <color=blue>[LEFT-SHIFT] (P2)</color>. To jump press <color=red>[/] (P1)</color> or <color=blue>[C] (P2)</color>. Don't let the wave get too close!";
			}
			text.GetComponent<Text>().text=displayText;
		}
		else if(between (70,100))
		{
			header.SetActive(true);
			if(isSinglePlayer)
			{
				displayText = "Both players can pick up several skillitems which are needed to proceed in the level. However the functionality of each item depends on the gnome type (fat or skinny). To pickup/throw an item press <color=red>[.]</color>. To use the item press <color=red>[,]</color>. Remember: to switch between players press <color=red>[']</color>";
			}else
			{
				displayText = "Both players can pick up several skillitems which are needed to proceed in the level. However the functionality of each item depends on the gnome type (fat or skinny). To pickup/throw an item press <color=red>[.] (P1)</color> or <color=blue>[V] (P2)</color>. To use the item press <color=red>[,] (P1)</color> or <color=blue>[B] (P2)</color>";
			}
			text.GetComponent<Text>().text=displayText;
		}else if (between(110,140))
		{
			header.SetActive(true);
			displayText = "Make sure that both the players touch the checkpoint (it will turn green) because it functions as a respawn point when one of them dies.";
			text.GetComponent<Text>().text=displayText;
		}else if (between(200,340))
		{
			header.SetActive(true);
			displayText = "In this part of the stage you should avoid waterpools and getting hit by arrows as it will decrease your health. Mind you, the enemies in this part of the stage are invulnerable so don't try anything funny. Try to pickup as many coins as possible to gain score.";
			text.GetComponent<Text>().text=displayText;
		}
		else if (between(360,400))
		{
			header.SetActive(true);
			if(isSinglePlayer)
			{
				displayText = "With the wingsuit the skinny player can Float to the other side. To enable/disable Float mode press <color=red>[,]</color>. The hammer can be used by the fat player. To use it press <color=red>[,]</color>. Try to hit the boulder and see what happends. Remember: to pickup/throw an item press <color=red>[.]</color>";
			}else
			{
				displayText = "With the wingsuit the skinny player can Float to the other side. To enable/disable Float mode press <color=red>[,]</color> (P1). The hammer can be used by the fat player. To use it press <color=blue>[B] (P2)</color>. Try to hit the boulder and see what happends. Remember: to pickup/throw an item press <color=red>[.] (P1)</color> or <color=blue>[V] (P2)</color>.";
			}
			text.GetComponent<Text>().text=displayText;
		}
		else if (between(630,660))
		{
			header.SetActive(true);
			if(isSinglePlayer)
			{
				displayText = "Some targets can trigger certain events. A target has a switch/lever that needs to be pulled <color=red>(press [.])</color>. When the target has a green color it can be triggered by throwing a block against it";
			}else
			{
				displayText = "Some targets can trigger certain events. A target has a switch/lever that needs to be pulled <color=red>(P1 press [.] </color> , <color=blue>P2 press [V])</color>. When the target has a green color it can be triggered by throwing a block against it";
			}
			text.GetComponent<Text>().text=displayText;
		}else if (between(830,980))
		{
			header.SetActive(true);
			if(isSinglePlayer)
			{
				displayText = "Position both players on the giant checkpoint to proceed to the next stage!";
			}else
			{
				displayText = "Both of you, step on the giant checkpoint to proceed to the next stage!";
			}
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
