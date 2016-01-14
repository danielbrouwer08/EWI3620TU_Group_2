using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Movementaudio : MonoBehaviour {

    public AudioSource playermovement;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip land;

    private float lowPitchRangewalk = 1.2f;
    private float highPitchRangewalk = 1.9f;
    private float lowPitchRangerun = 1.9f;
    private float highPitchRangerun = 3.0f;

    public PlayerController player;

	void OnCollisionEnter(Collision other)
    {
        //Colliders met andere opbjecten
    }

    void Update()
    {
        //StartCoroutine(PlayerWalk());
        if (player.anim.IsPlaying("Lopen") && playermovement.isPlaying == false)
        {
            playermovement.pitch = Random.Range(lowPitchRangewalk, highPitchRangewalk);
            playermovement.PlayOneShot(walk);
        }

        if (player.anim.IsPlaying("Rennen") && playermovement.isPlaying == false)
        {
            playermovement.pitch = Random.Range(lowPitchRangerun, highPitchRangerun);
            playermovement.PlayOneShot(walk);
        }
        if (player.anim.IsPlaying("Springen") && playermovement.isPlaying == false)
        {

            playermovement.PlayOneShot(jump);
        }
    }

    //IEnumerator PlayerWalk()
    //{
    //    if (player.anim.IsPlaying("Lopen") && playermovement.isPlaying == false)
    //    {
    //        playermovement.PlayOneShot(walk);
    //    }

    //    if (player.anim.IsPlaying("Rennen"))
    //    {
    //        playermovement.PlayOneShot(walk);
    //    }
    //    if (player.anim.IsPlaying("Springen") && playermovement.isPlaying == false)
    //    {
    //        playermovement.PlayOneShot(jump);
    //        playermovement.PlayOneShot(land);
    //    }
    //}
    
}
