using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    public AudioSource Track1;
    public AudioSource Track2;

    public void Start()
    {
        Track1.Play();
    }

    public void PlayTrack1()
    {
        Track2.Stop();
        Track1.Play();
    }

    public void PlayTrack2()
    {
        Track1.Stop();
        Track2.Play();
    }
}
