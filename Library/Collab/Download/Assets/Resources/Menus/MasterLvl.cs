using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterLvl : MonoBehaviour {

    public AudioMixer audioMusic;
    public AudioMixer audioEffects;

    public void SetEffectLvl(float effectLvl)
    {
        audioEffects.SetFloat("EffectsAudio", effectLvl);
    }
    public void SetMusicLvl(float musicLvl)
    {
        audioMusic.SetFloat("MusicAudio", musicLvl);
    }
}
