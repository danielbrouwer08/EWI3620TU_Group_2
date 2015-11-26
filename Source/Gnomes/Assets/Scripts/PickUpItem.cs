using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    public float pickdistance = 5;
    private GameObject[] player;
    private int playerinrange;
    private GameObject carrier;
    public float throwforce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private Rigidbody rb;
    private Collider col;
    public struct T { };

    void OnCollisionExit(Collision other)
    {
        hasPlayer = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }

	void Update () {

        playerinrange = -1;
        for(int i = 0; i < 2; i++)
        {
            if(Vector3.Magnitude(transform.position - player[i].transform.position) < pickdistance)
            {
                playerinrange = i;
            }
        }

        if (carrier != null)
        {
            if (Input.GetKeyDown("e"))
            {
                Destroy(carrier.GetComponent<Fly>());
                carrier = null;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                transform.parent = null;
                rb.AddForce(carrier.transform.forward * throwforce);
            }
        }
        else
        {
            if (Input.GetKeyDown("e") && playerinrange >= 0)
            {
                carrier = player[playerinrange];
                Debug.Log(carrier.name);
                carrier.AddComponent<T>();
                rb.isKinematic = true;
                rb.detectCollisions = false;
                transform.parent = player[playerinrange].transform;
                transform.localPosition = new Vector3(0.0f, 0.75f, 0.0f);
                //KAN OOK NOG DE HOEK VAN T OBJECT VERANDEREN
            }
        }
    }
}
