using UnityEngine;
using System.Collections;

public class LeafSpawnerScript : MonoBehaviour {
    public Rigidbody leaf;
    public float spawnrange;
	public float playerInRangeDistance = 80;
	private GameObject[] player;

    // Use this for initialization
    void Start()
    {
		player = GameObject.FindGameObjectsWithTag("Player");
        Invoke("Spawn", 1);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * 0.5f * spawnrange);
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0) * 0.5f * spawnrange);
    }

    void Spawn()
    {
		if((player[0].transform.position.x>this.transform.position.x-playerInRangeDistance && player[0].transform.position.x<this.transform.position.x+playerInRangeDistance) || (player[1].transform.position.x>this.transform.position.x-playerInRangeDistance && player[1].transform.position.x<this.transform.position.x+playerInRangeDistance))
		{
        	float randomTime = Random.Range(1, spawnrange * 0.1f);

       	 	float rnd = Random.value * spawnrange;
			Rigidbody clone = (Rigidbody)Instantiate(leaf, new Vector3(rnd + transform.position.x - 0.5f * spawnrange, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0.0f,0.0f,90.0f));

        	Invoke("Spawn", randomTime);
		}
    }
}
