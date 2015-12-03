using UnityEngine;
using System.Collections;

public class Demolish : MonoBehaviour {

    private int playerNum;

	void Start()
    {
        playerNum = GetComponent<PlayerController>().playerNum;
    }

    void OnCollisionStay(Collision other)
    {
		if(Input.GetButton("Item" + playerNum) && other.gameObject.CompareTag("Breakable"))
			{
				Debug.Log("verwijder");
				other.gameObject.SetActive(false);
			}
    }
}
