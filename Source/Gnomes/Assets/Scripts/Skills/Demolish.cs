using UnityEngine;
using System.Collections;

public class Demolish : MonoBehaviour {

    private int playerNum;
	public int hitDamage = 25;

	void Awake ()
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

	void OnCollisonEnter(Collision other)
	{
		if(Input.GetButton("Item" + playerNum) && other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.GetComponent<EnemyProperties>().health -= hitDamage;

		}
	}

}
