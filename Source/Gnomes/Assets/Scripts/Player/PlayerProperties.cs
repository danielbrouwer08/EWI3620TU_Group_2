using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

    public float health;
	public int score;

	void Start ()
    {
		score = 0;
        health = 100;
	}
	
	void Update ()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
