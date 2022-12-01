using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEffects : MonoBehaviour
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
        clips.Add("P1_Aim", adc[0]);
        clips.Add("P2_Aim", adc[1]);
        clips.Add("P3_Aim", adc[2]);
        clips.Add("P4_Aim", adc[3]);
        #endregion

        #region Set Audio Source
        source.Add("P1_Aim", ads[0]);
        source.Add("P2_Aim", ads[1]);
        source.Add("P3_Aim", ads[2]);
        source.Add("P4_Aim", ads[3]);
        #endregion
    }

    public static void PlayerAim(string playerPrefs)
    {
        source[playerPrefs + "_Aim"].PlayOneShot(clips[playerPrefs + "_Aim"]); // procura banco com audio clip
    }
}