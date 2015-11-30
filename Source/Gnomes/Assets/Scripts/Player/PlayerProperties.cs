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
	
	}

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
