using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutroEffects : MonoBehaviour
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
        clips.Add("Neutro_Throw", adc[0]);
        clips.Add("Neutro_MapCollider", adc[1]);
        clips.Add("Neutro_PlayerCollider", adc[2]);
        #endregion

        #region Set Audio Source
        source.Add("Neutro_Throw", ads[0]);
        source.Add("Neutro_MapCollider", ads[1]);
        source.Add("Neutro_PlayerCollider", ads[2]);
        #endregion
    }

    public static void ShotThrow()
    {
        source["Neutro_Throw"].PlayOneShot(clips["Neutro_Throw"]); // reproduz o som de lançamento
    }

    public static void MapCollider()
    {
        source["Neutro_MapCollider"].PlayOneShot(clips["Neutro_MapCollider"]); // reproduz o som de tocar na parede
    }

    public static void PlayerCollider()
    {
        source["Neutro_PlayerCollider"].PlayOneShot(clips["Neutro_PlayerCollider"]); // reproduz o som de de atingir player
    }
}