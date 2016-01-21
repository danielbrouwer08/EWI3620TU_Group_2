using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;
    private string kleur;
    private checkpoint checkpoint;
    public int playernumber;
    
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
            checkpoint = other.gameObject.GetComponent<checkpoint>();
			if (kleur == "checkpointstate1" && checkpoint!=null && checkpoint.collided[playernumber] == false)
            {
                source.PlayOneShot(clip);
            }
        }
    }
}
