using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    public GameObject prefab = Resources.Load("Block") as GameObject;
    public int blockcap = 3;
    private Rigidbody rb;
    private GameObject[] blocks;
    private int j = 0;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blocks = new GameObject[blockcap];
	}
	
	void Update ()
    {
	    if(Input.GetButtonDown("Fire3"))
        {
            Destroy(blocks[j]);
            blocks[j] = Instantiate(prefab, rb.position + transform.forward + new Vector3(0, -0.5f, 0), Quaternion.identity) as GameObject;
            j++;
            if(j >= blockcap)
            {
                j = 0;
            }
        }
	}

    void OnDestroy()
    {
        for(int i = 0; i < 3; i++)
        {
            Destroy(blocks[i]);
        }
    }


}
