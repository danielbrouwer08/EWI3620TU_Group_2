using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    private GameObject prefab;
    private int blockcap = 3;
    private Rigidbody rb;
    private GameObject[] blocks;
    private int i = 0;
	private string Fire3;
    private int playerNum;

	void Start ()
    {
        playerNum = GetComponent<PlayerController>().playerNum;
        rb = GetComponent<Rigidbody>();
        blocks = new GameObject[blockcap];
		Fire3 = null;
        prefab = Resources.Load("Block") as GameObject;
	}
	
	void Update ()
    {
		if(Input.GetButtonDown("Item" + playerNum))
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

