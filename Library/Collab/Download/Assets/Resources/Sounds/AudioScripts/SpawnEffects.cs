using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffects : MonoBehaviour
{
    public static AudioSource ads;

    private void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    public static void StoneSpawn()
    {
        ads.PlayOneShot(Resources.Load("Sounds/Effects/Itens/stone_spawn") as AudioClip);
    }
}