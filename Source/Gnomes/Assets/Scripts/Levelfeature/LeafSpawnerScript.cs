using UnityEngine;
using System.Collections;

public class LeafSpawnerScript : MonoBehaviour {
    public Rigidbody leaf;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            float rnd = Random.value * 50;
            Rigidbody clone = (Rigidbody)Instantiate(leaf, new Vector3(rnd, 0, 0), transform.rotation);
        }
    }
}
