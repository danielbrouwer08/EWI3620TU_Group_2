using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

<<<<<<< HEAD
    public float startinghealth;
    public Slider healthbar;
    public float currenthealth;
=======
    public float health;
	public int score;
>>>>>>> 41e6782bdbc16c49496dc113108971a89452c9c1

    bool damaged;
    bool dead;

	void Awake ()
    {
<<<<<<< HEAD
        startinghealth = 100;
        currenthealth = startinghealth;
        healthbar.value = currenthealth;
=======
		score = 0;
        health = 100;
>>>>>>> 41e6782bdbc16c49496dc113108971a89452c9c1
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
