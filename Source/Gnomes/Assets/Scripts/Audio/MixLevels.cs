using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MixLevels : MonoBehaviour {

    public AudioMixer MasterMixer;

    public void SetMusiclvl(float musiclvl)
    {
        MasterMixer.SetFloat("MusicVolume", musiclvl);
    }
	
    public void SetSFXlvl(float sfxlvl)
    {
        MasterMixer.SetFloat("SFXVolume", sfxlvl);
    }
}
