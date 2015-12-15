using UnityEngine;
using System.Collections;

public class LeafSpawnerScript : MonoBehaviour {
    public Rigidbody leaf;

    // Use this for initialization
    void Start()
    {
        Invoke("Spawn", 1);
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

        //}
    }

    void Spawn()
    {
        float randomTime = Random.Range(1, 5);

        float rnd = Random.value * 50;
        Rigidbody clone = (Rigidbody)Instantiate(leaf, new Vector3(rnd, 0, 0), transform.rotation);

        Invoke("Spawn", randomTime);
    }
}
