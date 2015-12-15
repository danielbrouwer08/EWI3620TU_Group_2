using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
            //Death();
        }
        healthbar.value = health;
	}

    public void TakeDamage(float damage)
    {
        damaged = true;
        health -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
