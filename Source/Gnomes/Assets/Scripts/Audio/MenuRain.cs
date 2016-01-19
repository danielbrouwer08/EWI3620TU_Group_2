using UnityEngine;
using System.Collections;

public class MenuRain : MonoBehaviour {

	// Use this for initialization
	void Awake () {
            DontDestroyOnLoad(this.gameObject);
	}
}
