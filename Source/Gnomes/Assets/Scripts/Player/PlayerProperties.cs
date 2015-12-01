using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

    private float health;

	void Start ()
    {
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
