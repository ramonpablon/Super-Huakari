using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedEffects : MonoBehaviour
{
    public AudioSource[] audioSource = new AudioSource[4];

    public AudioClip[] adc = new AudioClip[4];

    public static AudioSource[] ads;

    public static Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    public static Dictionary<string, AudioSource> source = new Dictionary<string, AudioSource>();

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        ads = audioSource;

        #region Set Audio Clips
        clips.Add("Fox", adc[0]);
        clips.Add("Shark", adc[1]);
        clips.Add("Bear", adc[2]);
        clips.Add("Eagle", adc[3]);
        #endregion

        #region Set Audio Source
        source.Add("Fox", ads[0]);
        source.Add("Shark", ads[1]);
        source.Add("Bear", ads[2]);
        source.Add("Eagle", ads[3]);
        #endregion
    }

    public static void FoxSound()
    {
        source["Fox"].PlayOneShot(clips["Fox"]); 
    }

    public static void SharkSound()
    {
        source["Shark"].PlayOneShot(clips["Shark"]); 
    }

    public static void BearSound()
    {
        source["Bear"].PlayOneShot(clips["Bear"]); 
    }

    public static void EagleSound()
    {
        source["Eagle"].PlayOneShot(clips["Eagle"]); 
    }
    public static void WolfSound()
    {
        source["Fox"].PlayOneShot(clips["Fox"]);
    }
}