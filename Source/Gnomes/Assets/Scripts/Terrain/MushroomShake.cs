using UnityEngine;
using System.Collections;

public class MushroomShake : MonoBehaviour {


	public float speed;
	public float flipTime;
	private float standardXRotation;
	private float totalTime;
	private bool shake;

	// Use this for initialization
	void Start () {
		shake =  (Random.Range (0,2)==1);//50% chance the mushroom is going to shake

		if(shake)
		{
			speed = Random.Range(0.1f,0.5f) * speed;
			flipTime = Random.Range (0.1f,1.0f) * flipTime;
			//Randomize speed and fliptime a bit for diversity.
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(shake)
		{
		totalTime += Time.deltaTime;
			transform.rotation = Quaternion.Euler(speed * Time.deltaTime, 0.0f, 0.0f) * transform.rotation;
		
		//flip rotation direction after some amount of time
		if(totalTime > flipTime)
		{
			speed = -speed;
			totalTime = 0.0f;
		}
		}
	}

}
