using UnityEngine;
using System.Collections;

public class Demolish : MonoBehaviour {

    void OnCollisionStay(Collision other){
		string Fire3 = null;
	
		if(this.GetComponent<PlayerController>().playerNum == 1)
		{
			Fire3 = "Fire3Player";
		}else if(this.GetComponent<PlayerController>().playerNum == 2)
		{
			Fire3 = "Fire3Companion";
		}else {
			print ("Player " + this.GetComponent<PlayerController>().playerNum + " is not valid");
		}



		if(Input.GetButton(Fire3) && other.gameObject.CompareTag("Breakable"))
			{
				Debug.Log("verwijder");
				other.gameObject.SetActive(false);
			}
	

    }
}
