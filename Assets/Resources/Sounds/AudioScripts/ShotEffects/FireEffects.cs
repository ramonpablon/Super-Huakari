using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffects : MonoBehaviour
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
        clips.Add("Fire_Throw", adc[0]);
        clips.Add("Fire_MapCollider", adc[1]);
        clips.Add("Fire_PlayerCollider", adc[2]);
        #endregion

        #region Set Audio Source
        source.Add("Fire_Throw", ads[0]);
        source.Add("Fire_MapCollider", ads[1]);
        source.Add("Fire_PlayerCollider", ads[2]);
        #endregion
    }

    public static void ShotThrow()
    {
        source["Fire_Throw"].PlayOneShot(clips["Fire_Throw"]); // reproduz o som de lançamento
    }

    public static void MapCollider()
    {
        source["Fire_MapCollider"].PlayOneShot(clips["Fire_MapCollider"]); // reproduz o som de tocar na parede
    }

    public static void PlayerCollider()
    {
        source["Fire_PlayerCollider"].PlayOneShot(clips["Fire_PlayerCollider"]); // reproduz o som de de atingir player
    }
}