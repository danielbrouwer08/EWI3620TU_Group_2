using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEditor;

public class PickUpItem : MonoBehaviour
{
    public float pickdistance = 5;
    private GameObject[] player;
    private int playerinrange;
    private int playerNum;
    private float xPosPlayer;
    private float zPosPlayer;
    private GameObject carrier;
    public float throwforce;
    bool hasPlayer = false;
    bool beingCarried = false;
    private Rigidbody rb;
    private Collider col;
    public string skill;
    private Vector3 startpos;


    void OnCollisionExit(Collision other)
    {
        hasPlayer = false;
    }
    void Start()
    {
        startpos = transform.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if(transform.position.z > 50 | transform.position.z < 0 | transform.position.x < 0)
        {
            transform.position = startpos;
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.velocity = Vector3.zero;
        
        }
        playerinrange = -1;
        for (int i = 0; i < player.Length; i++)
        {
            if (Vector3.Magnitude(transform.position - player[i].transform.position) < pickdistance)
            {
                playerNum = player[i].GetComponent<PlayerController>().playerNum;
                xPosPlayer = player[i].GetComponent<Transform>().position.x;
                zPosPlayer = player[i].GetComponent<Transform>().position.z;

                playerinrange = i;
                Debug.Log(playerNum);
            }
        }
        //Debug.Log(playerinrange);

        if (carrier != null)
        {
            if (Input.GetButtonDown("Interact" + playerNum))
            {
                DeleteSkillfromPlayer(skill);
                rb.isKinematic = false;
                rb.detectCollisions = true;
                transform.parent = null;
                rb.AddForce(carrier.transform.forward * throwforce + Vector3.up * throwforce * 0.1f);
                carrier = null;
            }
        }
        else
        {
            if(playerinrange >= 0)
            {
                if (Input.GetButtonDown("Interact" + playerNum))
                {
                    carrier = player[playerinrange];
                    AddSkilltoPlayer(skill);
                    rb.isKinematic = true;
                    rb.detectCollisions = false;
                    transform.parent = player[playerinrange].transform;
                    transform.localPosition = new Vector3(0.0f, 4.1f, 0.0f);
                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }

        }
    }

    void AddSkilltoPlayer(string skill)
    {
        var dict = new Dictionary<string, object>();
        dict["currentScene"] = EditorApplication.currentScene;
        dict["skill"] = skill;
        dict["playerNum"] = playerNum;

        Analytics.CustomEvent("pickUpItem", new Dictionary<string, object>
        {
            { "skill", skill },
            { "playerNum",  playerNum},
            { "xPos", xPosPlayer },
            { "zPos", zPosPlayer },
            { "currentScene", EditorApplication.currentScene }
        });

        UnityAnalyticsHeatmap.HeatmapEvent.Send("checkpoint", gameObject.transform.position, dict);

        if (playerNum == 1)
        {
            switch (skill)
            {
                case "Fly": carrier.AddComponent<Fly>(); break;
                case "Float": carrier.AddComponent<Float>(); break;
                // case "Build": carrier.AddComponent<Build>(); break;
                // case "Demolish": carrier.AddComponent<Demolish>(); break;
            }
        }
        else
        {

            switch (skill)
            {
                // case "Fly": carrier.AddComponent<Fly>(); break;
                // case "Float": carrier.AddComponent<Float>(); break;
                case "Build": carrier.AddComponent<Build>(); break;
                case "Demolish": carrier.AddComponent<Demolish>(); break;
            }
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
