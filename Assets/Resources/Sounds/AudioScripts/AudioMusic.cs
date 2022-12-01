using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMusic : MonoBehaviour
{
    public AudioSource[] audioSource = new AudioSource[5];

    public static AudioSource[] ads;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        ads = audioSource;
    }

    public static void Victory() // reproduz musicas do menu vitoria
    {
        ads[0].enabled = false;
        ads[1].enabled = true;
        ads[2].enabled = true;
    }
}
