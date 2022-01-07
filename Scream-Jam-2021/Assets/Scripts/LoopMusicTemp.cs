using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMusicTemp : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicStart;

    void Start()
    {
        musicSource.PlayOneShot(musicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }

}
