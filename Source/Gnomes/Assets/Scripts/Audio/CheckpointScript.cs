using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;
    private string kleur;
    private checkpoint checkpoint;
    

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Checkpoint")){
            kleur = other.gameObject.GetComponent<Renderer>().sharedMaterial.name;
            checkpoint = other.gameObject.GetComponent<checkpoint>();
            if (kleur == "checkpointstate2" && checkpoint.audioplayed[0] == true ^ checkpoint.audioplayed[1] == true)
            {
                source.PlayOneShot(clip);
            }
        }
    }
}
