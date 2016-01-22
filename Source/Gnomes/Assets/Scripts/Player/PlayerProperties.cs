using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
//using UnityEditor;

public class PlayerProperties : MonoBehaviour {

    public float startinghealth;
    public Slider healthbar;
    private Transform inventory;
    private Text itemtext;
    public float health;
	public int score;
    private GameManger gameManager;
    private int playerNum;
    bool damaged;
    bool dead;
    public GameObject item;
    private Text scoretext;
    public AudioSource pijnsource;
    public AudioClip pijnsound;
    private CameraShake camerashaker;

	void Awake ()
    {
        playerNum = GetComponent<PlayerController>().playerNum;
        inventory = GameObject.FindGameObjectWithTag("IngamePanel").transform.FindChild("Player " + playerNum).FindChild("Item inventory");
        camerashaker = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
        startinghealth = 100;
        health = startinghealth;
        healthbar.value = health;
        itemtext = inventory.FindChild("Item").FindChild("Text").GetComponent<Text>();
        itemtext.text = "None";
	}

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
        scoretext = GameObject.FindGameObjectWithTag("IngamePanel").transform.FindChild("Player " + playerNum).FindChild("Point total").FindChild("Score").FindChild("Text").GetComponent<Text>();
        score = gameManager.getscore(playerNum);
        scoretext.text = score.ToString();
    }
	
	void Update ()
    {
        if(health <= 0 || transform.position.y < -25.0f)
        {
            Death();
        }
        healthbar.value = health;
    }

    public void TakeDamage(float damage)
    {
        damaged = true;
        health -= damage;
        pijnsource.PlayOneShot(pijnsound);
        if (camerashaker != null && camerashaker.shaking == false)
        {
            camerashaker.StartCoroutine("Shake");
        }
    }

    public void UpdateScore(int newscore)
    {
        score = score + newscore;
        scoretext.text = score.ToString();
    }

    public void Death()
    {
        int playerNum = GetComponent<PlayerController>().playerNum;

        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            { "score", score },
            { "x-location",  gameObject.transform.position.x},
            { "z-location", gameObject.transform.position.z},
            { "playerNum",  playerNum},
			{ "currentScene", Application.loadedLevelName}

        });

        var dict3 = new Dictionary<string, object>();

		dict3["currentScene"] = Application.loadedLevelName;
        dict3["Score"] = score;

        if (gameObject.name.Equals("kabouterdun"))
        {
            UnityAnalyticsHeatmap.HeatmapEvent.Send("playerOneDeath", GetComponent<Transform>().position, dict3);
        }

        if (gameObject.name.Equals("kabouterdik"))
        {
            UnityAnalyticsHeatmap.HeatmapEvent.Send("playerTwoDeath", GetComponent<Transform>().position, dict3);
        }

        if(item != null)
        {
            item.GetComponent<PickUpItem>().Respawnitem();
            item.GetComponent<PickUpItem>().Loseitem();
        }

        Vector3 position = getLastSavedPos();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        transform.position = position;
        if(Mathf.Abs(players[0].transform.position.x - players[1].transform.position.x) > 100)
        {
            if(players[0].name.Equals(gameObject.name))
            {
                players[1].GetComponent<PlayerProperties>().Death();
            }
            else
            {
                players[0].GetComponent<PlayerProperties>().Death();
            }
        }
        health = startinghealth;

    }

    public Vector3 getLastSavedPos()
    {
        Vector3 spawnpos;

        if (playerNum == 1)
        {
            spawnpos = gameManager.returnCurrent().P1Pos;
            print("p1 spawn pos: " + spawnpos);
        }
        else
        {
            spawnpos = gameManager.returnCurrent().P2Pos;
            print("p2 spawn pos: " + spawnpos);
        }

        transform.position = spawnpos;
        return spawnpos;
    }
}
