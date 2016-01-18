using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Menusounds : MonoBehaviour {

    public AudioSource menuclick;
    public AudioClip clicksound;

    public void PlayClick()
    {
        menuclick.PlayOneShot(clicksound);
    }
}
