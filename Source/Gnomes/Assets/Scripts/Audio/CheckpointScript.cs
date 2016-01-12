using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;

    private string kleur;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Checkpoint")){
            kleur = other.gameObject.GetComponent<Renderer>().material.name;
            Debug.Log(kleur);
            if (kleur != "checkpointstate2")
            {
                source.PlayOneShot(clip);
            }
        }
    }

}
