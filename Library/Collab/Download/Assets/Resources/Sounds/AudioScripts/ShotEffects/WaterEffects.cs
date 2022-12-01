using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffects : MonoBehaviour
{
    public AudioSource[] audioSource = new AudioSource[3];

    public AudioClip[] adc = new AudioClip[3];

    public static AudioSource[] ads;

    public static Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    public static Dictionary<string, AudioSource> source = new Dictionary<string, AudioSource>();

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        ads = audioSource;

        #region Set Audio Clips
        clips.Add("Water_Throw", adc[0]);
        clips.Add("Water_MapCollider", adc[1]);
        clips.Add("Water_PlayerCollider", adc[2]);
        #endregion

        #region Set Audio Source
        source.Add("Water_Throw", ads[0]);
        source.Add("Water_MapCollider", ads[1]);
        source.Add("Water_PlayerCollider", ads[2]);
        #endregion
    }

    public static void ShotThrow()
    {
        source["Water_Throw"].PlayOneShot(clips["Water_Throw"]); // reproduz o som de lançamento
    }

    public static void MapCollider()
    {
        source["Water_MapCollider"].PlayOneShot(clips["Water_MapCollider"]); // reproduz o som de tocar na parede
    }

    public static void PlayerCollider()
    {
        source["Water_PlayerCollider"].PlayOneShot(clips["Water_PlayerCollider"]); // reproduz o som de de atingir player
    }
}