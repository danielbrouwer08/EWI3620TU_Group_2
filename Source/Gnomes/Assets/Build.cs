using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    public GameObject prefab = Resources.Load("Block") as GameObject;
    public int blockcap = 3;
    private Rigidbody rb;
    private GameObject[] blocks;
<<<<<<< HEAD:Source/Gnomes/Assets/Scripts/Skills/Build.cs
    private int i = 0;
	private string Fire3;
=======
    private int j = 0;
>>>>>>> feature/Level_2:Source/Gnomes/Assets/Build.cs

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        blocks = new GameObject[blockcap];
		Fire3 = null;
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

