using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    private Transform player;
    public float throwforce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private Rigidbody rb;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            player = other.transform;
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
            if (Input.GetKeyDown("e"))
            {
                Destroy(player.gameObject.GetComponent<Fly>());
                rb.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                rb.AddForce(player.forward * throwforce);
            }
        }
        else
        {
            if (Input.GetKeyDown("e") && hasPlayer)
            {
                player.gameObject.AddComponent<Fly>();
                rb.isKinematic = true;
                transform.parent = player;
                //DE Y-WAARDE IS AFHANKELIJK VAN HOE GROOT DE PLAYER IS
                transform.localPosition = new Vector3(0.0f, 1.3f, 0.0f);
                //KAN OOK NOG DE HOEK VAN T OBJECT VERANDEREN
                beingCarried = true;
            }
        }

        //if(rb.velocity.sqrMagnitude < 0.01)
        //{
        //    rb.isKinematic = true;
        //}
    }
}
