using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;
    private string kleur;
    private checkpoint checkpoint;
    private int playernumber;
    
    void Awake()
    {
        if(this.gameObject.name == "kabouterdun")
        {
            playernumber = 1;
        }
        if(this.gameObject.name == "kabouterdik")
        {
            playernumber = 0;
        }
    }    

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Checkpoint")){
            kleur = other.gameObject.GetComponent<Renderer>().sharedMaterial.name;
            if (kleur == "checkpointstate1" && playernumber != 1)
            {
                source.PlayOneShot(clip);
            }
        }
    }
}
