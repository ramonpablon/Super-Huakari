using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEffects : MonoBehaviour {

    public AudioSource[] audioSource = new AudioSource[2];

    public static AudioSource[] ads;

    public GameController GC;

    private void Start()
    {
        audioSource = GetComponents<AudioSource>();
        ads = audioSource;
    }

    void Update () {
        if (GC.temporizador < 15.02 && GC.temporizador > 15 || GC.temporizador < 5.02 && GC.temporizador > 5)
            ads[0].GetComponent<AudioSource>().pitch = GetComponent<AudioSource>().pitch + 0.5f;
    }

    public static void Gongo()
    {
        ads[1].enabled = true;
    }
    public static void TicTac()
    {
        ads[0].enabled = false;
    }

}
