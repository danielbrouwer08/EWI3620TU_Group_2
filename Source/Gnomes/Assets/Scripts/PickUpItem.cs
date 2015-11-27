using UnityEngine;
using System.Collections;
using System;

public class PickUpItem : MonoBehaviour
{
    private Vector3 startpos;
    public float pickdistance = 5;
    private GameObject[] player;
    private int playerinrange;
    private GameObject carrier;
    public float throwforce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private Rigidbody rb;
    private Collider col;
    public string skill;

    void OnCollisionExit(Collision other)
    {
        hasPlayer = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.FindGameObjectsWithTag("Player");
        startpos = transform.position;
    }

    void Update()
    {

        playerinrange = -1;
        for (int i = 0; i < player.Length; i++)
        {
            if (Vector3.Magnitude(transform.position - player[i].transform.position) < pickdistance)
            {
                playerinrange = i;
            }
        }

        if (carrier != null)
        {
            if (Input.GetKeyDown("e"))
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
            if (Input.GetKeyDown("e") && playerinrange >= 0)
            {
                carrier = player[playerinrange];
                AddSkilltoPlayer(skill);
                rb.isKinematic = true;
                rb.detectCollisions = false;
                transform.parent = player[playerinrange].transform;
                transform.localPosition = new Vector3(0.0f, 1.53f, 0.0f);
                //KAN OOK NOG DE HOEK VAN T OBJECT VERANDEREN
            }
        }
        //NAAR DEZE WAARDEN NOG EVEN KIJKEN
        if(Math.Abs(transform.position.x) > 50 || Math.Abs(transform.position.y) > 50 || Math.Abs(transform.position.z) > 50)
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = startpos;
            transform.rotation = Quaternion.identity;
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
