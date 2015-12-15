using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerProperties : MonoBehaviour {


    public float startinghealth;
    public Slider healthbar;
    public float currenthealth;

    public float health;
	public int score;


    bool damaged;
    bool dead;

	void Awake ()
    {
        startinghealth = 100;
        currenthealth = startinghealth;
        healthbar.value = currenthealth;
		score = 0;
        health = 100;
	}
	
	void Update ()
    {
        if(currenthealth <= 0)
        {
            //Death();
        }
        healthbar.value = currenthealth;
	}

    public void TakeDamage(float damage)
    {
        damaged = true;
        currenthealth -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
