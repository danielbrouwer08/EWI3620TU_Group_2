using UnityEngine;
using System.Collections;

public class EnemyProperties : MonoBehaviour {

    public float health;
    public GameObject[] wp;

    void Start()
    {
        wp = GameObject.FindGameObjectsWithTag("WoodenPoleWall");
    }
	
	void Update ()
    {
        if(health <= 0)
        {
            Debug.Log(health);
            Destroy(gameObject);
            for (int i=0; i<wp.Length; i++)
            {
                Destroy(wp[i]);
            }
        }
	}

}
