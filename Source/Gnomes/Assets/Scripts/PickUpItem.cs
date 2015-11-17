using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    private Transform player;
    public float throwforce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private Rigidbody rb;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            hasPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        hasPlayer = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	// Update is called once per frame
	void Update () {
        Debug.Log(hasPlayer);
        if (beingCarried)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                rb.AddForce(player.forward * throwforce);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && hasPlayer)
            {
                rb.isKinematic = true;
                transform.parent = player;
                transform.localPosition = new Vector3(0.0f, 0.75f, 0.0f);
                //KAN OOK NOG DE HOEK VAN T OBJECT VERANDEREN
                beingCarried = true;
            }
        }
    }
}
