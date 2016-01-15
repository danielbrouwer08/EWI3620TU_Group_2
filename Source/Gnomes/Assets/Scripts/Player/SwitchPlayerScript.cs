using UnityEngine;
using System.Collections;

public class SwitchPlayerScript : MonoBehaviour {
    private GameObject[] players;
    private bool SinglePlayer = false;
    Vector3 curposition;
    Vector3 lastposition;
    private Animation anim;
    private float animwalk = 3.5f;


    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (PlayerPrefs.GetString("playermode") == "single")
        {
            for(int i = 0; i < players.Length; i++) {
                players[i].AddComponent<AIPath>();
                players[i].GetComponent<AIPath>().target = players[1 - i].transform;
                players[i].GetComponent<AIPath>().endReachedDistance = 2;
                players[i].GetComponent<AIPath>().speed = 9;
            }
            SinglePlayer = true;
            players[1].GetComponent<PlayerController>().enabled = false;
            players[0].GetComponent<AIPath>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("SwitchPlayer") && SinglePlayer)
        {
            foreach(GameObject cur in players)
            {
                cur.GetComponent<PlayerController>().isActive = !cur.GetComponent<PlayerController>().isActive;
                cur.GetComponent<PlayerController>().playerNum = 3 - cur.GetComponent<PlayerController>().playerNum;
                cur.GetComponent<PlayerController>().enabled = !cur.GetComponent<PlayerController>().enabled;
                cur.GetComponent<AIPath>().enabled = !cur.GetComponent<AIPath>().enabled;


            }
        }
        if (Input.GetButtonDown("StopPlayer") && SinglePlayer)
        {
            foreach(GameObject cur in players)
            {
                if(!cur.GetComponent<PlayerController>().enabled)
                {
                    cur.GetComponent<AIPath>().canMove = !cur.GetComponent<AIPath>().canMove;
                }
            }
        }

        foreach (GameObject cur in players)
        {
            curposition = cur.GetComponent<Transform>().position;
            if (SinglePlayer && cur.GetComponent<AIPath>().canMove && !cur.GetComponent<PlayerController>().isActive)
            {
                anim = cur.GetComponent<Animation>();
                anim["Lopen"].speed = animwalk;
                if (curposition != lastposition)
                {
                    anim.Play("Lopen");
                    Debug.Log("Moving");
                    lastposition = curposition;
                }
                else
                {
                    anim.Play("Stilstaan");
                }
            }
        }


    }
}
