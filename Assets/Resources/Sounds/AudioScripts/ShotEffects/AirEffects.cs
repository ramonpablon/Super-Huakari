using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEffects : MonoBehaviour
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
        clips.Add("Air_Throw", adc[0]);
        clips.Add("Air_MapCollider", adc[1]);
        clips.Add("Air_PlayerCollider", adc[2]);
        #endregion

        #region Set Audio Source
        source.Add("Air_Throw", ads[0]);
        source.Add("Air_MapCollider", ads[1]);
        source.Add("Air_PlayerCollider", ads[2]);
        #endregion
    }

    public static void ShotThrow()
    {
        source["Air_Throw"].PlayOneShot(clips["Air_Throw"]); // reproduz o som de lançamento
    }

    public static void MapCollider()
    {
        source["Air_MapCollider"].PlayOneShot(clips["Air_MapCollider"]); // reproduz o som de tocar na parede
    }

    public static void PlayerCollider()
    {
        source["Air_PlayerCollider"].PlayOneShot(clips["Air_PlayerCollider"]); // reproduz o som de de atingir player
    }
}