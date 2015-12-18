using UnityEngine;
using System.Collections;

public class LeafSpawnerScript : MonoBehaviour {
    public Rigidbody leaf;
    public float spawnrange;

    // Use this for initialization
    void Start()
    {
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
        float randomTime = Random.Range(1, spawnrange * 0.1f);

        float rnd = Random.value * spawnrange;
        Rigidbody clone = (Rigidbody)Instantiate(leaf, new Vector3(rnd + transform.position.x - 0.5f * spawnrange, transform.position.y, 1f), transform.rotation);

        Invoke("Spawn", randomTime);
    }
}
