using UnityEngine;
using System.Collections;

public class Demolish : MonoBehaviour
{

	private int playerNum;
	public int hitDamage = 25;
	private GameObject hammer;
	private float hammertimer = 0;

	void Awake ()
	{
		playerNum = GetComponent<PlayerController> ().playerNum;
	}

	void Update ()
	{
		if (Input.GetButton ("Item" + playerNum)) {
			hammertimer += Time.deltaTime;
			hammer = transform.FindChild ("Hammer").gameObject;
			if (hammertimer < 0.5) {
				hammer.transform.localEulerAngles -= new Vector3 (5, 0, 0);
			} else if (hammertimer < 1) {
				hammer.transform.localEulerAngles += new Vector3 (5, 0, 0);
			} else {
				hammer.transform.localEulerAngles = new Vector3 (0, 0, 0);
				hammertimer = 0;
			}
		}
	}

	void OnCollisionStay (Collision other)
	{
		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Breakable")) {
			Debug.Log ("verwijder");
			other.gameObject.SetActive (false);
		}

		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Stone")) {
			other.gameObject.GetComponent<StoneLevel11> ().force = new Vector3 (10000000, 0, 0);
			other.gameObject.GetComponent<StoneLevel11> ().move = true;
		}
	}

	void OnCollisonEnter (Collision other)
	{
		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Enemy")) {
			other.gameObject.GetComponent<EnemyProperties> ().health -= hitDamage;

		}
	}

}
