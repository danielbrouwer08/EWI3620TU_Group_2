using UnityEngine;
using System.Collections;

public class killMouse : MonoBehaviour {

    public int damage;
    public GameObject hammer;
    private GameObject[] players;
    private Vector3 position;
    private Quaternion rotation;
    private bool hammerdown;
    public float xrot;
    public float yrot;
    public float zrot;
    public float xrot2;
    public float yrot2;
    public float zrot2;
    private bool status1;


    // Use this for initialization
    void Start () {

        xrot = 0;
        yrot = 0;
        zrot = 0;

        xrot2 = 320;
        yrot2 = 10;
        zrot2 = -2;

        position = GetComponent<Transform>().position;
        rotation = GetComponent<Transform>().rotation;
        players = GameObject.FindGameObjectsWithTag("Player");
        

	}
	
    void FixedUpdate()
    {
        GameObject Mouse = GameObject.FindGameObjectWithTag("Enemy");
        foreach(GameObject cur in players)
        {
            if (cur.GetComponent<Demolish>())
            {
                Destroy(cur.GetComponent<Demolish>());
            }

            if (cur.GetComponent<PlayerProperties>().item != null && Mouse != null)
            {
                if(cur.GetComponent<PlayerProperties>().item.tag == "Hammer" && (Input.GetButtonDown("Item1") || Input.GetButtonDown("Item2")))
                {
                    hammerdown = true;
                    status1 = true;
                }

                if (cur.GetComponent<PlayerProperties>().item.tag == "Hammer" && (Input.GetButtonDown("Item1") || Input.GetButtonDown("Item2")) && Vector3.Distance(transform.position, Mouse.transform.position) < 10)
                {
                    Mouse.GetComponentInChildren<EnemyProperties>().health -= damage;
                    Instantiate(hammer, position, rotation);
                    Destroy(gameObject);
                }
            }

        }

        if (hammerdown && transform.parent != null && transform.parent.name != "level7")
        {
            //Debug.Log("check");
            if (status1)
            {
                this.transform.localEulerAngles = new Vector3(xrot2, yrot2, zrot2);
                status1 = false;
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(xrot, yrot, zrot);
                hammerdown = false;
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
