using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumppadEffects : MonoBehaviour {

    public static AudioSource ads;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    public static void Jumppadsound()
    {
        ads.PlayOneShot(Resources.Load("Sounds/Effects/Environment/jumppad_audio") as AudioClip);
    }
}
