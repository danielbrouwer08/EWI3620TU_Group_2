using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class WaveSoundScript : MonoBehaviour {

    public GameObject[] players;
    private float[] distance;
    public AudioSource wavesound;

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        distance = new float[2];
    }
	
	// Update is called once per frame
	void Update () {
        float mindistance = MinDistance();

        wavesound.volume = 1 - mindistance / 30;
	}

    private float MinDistance()
    {
        for (int i = 0; i < players.Length; i++)
        {
            distance[i] = players[i].transform.position.x - this.transform.position.x;
        }
        float mindistance = Mathf.Min(distance[0], distance[1]);
        return mindistance;
    }
}
