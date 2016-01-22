using UnityEngine;
using System.Collections;

public class EnemyProperties : MonoBehaviour {

    public float health;
    public GameObject[] wp;
	private GameObject body;
	private Shader standardShader;
	public float visualHitTime = 0.4f;
	public Color[] colors = new Color[2];

    void Start()
    {
		body = transform.Find("Body").gameObject;

		colors[0] = body.GetComponent<Renderer>().material.color;
		colors[1] = Color.red;
		//Debug.Log ("kleur: " + tempColor);
        wp = GameObject.FindGameObjectsWithTag("WoodenPoleWall");
    }
	
	void Update ()
    {
        if(health <= 0)
        {
            //Debug.Log(health);
            Destroy(gameObject);
            for (int i=0; i<wp.Length; i++)
            {
                Destroy(wp[i]);
            }
        }
	}

	public void dealDamage(int damage)
	{
		StartCoroutine(visualHit());
		health = health - damage;
	}

	IEnumerator visualHit(){
		body.GetComponent<Renderer>().material.color = colors[0];
		yield return new WaitForSeconds(visualHitTime);
		body.GetComponent<Renderer>().material.color = colors[1];
	} 
}
