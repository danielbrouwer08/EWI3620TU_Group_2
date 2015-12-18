using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEditor;

public class PlayerProperties : MonoBehaviour {

    public float startinghealth;
    public Slider healthbar;
    public float health;
	public int score;

    bool damaged;
    bool dead;

	void Awake ()
    {
        startinghealth = 100;
        health = startinghealth;
        healthbar.value = health;
	}
	
	void Update ()
    {
        if(health <= 0)
        {
            Death();
        }
        healthbar.value = health;
	}

    public void TakeDamage(float damage)
    {
        damaged = true;
        health -= damage;
    }

    public void UpdateScore(int newscore)
    {
        score = score + newscore;
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
            { "currentScene", EditorApplication.currentScene}

        });

        Destroy(gameObject);

    }
}
