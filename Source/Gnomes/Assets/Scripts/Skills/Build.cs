using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    private GameObject prefab;
    private int blockcap = 5;
    private Rigidbody rb;
    private GameObject[] blocks;
    private int i = 0;
    private int playerNum;
    private bool isactive;

	void Start ()
    {
        playerNum = GetComponent<PlayerController>().playerNum;
        rb = GetComponent<Rigidbody>();
        blocks = new GameObject[blockcap];
        prefab = Resources.Load("Block") as GameObject;
	}
	
	void Update ()
    {
        isactive = GetComponent<PlayerController>().enabled;
		if(Input.GetButtonDown("Item" + playerNum) && isactive)
        {
            Destroy(blocks[i]);
            blocks[i] = Instantiate(prefab, rb.position + transform.forward + new Vector3(0, 3, 0), Quaternion.identity) as GameObject;
            i++;
            if(i >= blockcap)
            {
                i = 0;
            }
        }
	}

    void OnDisable()
    {
        for(int i = 0; i < blockcap; i++)
        {
            Destroy(blocks[i]);
        }
    }
}

