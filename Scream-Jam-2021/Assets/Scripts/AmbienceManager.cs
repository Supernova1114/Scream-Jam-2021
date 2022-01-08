using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    private const int delayTime = 1;
    public AudioSource ambienceNoise;
    public AudioClip sceneStartSound;

    void Start()
    {
        ambienceNoise.PlayOneShot(sceneStartSound);
        ambienceNoise.PlayScheduled(AudioSettings.dspTime + delayTime);
    }
}
