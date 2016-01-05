using UnityEngine;
using System.Collections;

public class Demolish : MonoBehaviour {

    private int playerNum;
	public int hitDamage = 25;
	private GameObject hammer;

	void Awake ()
	{
        playerNum = GetComponent<PlayerController>().playerNum;
    }

	void Update()
	{
		hammer = transform.FindChild("Hammer").gameObject;
		if(Input.GetButton("Item" + playerNum) && hammer.transform.localEulerAngles.x < 50)
		{
			hammer.transform.localEulerAngles += new Vector3(10, 0, 0);
		}
	}

    void OnCollisionStay(Collision other)
    {
		if(Input.GetButton("Item" + playerNum) && other.gameObject.CompareTag("Breakable"))
			{
				Debug.Log("verwijder");
				other.gameObject.SetActive(false);
			}

		if(Input.GetButton("Item" + playerNum) && other.gameObject.CompareTag("Stone"))
		{
			other.gameObject.GetComponent<StoneLevel11>().force = new Vector3(10000000,0,0);
			other.gameObject.GetComponent<StoneLevel11>().move = true;
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
