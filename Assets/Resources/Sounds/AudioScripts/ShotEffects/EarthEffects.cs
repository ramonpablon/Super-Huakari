using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEffects : MonoBehaviour
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
        clips.Add("Earth_Throw", adc[0]);
        clips.Add("Earth_MapCollider", adc[1]);
        clips.Add("Earth_PlayerCollider", adc[2]);
        #endregion

        #region Set Audio Source
        source.Add("Earth_Throw", ads[0]);
        source.Add("Earth_MapCollider", ads[1]);
        source.Add("Earth_PlayerCollider", ads[2]);
        #endregion
    }

    public static void ShotThrow()
    {
        source["Earth_Throw"].PlayOneShot(clips["Earth_Throw"]); // reproduz o som de lançamento
    }

    public static void MapCollider()
    {
        source["Earth_MapCollider"].PlayOneShot(clips["Earth_MapCollider"]); // reproduz o som de tocar na parede
    }

    public static void PlayerCollider()
    {
        source["Earth_PlayerCollider"].PlayOneShot(clips["Earth_PlayerCollider"]); // reproduz o som de de atingir player
    }
}