using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedStone : MonoBehaviour {

    public AudioSource[] ads;
    public AudioClip[] adc;

    void Start()
    {
        ads = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ads[0].PlayOneShot(adc[0]);
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
    }
}
