using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
//using UnityEditor;

public class PickUpItem : MonoBehaviour
{
    public float pickdistance = 5;
    private GameObject[] player;
    private bool[] playerinrange;
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
        startpos = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.FindGameObjectsWithTag("Player");
        playerinrange = new bool[player.Length];
    }

    void Update()
    {
        if ((transform.position.z > 50 || transform.position.z < 0 || transform.position.x < 0) && carrier == null)
        {
            Respawnitem();
        }
        for (int i = 0; i < player.Length; i++)
        {
            playerNum = player[i].GetComponent<PlayerController>().playerNum;
            if (carrier == null)
            {
                if (Vector3.Magnitude(transform.position - player[i].transform.position) < pickdistance)
                {
                    if (Input.GetButtonDown("Interact" + playerNum) && player[i].GetComponent<PlayerProperties>().item == null)
                    {
                        carrier = player[i];
                        carrier.GetComponent<PlayerProperties>().item = gameObject;
                        AddSkilltoPlayer(skill);
                        rb.isKinematic = true;
                        rb.detectCollisions = false;
                        transform.parent = player[i].transform;
                        transform.localPosition = new Vector3(0.0f, 4.1f, 0.0f);
                        transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    xPosPlayer = player[i].GetComponent<Transform>().position.x;
                    zPosPlayer = player[i].GetComponent<Transform>().position.z;
                }
            }
            else
            {
                if (Input.GetButtonDown("Interact" + playerNum))
                {
                    Loseitem();
                }
            }
        }
    }

    public void Loseitem()
    {
        DeleteSkillfromPlayer(skill);
        carrier.GetComponent<PlayerProperties>().item = null;
        rb.isKinematic = false;
        rb.detectCollisions = true;
        transform.parent = null;
        rb.AddForce(carrier.transform.forward * throwforce + Vector3.up * throwforce * 0.1f);
        carrier = null;
    }

    public void Respawnitem()
    {
        transform.position = startpos;
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.velocity = Vector3.zero;
    }

    void AddSkilltoPlayer(string skill)
    {
        var dict = new Dictionary<string, object>();
		dict["currentScene"] = Application.loadedLevelName;
        dict["skill"] = skill;
        dict["playerNum"] = playerNum;

        Analytics.CustomEvent("pickUpItem", new Dictionary<string, object>
        {
            { "skill", skill },
            { "playerNum",  playerNum},
            { "xPos", xPosPlayer },
            { "zPos", zPosPlayer },
			{ "currentScene", Application.loadedLevelName }
        });

        UnityAnalyticsHeatmap.HeatmapEvent.Send("checkpoint", gameObject.transform.position, dict);

        if (carrier.name.Equals("kabouterdun"))
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
