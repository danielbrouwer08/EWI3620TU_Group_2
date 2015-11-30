using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    private GameObject prefab;
    private int blockcap = 3;
    private Rigidbody rb;
    private GameObject[] blocks;
    private int i = 0;
	private string Fire3;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blocks = new GameObject[blockcap];
		Fire3 = null;
        prefab = Resources.Load("Block") as GameObject;
		if(this.GetComponent<PlayerController>().playerNum == 1)
		{
			Fire3 = "Fire3Player";
		}else if(this.GetComponent<PlayerController>().playerNum == 2)
		{
			Fire3 = "Fire3Companion";
		}else {
			print ("Player " + this.GetComponent<PlayerController>().playerNum + " is not valid");
		}
	}
	
	void Update ()
    {
		if(Input.GetButtonDown(Fire3))
        {
            Destroy(blocks[i]);
            blocks[i] = Instantiate(prefab, rb.position + transform.forward + new Vector3(0, -0.5f, 0), Quaternion.identity) as GameObject;
            i++;
            if(i >= blockcap)
            {
                i = 0;
            }
        }
	}
}

