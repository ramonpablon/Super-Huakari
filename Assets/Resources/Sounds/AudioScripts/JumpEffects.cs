using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffects : MonoBehaviour
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
        clips.Add("P1_Jump", adc[0]);
        clips.Add("P2_Jump", adc[1]);
        clips.Add("P3_Jump", adc[2]);
        clips.Add("P4_Jump", adc[3]);
        #endregion

        #region Set Audio Source
        source.Add("P1_Jump", ads[0]);
        source.Add("P2_Jump", ads[1]);
        source.Add("P3_Jump", ads[2]);
        source.Add("P4_Jump", ads[3]);
        #endregion
    }

    public static void PlayerJump(string playerPrefs)
    {
        source[playerPrefs + "_Jump"].PlayOneShot(clips[playerPrefs + "_Jump"]); // procura banco com audio clip
    }



}