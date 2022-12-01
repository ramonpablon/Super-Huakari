using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour {

    public AudioSource[] ads = new AudioSource[4];

    public AudioClip[] adc = new AudioClip[4];

    public void Selected()
    {
        ads[0].PlayOneShot(adc[0]);
    }

    public void Select()
    {
        ads[1].PlayOneShot(adc[1]);
    }

    public void ClickOn()
    {
        ads[2].PlayOneShot(adc[2]);
    }

    public void ClickOff()
    {
        ads[3].PlayOneShot(adc[3]);
    }
}
