using UnityEngine;
using System.Collections;

public class Demolish : MonoBehaviour
{

	private int playerNum;
	public int hitDamage = 4;
	private GameObject hammer;
	//private float hammertimer = 0;
	private bool hammerdown;
	public float xoffset;
	public float yoffset;
	public float zoffset;
	public float xrot;
	public float yrot;
	public float zrot;
	public float xrot2;
	public float yrot2;
	public float zrot2;
	private bool status1;
	private bool isactive;

	void Awake ()
	{
		playerNum = GetComponent<PlayerController> ().playerNum;
		xoffset = 0f;
		yoffset = 3.19f;
		zoffset = 0f;

		xrot = 0;
		yrot = 0;
		zrot = 0;

		xrot2 = 320;
		yrot2 = 10;
		zrot2 = -2;
	}

	void Start ()
	{
		hammer = transform.GetComponentInChildren<PickUpItem> ().gameObject;
		if(hammer!=null)
		{
			hammer.transform.localPosition = new Vector3 (xoffset, yoffset, zoffset);
			hammer.transform.localEulerAngles = new Vector3 (xrot, yrot, zrot);
		}
	}

	void Update ()
	{
		isactive = GetComponent<PlayerController> ().enabled;
		if (Input.GetButton ("Item" + playerNum) && isactive) {
			hammerdown = true;
			status1 = true;
		}

		if (hammerdown) {
			if (status1) {
				hammer.transform.localEulerAngles = new Vector3 (xrot2, yrot2, zrot2);
				status1 = false;
			} else {
				hammer.transform.localEulerAngles = new Vector3 (xrot, yrot, zrot);
				hammerdown = false;
				//hammertimer = 0;
			}
		}

	}

	public void hammerCollision (Collision other)
	{
		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Breakable")) {
			other.gameObject.SetActive (false);
		}
		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Enemy")) {
			//Debug.Log ("Mouse is going down muhahahahaha");
			other.gameObject.GetComponent<EnemyProperties> ().dealDamage (hitDamage);		
		}
		
		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Stone")) {
			other.gameObject.GetComponent<StoneLevel11> ().move = true;
		}
		
	}


//Volgende stuk code werkt niet lekker want deze checkt op collision met player ipv de hamer :(
//	void OnCollisionStay (Collision other)
//	{
//		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Breakable")) {
//			Debug.Log ("verwijder");
//			other.gameObject.SetActive (false);
//		}
//
//		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Stone")) {
//			other.gameObject.GetComponent<StoneLevel11> ().force = new Vector3 (10000000, 0, 0);
//			other.gameObject.GetComponent<StoneLevel11> ().move = true;
//		}
//	}
//
//	void OnCollisonEnter (Collision other)
//	{
//		if (Input.GetButton ("Item" + playerNum) && other.gameObject.CompareTag ("Enemy")) {
//			other.gameObject.GetComponent<EnemyProperties> ().dealDamage (hitDamage);
//
//		}
//	}

}
