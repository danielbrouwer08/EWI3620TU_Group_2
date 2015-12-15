using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

    public float startinghealth;
    public Slider healthbar;
<<<<<<< HEAD
=======
    public float currenthealth;
>>>>>>> 675bd35fad532a10756c2f0d8bcc8b1fe002474a
    public float health;
	public int score;

    bool damaged;
    bool dead;

	void Awake ()
    {
        startinghealth = 100;
<<<<<<< HEAD
        health = startinghealth;
        healthbar.value = health;
=======
        currenthealth = startinghealth;
        healthbar.value = currenthealth;
		score = 0;
        health = 100;
>>>>>>> 675bd35fad532a10756c2f0d8bcc8b1fe002474a
	}
	
	void Update ()
    {
        if(currenthealth <= 0)
        {
            //Death();
        }
<<<<<<< HEAD
        healthbar.value = health;
=======
        healthbar.value = currenthealth;
>>>>>>> 675bd35fad532a10756c2f0d8bcc8b1fe002474a
	}

    public void TakeDamage(float damage)
    {
        damaged = true;
<<<<<<< HEAD
        health -= damage;
=======
        currenthealth -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
>>>>>>> 675bd35fad532a10756c2f0d8bcc8b1fe002474a
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
