using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour
{
    public float pickdistance = 5;
    private GameObject[] player;
    private GameObject carrier;
    public float throwforce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private Rigidbody rb;
    private Collider col;
    public string skill;
    private int playerNum;

    void OnCollisionExit(Collision other)
    {
        hasPlayer = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.FindGameObjectsWithTag("Player");
        if(player[0].GetComponent<PlayerController>().playerNum != 1)
        {
            GameObject temp = player[0];
            player[0] = player[1];
            player[1] = player[0];
        }
    }

    void Update()
    {
        if (carrier != null)
        {
            if (Input.GetButtonDown("Interact" + playerNum))
            {
                DeleteSkillfromPlayer(skill);
                rb.isKinematic = false;
                rb.detectCollisions = true;
                transform.parent = null;
                rb.AddForce(carrier.transform.forward * throwforce);
                carrier = null;
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                int n = i + 1;
                if (Input.GetButtonDown("Interact" + n) && Vector3.Magnitude(transform.position - player[i].transform.position) < pickdistance)
                {
                    carrier = player[i];
                    playerNum = i + 1;
                    AddSkilltoPlayer(skill);
                    rb.isKinematic = true;
                    rb.detectCollisions = false;
                    transform.parent = player[i].transform;
                    transform.localPosition = new Vector3(0.0f, 0.75f, 0.0f);
                    //KAN OOK NOG DE HOEK VAN T OBJECT VERANDEREN
                }
            }
        }
    }

    void AddSkilltoPlayer(string skill)
    {
        switch (skill)
        {
            case "Fly": carrier.AddComponent<Fly>(); break;
            case "Float": carrier.AddComponent<Float>(); break;
            case "Build": carrier.AddComponent<Build>(); break;
            case "Demolish": carrier.AddComponent<Demolish>(); break;
        }
    }

    void DeleteSkillfromPlayer(string skill)
    {
        switch (skill)
        {
            case "Fly": Destroy(carrier.GetComponent<Fly>()); break;
            case "Float": Destroy(carrier.GetComponent<Float>()); break;
            case "Build": Destroy(carrier.GetComponent<Build>()); break;
            case "Demolish": Destroy(carrier.GetComponent<Demolish>()); break;
        }
    }
}
