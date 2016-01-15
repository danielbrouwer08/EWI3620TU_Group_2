using UnityEngine;
using System.Collections;

public class killMouse : MonoBehaviour {

    public int damage;
    public GameObject hammer;
    private GameObject[] players;
    private Vector3 position;
    private Quaternion rotation;

	// Use this for initialization
	void Start () {

        position = GetComponent<Transform>().position;
        rotation = GetComponent<Transform>().rotation;
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
    void FixedUpdate()
    {
        GameObject Mouse = GameObject.FindGameObjectWithTag("Enemy");
        foreach(GameObject cur in players)
        {
            if(cur.GetComponent<PlayerProperties>().item != null && Mouse != null)
            {
                if (cur.GetComponent<PlayerProperties>().item.tag == "Hammer" && (Input.GetButtonDown("Item1") || Input.GetButtonDown("Item2")) && Vector3.Distance(transform.position, Mouse.transform.position) < 10)
                {
                    Mouse.GetComponentInChildren<EnemyProperties>().health -= damage;
                    Instantiate(hammer, position, rotation);
                    Destroy(gameObject);
                    
                    Debug.Log("Distance is: " + Vector3.Distance(transform.position, Mouse.transform.position));
                }
            }

        }

    }

	//void OnTriggerEnter (Collider other) {
	  //  if(other.gameObject.tag == "Enemy")
        //{
          //  Instantiate(hammer, position, rotation);
            //Destroy(gameObject);
            //
            //other.gameObject.GetComponent<EnemyProperties>().health = other.gameObject.GetComponent<EnemyProperties>().health - damage; 
        //}
	//}
}
