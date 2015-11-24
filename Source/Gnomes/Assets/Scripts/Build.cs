using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    public GameObject prefab;
    public int blockcap;
    private Rigidbody rb;
    private GameObject[] blocks;
    private int i = 0;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blocks = new GameObject[blockcap];
	}
	
	void Update ()
    {
	    if(Input.GetButtonDown("Fire3"))
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
