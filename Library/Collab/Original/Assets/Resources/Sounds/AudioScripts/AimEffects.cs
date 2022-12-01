using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEffects : MonoBehaviour {

    public AudioSource[] audioSource;

    public AudioClip[] adc;

    public static AudioSource[] ads;

    public static Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        ads = audioSource;

        clips.Add("P1_Aim", adc[0]);
        clips.Add("P2_Aim", adc[1]);
        clips.Add("P3_Aim", adc[2]);
        clips.Add("P4_Aim", adc[3]);


    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ads[0].PlayOneShot(clips.);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ads[1].PlayOneShot(adc[1]);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ads[2].PlayOneShot(adc[2]);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ads[3].PlayOneShot(adc[3]);
        }
    }*/

    public static void PlayerAim(string playerPrefs)
    {
        ads[0].PlayOneShot(clips[playerPrefs + "_Aim"]); // procura banco com audio clip
    }

}
