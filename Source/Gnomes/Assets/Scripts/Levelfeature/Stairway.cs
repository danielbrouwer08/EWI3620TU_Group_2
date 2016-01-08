using UnityEngine;
using System.Collections;

public class Stairway : MonoBehaviour {

	public bool appear = false;
	private int startheight = -25;
	private Vector3 startpos;
	private float speed = 15;
	// Use this for initialization
	void Start () {
		startpos = this.transform.position;
		this.transform.position += new Vector3(0,startheight,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(appear && this.transform.position.y < startpos.y)
		{
			transform.position += new Vector3(0,Time.deltaTime*speed,0);
		}
	
	}
}
