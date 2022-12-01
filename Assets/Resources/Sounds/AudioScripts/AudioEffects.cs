using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour {

    GameController GC;

    public AudioSource ads;


    // Use this for initialization
    void Start () {
        GC = GetComponent<GameController>();

        ads = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(GC.temporizador < 33)
        {
            ads.Play();
        }
	}
}
