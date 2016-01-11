using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Movementaudio : MonoBehaviour {

    public AudioSource playeraudio;
    public PlayerController player;

	void OnCollisionEnter(Collision other)
    {
        playeraudio.Play();
    }

    void Update()
    {
        Debug.Log(player.walking);
        if (player.walking == true)
        {
            playeraudio.Play();
        }
    }

    
}
