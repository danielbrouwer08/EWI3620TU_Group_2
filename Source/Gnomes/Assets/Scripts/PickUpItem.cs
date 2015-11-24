using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    private GameObject player;
    public float throwforce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private Rigidbody rb;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player found");
            player = other.gameObject;
            hasPlayer = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        hasPlayer = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	// Update is called once per frame
	void Update () {
        if (beingCarried)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(player.GetComponent<Build>());
                rb.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                rb.AddForce(player.transform.forward * throwforce);
            }
        }
        else
        {
            Debug.Log("Not being carried");
            if (Input.GetMouseButtonDown(0) && hasPlayer)
            {
                Debug.Log(player.name);
                player.GetComponent<Build>();
                rb.isKinematic = true;
                transform.parent = player.transform;
                transform.localPosition = new Vector3(0.0f, 0.75f, 0.0f);
                //KAN OOK NOG DE HOEK VAN T OBJECT VERANDEREN
                beingCarried = true;
            }
        }
    }
}
