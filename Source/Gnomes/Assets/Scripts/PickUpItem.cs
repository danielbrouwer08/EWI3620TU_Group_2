using UnityEngine;
using System.Collections;
using System;

public class PickUpItem : MonoBehaviour
{
<<<<<<< HEAD
    private Vector3 startpos;
=======
>>>>>>> feature/Level_2
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
<<<<<<< HEAD
        startpos = transform.position;
=======
>>>>>>> feature/Level_2
    }

    void Update()
    {

        playerinrange = -1;
<<<<<<< HEAD
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
=======
        for (int i = 0; i < 2; i++)
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
                carrier = null;
>>>>>>> feature/Level_2
                rb.isKinematic = false;
                rb.detectCollisions = true;
                transform.parent = null;
                rb.AddForce(carrier.transform.forward * throwforce);
<<<<<<< HEAD
                carrier = null;
=======
>>>>>>> feature/Level_2
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
<<<<<<< HEAD
                transform.localPosition = new Vector3(0.0f, 1.53f, 0.0f);
=======
                transform.localPosition = new Vector3(0.0f, 0.75f, 0.0f);
>>>>>>> feature/Level_2
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
